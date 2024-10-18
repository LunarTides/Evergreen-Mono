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
            GenerateDebugGround();
        }

        private static void GenerateDebugGround()
        {
            for (int y = 20; y < 40; y++)
            {
                for (int x = 0; x < 50; x++)
                {
                    Tile.Create<Dirt>(new Vector2(x, y));
                }
            }
        }
    }
}
