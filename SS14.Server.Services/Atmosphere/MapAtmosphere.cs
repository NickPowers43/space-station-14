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
    class MapAtmosphere : IMapAtmosphere
    {
        //For now just have one large atmosphere for the map
        IAtmosphere globalAtmosphere;
        AABBi aabb;

        public MapAtmosphere(IMapManager mm)
        {
            Initialize(mm);
        }

        public void Initialize(IMapManager mm)
        {
            mm.TileChanged += new TileChangedEventHandler(HandleTileChanged);

            //Initialize a global atmosphere with a healthy mixture of gases
            globalAtmosphere = new BalancedAtmosphere(800,5,0,200,2,0,100000,100400,1);
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
            globalAtmosphere.Update(frameTime);
        }

        public IAtmosphere AtmosphereAt(Vector2f pos)
        {
            return globalAtmosphere;
        }

        public Vector2f AtmosphereVelocityAt(Vector2f pos)
        {
            return new Vector2f(0, 0);
        }
    }
}
