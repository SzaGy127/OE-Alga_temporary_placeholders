using System;

namespace OE.ALGA.Optimalizalas
{
    public class HatizsakProblema 
    {
        public HatizsakProblema(int n, int wmax, int[] w, double[] p)
        {
            N = n;
            Wmax = wmax;
            W = w;
            P = p;
        }

        public int N { get; }
        public int Wmax { get; }
        public int[] W { get; }
        public double[] P { get; }

        public int OsszSuly(bool[] pakolas)
        {
            int s = 0;
            for(int i = 0; i < N; i++)
            {
                if (pakolas[i])
                {
                    s += W[i];
                }
            }
            return s;
        }

        public double OsszErtek(bool[] pakolas)
        {
            double s = 0;
            for(int i = 0; i < N; i++)
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
            if(OsszSuly(pakolas) < Wmax)
            {
                return true;
            }
            return false;
        }
    }

    public class NyersEro<T>
    {
        int m; // # of solutions
        Func<int, T> generator; //fv ami legenerálja az összes lehetséges értéket (egyesével)
        Func<T, double> josag; //fv ami megmondja, mennyire jó/rossz az adott pakolas

        public NyersEro(int m, Func<int, T> generator, Func<T, double> josag)
        {
            this.m = m;
            this.generator = generator;
            this.josag = josag;
            //LepesSzam = 0;
        }

        public int LepesSzam { get; } //!!missing implementation!!

        public T OptimalisMegoldas() 
        {
            T o = generator(0);
            for(int i = 1; i < m; i++)
            {
                T x = generator(i);
                if(josag(x) > josag(o))
                {
                    o = x;
                }
            }
            return o;
        }
    }

    public class NyersEroHatizsakProblema
    {
        HatizsakProblema problema;

        public NyersEroHatizsakProblema(HatizsakProblema problema)
        {
            this.problema = problema;
        }

        public int LepesSzam { get; }   //!!missing implementation!!
                                        //majd ezt az optimalismegoldasban kell beállítani
        public bool[] OptimalisMegoldas()
        {
            int help = 2;
            for(int i = 0; i < problema.N; i++)
            {
                help *= 2;
            }
            NyersEro<bool[]> nyersEro = new NyersEro<bool[]>(help, Generator, Josag);
            return nyersEro.OptimalisMegoldas();
        }

        public double OptimalisErtek()
        {
            throw new NotImplementedException();
        }

        public bool[] Generator(int i)
        {
            bool[] output = new bool[problema.N];

        }

        public double Josag(bool[] pakolas)
        {
            for(int i = 0; i < pakolas.Length; i++)
            {
                if (!pakolas[i])
                {
                    return -1;
                }
            }
            return problema.OsszErtek(pakolas);
        }
    }
}
