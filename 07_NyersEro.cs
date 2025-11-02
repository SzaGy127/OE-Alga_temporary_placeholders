using System;

namespace OE.ALGA.Optimalizalas
{
    public class HatizsakProblema
    {
        public HatizsakProblema(int n, int wmax, int[] w, float[] p)
        {
            N = n;
            Wmax = wmax;
            W = w;
            P = p;
        }

        public int N { get; }
        public int Wmax { get; }
        public int[] W { get; }
        public float[] P { get; }

        public int OsszSuly(bool[] pakolas)
        {
            int s = 0;
            for (int i = 0; i < N; i++)
            {
                if (pakolas[i])
                {
                    s += W[i];
                }
            }
            return s;
        }

        public float OsszErtek(bool[] pakolas)
        {
            float s = 0;
            for (int i = 0; i < N; i++)
            {
                if (pakolas[i])
                {
                    s += P[i];
                }
            }
            return s;
        }

        public bool Ervenyes(bool[] pakolas)
        {
            return OsszSuly(pakolas) <= Wmax;
        }
    }

    public class NyersEro<T>
    {
        int m; // # of solutions
        Func<int, T> generator; //fv ami legenerálja az összes lehetséges értéket (egyesével)
        Func<T, float> josag; //fv ami megmondja, mennyire jó/rossz az adott pakolas
        private int lepesSzam = 0;

        public NyersEro(int m, Func<int, T> generator, Func<T, float> josag)
        {
            this.m = m;
            this.generator = generator;
            this.josag = josag;
        }

        public int LepesSzam { get { return lepesSzam; } }

        public T OptimalisMegoldas()
        {
            T o = generator(1);
            lepesSzam = 0;
            for(int i = 2; i <= m; i++)
            {
                T x = generator(i);
                if (josag(x) > josag(o))
                {
                    o = x;
                }
                lepesSzam++;
            }
            return o;
        }
    }

    public class NyersEroHatizsakPakolas
    {
        HatizsakProblema problema;
        private int lepesSzam = 0;

        public NyersEroHatizsakPakolas(HatizsakProblema problema)
        {
            this.problema = problema;
        }

        public int LepesSzam { get { return lepesSzam; } }   //!!missing implementation!!
                                        //majd ezt az optimalismegoldasban kell beállítani
        public bool[] Generator(int i)
        {
            int num = i - 1;
            bool[] K = new bool[problema.N];
            for(int j = 0; j <  problema.N; j++)
            {
                K[j] = (num / i << j) % 2 == 1;
            }
            return K;
        }

        public float Josag(bool[] pakolas)
        {
            if (problema.Ervenyes(pakolas))
            {
                return problema.OsszErtek(pakolas);
            }
            else
            {
                return -1;
            }
        }

        public bool[] OptimalisMegoldas()
        {
            NyersEro<bool[]> nyersEro = new NyersEro<bool[]>(1 << problema.N, Generator, Josag);
            bool[] output = nyersEro.OptimalisMegoldas();
            lepesSzam = nyersEro.LepesSzam;
            return output;
        }

        public float OptimalisErtek()
        {
            return problema.OsszErtek(OptimalisMegoldas());
        }
    }
}
