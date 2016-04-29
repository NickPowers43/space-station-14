using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS14.Server.Services.Atmosphere
{
    public struct Gas
    {
        private static double SPHERE_AREA_CONSTANT = Math.Pow(Math.PI, 1.0f / 3.0f);
        private static double SPHERE_VOLUME_CONSTANT = 1.0d / (6.0d * Math.Sqrt(Math.PI));

        //Some fields in this struct are redundant. There was a trade of space for time.
        private float v;
        private float p;//(cached variable)
        private float t;

        //moles of each gas
        private float o2;
        private float co2;
        private float co;
        private float n2;
        private float h2o;
        private float total;//sum of each gas

        public float Temperature
        {
            get
            {
                return t;
            }
            set
            {
                float scale = value / t;

                p *= scale;
                t = value;
            }
        }
        public float Pressure
        {
            get
            {
                return p;
            }
            set
            {
                float scale = value / p;

                p = value;
                t *= scale;
            }
        }
        public float Volume
        {
            get
            {
                return v;
            }
            set
            {
                float sa = SurfaceAreaOfASphere(v);

                CalculatePressure(sa);
                v = value;
            }
        }

        public float O2
        {
            get
            {
                return o2;
            }
            set
            {
                float diff = value - o2;

                total += diff;
            }
        }
        public float CO2
        {
            get
            {
                return co2;
            }
        }
        public float CO
        {
            get
            {
                return co;
            }
        }
        public float N2
        {
            get
            {
                return n2;
            }
        }
        public float H2O
        {
            get
            {
                return h2o;
            }
        }
        
        public float Moles
        {
            get
            {
                return total;
            }
            set
            {
                float scale = value / total;

                ScaleMoles(scale);
                p *= scale;
                total = value;
            }
        }
        public float Energy
        {
            get
            {
                return t * total;
            }
            set
            {
                float scale = value / t * total;

                //scale temperature and pressure to match new energy level
                t *= scale;
                p *= scale;
            }
        }

        public Gas(float o2, float co2, float co, float n2, float h2o, float v, float sa, float t)
        {
            total = 0.0f;
            total += this.o2 = o2;
            total += this.co2 = co2;
            total += this.co = co;
            total += this.n2 = n2;
            total += this.h2o = h2o;

            this.p = 0.0f;
            this.v = v;
            this.t = t;
            CalculatePressure(sa);
        }

        public void ScaleMoles(float scale)
        {
            o2 *= scale;
            co2 *= scale;
            co *= scale;
            n2 *= scale;
            h2o *= scale;
        }
        
        private void CalculatePressure(float sa)
        {
            p = t * total / sa;
        }

        public void Mix(ref Gas src)
        {
            float nTotal = src.Moles + total;
            //find equilibrium temperature
            float nTemperature = ((src.t * src.total) + (t * total)) / nTotal;

            p *= (nTemperature / t) * (nTotal / total);

            t = nTemperature;

            o2 += src.o2;
            co2 += src.co2;
            co += src.co;
            n2 += src.n2;
            h2o += src.h2o;

            total = nTotal;
            t = nTemperature;
        }

        public void Take(out Gas dst, float volume)
        {
            if (volume > 0.0f)
            {
                float orgV = v;

                volume = Math.Min(orgV, volume);

                float c = volume / orgV;
                float c1 = 1.0f - c;

                dst = new Gas(
                    o2 * c,
                    co2 * c,
                    co * c,
                    n2 * c,
                    h2o * c,
                    volume, SurfaceAreaOfASphere(volume), t);

                //redefine RAW values of this container. P and T will not change
                ScaleMoles(c1);
                total *= c1;
                p *= c1;
            }
            else
            {
                throw new ArgumentException("Cannot take minus " + -volume + "m^3 from gas.");
            }
        }

        public void TakeAndMix(ref Gas dst, float volume)
        {
            Gas temp;

            Take(out temp, volume);

            dst.Mix(ref temp);
        }

        public override string ToString()
        {
            return "E: " + Energy + "\nT: " + t + "\nV: " + v + "\nP: " + p + "\n_O2: " + o2 + "\nCO2: " + co2 + "\n_CO: " + co + "\n_N2: " + n2 + "\nH2O: " + h2o + "\nTOT: " + total;
        }

        private static float SurfaceAreaOfASphere(float v)
        {
            return (float)(SPHERE_AREA_CONSTANT * Math.Pow(6.0d * v, 2.0d / 3.0d));
        }

        public static void Testing()
        {
            Gas g0 = new Gas(
                1,
                0,
                0,
                0,
                0,
                1,
                6,
                1);

            Gas g1 = new Gas(
                1,
                0,
                0,
                0,
                0,
                1,
                6,
                1);

            Console.WriteLine("g0:\n" + g0);
            Console.WriteLine();
            Console.WriteLine("g1:\n" + g1);
            Console.WriteLine();
            Console.WriteLine("Energy T: " + (g1.Energy + g0.Energy));
            Console.WriteLine();
            Console.WriteLine();

            Gas taken;
            g0.Take(out taken, 0.5f);

            Console.WriteLine("taken:\n" + taken);
            Console.WriteLine();
            Console.WriteLine();

            g1.Mix(ref taken);

            Console.WriteLine("g1:\n" + g1);
            Console.WriteLine("Energy T: " + (g1.Energy + g0.Energy));
            Console.WriteLine();
            Console.WriteLine();

            g1.TakeAndMix(ref g0, 0.5f);

            Console.WriteLine("g0:\n" + g0);
            Console.WriteLine();
            Console.WriteLine("g1:\n" + g1);
            Console.WriteLine();
            Console.WriteLine("Energy T: " + (g1.Energy + g0.Energy));
            Console.WriteLine();
            Console.WriteLine();

            Console.ReadKey();
            Console.ReadKey();

            return;
        }
    }
}
