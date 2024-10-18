using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Evergreen
{
    public class World
    {
        public static Dictionary<Vector2, Tile> Tiles = [];

        public World()
        {
            Evergreen.WorldGen = new();

            // TODO: Remove
            Evergreen.WorldGen.GenerateWorld(Random.Shared.Next());

            Evergreen.Inventory = new();

            Evergreen.Player = new(Evergreen.Instance);
            Evergreen.Instance.Components.Add(Evergreen.Player);
        }
    }
}
