using Evergreen.Tiles;
using Microsoft.Xna.Framework;
using System;

namespace Evergreen
{
    public class WorldGen
    {
        private Random rng;

        public WorldGen()
        {
        }

        public void GenerateWorld(int seed)
        {
            rng = new Random(seed);

            // TODO: Remove
            Tile.Create<Dirt>(Vector2.Zero);
        }
    }
}
