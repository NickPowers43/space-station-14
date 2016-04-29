using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SS14.Server.Interfaces.Atmosphere;

namespace SS14.Server.Services.Atmosphere
{
    public class BaseAtmosphere : IAtmosphere
    {
        Gas gas;
        
        public float O2
        {
            get
            {
                return gas.O2;
            }
        }
        public float CO2
        {
            get
            {
                return gas.CO2;
            }
        }
        public float CO
        {
            get
            {
                return gas.CO;
            }
        }
        public float N2
        {
            get
            {
                return gas.N2;
            }
        }
        public float H2O
        {
            get
            {
                return gas.H2O;
            }
        }

        public float Moles
        {
            get
            {
                return gas.Moles;
            }
        }
        public float Pressure
        {
            get
            {
                return gas.Pressure;
            }
        }
        public float Volume
        {
            get
            {
                return gas.Volume;
            }
        }
        public float Temperature
        {
            get
            {
                return gas.Temperature;
            }
        }

        public BaseAtmosphere(float o2, float co2, float co, float n2, float h2o, float v, float sa, float t)
        {
            gas = new Gas(
                o2,
                co2,
                co,
                n2,
                h2o,
                v,
                sa,
                t);
        }

        //public void Update()
        //{
        //    prevBackPressure = backPressure;
        //    backPressure = 0.0f;
        //}

        //public void TransferInto(IAtmosphere src, float moleTransfer)
        //{
        //    float vRatio;
        //    float finalT;
        //    if (volume > 0.0f)
        //    {
        //        if (volume > Volume)
        //        {
        //            vRatio = Volume / volume;
        //            finalT = ((1.0f - vRatio) * src.Temperature) + (vRatio * temperature);
        //        }
        //        else
        //        {
        //            vRatio = volume / Volume;
        //            finalT = (vRatio * src.Temperature) + ((1.0f - vRatio) * temperature);
        //        }
        //    }
        //    else
        //    {

        //    }

        //    float finalP = 0.0f;



        //    //determine final temperature and pressure for this atmosphere

        //    temperature = finalT;
        //    pressure = finalP;
        //}

        public virtual void Mix(ref Gas src)
        {
            gas.Mix(ref src);
        }

        public virtual void Update(float frameTime)
        {
            //determine the amount of volume to transfer
            //if (other.Pressure > Pressure)
            //{
            //    float transfer = 0.0f;
            //}
            //else
            //{

            //}
            //float tVolume = (other.Pressure - Pressure) * area;

            //float moleTransfer;
            //if (tVolume > Volume)
            //{
            //    moleTransfer = Volume / tVolume;
            //}
            //else
            //{
            //    moleTransfer = tVolume / Volume;
            //}

            //TransferInto(other, tVolume);
        }

        public virtual void Transfer(IAtmosphere other, float area)
        {

        }
    }
}
