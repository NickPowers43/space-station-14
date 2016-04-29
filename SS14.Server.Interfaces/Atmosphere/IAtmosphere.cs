using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS14.Server.Interfaces.Atmosphere
{
    public interface IAtmosphere
    {
        float Pressure { get; }
        float Temperature { get; }
        float Volume { get; }
        float Moles { get; }

        float O2 { get; }
        float CO2 { get; }
        float CO { get; }
        float N2 { get; }
        float H2O { get; }

        void Mix(ref Gas src);
        void Update(float frameTime);
        void Transfer(IAtmosphere other, float area);
    }
}
