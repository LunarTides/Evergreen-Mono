using Evergreen.Tiles;
using Microsoft.Xna.Framework;
using System;

namespace Evergreen
{
    public abstract class WorldGen
    {
        private static Random rng;

        public static void GenerateWorld(int seed)
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
