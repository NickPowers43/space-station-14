using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SS14.Server.Interfaces.Map;

namespace SS14.Server.Interfaces.Atmos
{
    public interface IAtmosRegionManager
    {
        void Initialize(ISS14Server server);

        IAtmosRegion RegionAt(Vector2f position);
        void Update(float elapsedTime);

    }
}
