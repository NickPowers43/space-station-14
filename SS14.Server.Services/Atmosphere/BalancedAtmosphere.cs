using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SS14.Server.Interfaces.Atmosphere;

namespace SS14.Server.Services.Atmosphere
{
    public class BalancedAtmosphere : BaseAtmosphere
    {
        List<GasPassage> gasPassages;

        public BalancedAtmosphere(float o2, float co2, float co, float n2, float h2o, float toxins, float v, float sa, float t) : base(o2, co2, co, n2, h2o, toxins, v, sa, t)
        {
            gasPassages = new List<GasPassage>();
        }

        public override void Mix(ref Gas src)
        {

        }

        public override void Update(float frameTime)
        {
            //distribute the flow of gas among the multiple transfer points

            gasPassages.Clear();
        }

        public override void Transfer(IAtmosphere other, float area)
        {
            for (int i = 0; i < gasPassages.Count; i++)
            {
                if (gasPassages[i].atm == other)
                {
                    gasPassages[i] = new GasPassage(other, gasPassages[i].area + area);
                    return;
                }
            }
            gasPassages.Add(new GasPassage(other, area));
        }

        struct GasPassage
        {
            public float area;
            public IAtmosphere atm;

            public GasPassage(IAtmosphere atm, float area)
            {
                this.atm = atm;
                this.area = area;
            }
        }
    }
}
