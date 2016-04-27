using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SS14.Shared;
using SS14.Server.Interfaces;
using SS14.Server.Interfaces.Map;
using SS14.Server.Interfaces.Atmosphere;

namespace SS14.Server.Services.Atmosphere
{
    class AtmosphereManager : IAtmoshpereManager
    {
        Atmosphere globalAtmosphere;
        AABBi aabb;

        public void Initialize(ISS14Server server)
        {
            server.GetMap().TileChanged += new TileChangedEventHandler(HandleTileChanged);

            //Initialize a global atmosphere with 100.0m^3 volume;
            globalAtmosphere = new Atmosphere(100.0f, 101.325f);
        }

        void HandleTileChanged(TileRef tileRef, Tile oldTile)
        {
            //Do nothing for now
            return;

            //Modify internal representation of map
            aabb.Fit(new Vector2i(tileRef.X, tileRef.Y));

        }

        public void Update(float frameTime)
        {

        }

        public IAtmosphere AtmosphereAt(Vector2f pos)
        {
            return globalAtmosphere;
        }

        private class Atmosphere : IAtmosphere
        {
            float volume;
            float pressure;

            public Atmosphere(float volume, float pressure)
            {
                this.volume = volume;
                this.pressure = pressure;
            }
        }
    }
}
