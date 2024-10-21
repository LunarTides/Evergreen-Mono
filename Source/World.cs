using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Evergreen {
    public abstract class World {
        public static Dictionary<Vector2, Tile> Tiles = [];

        public static void Create() {
            // TODO: Remove
            WorldGen.GenerateWorld(Random.Shared.Next());

            Evergreen.Player = new(Evergreen.Instance);
            Evergreen.Instance.Components.Add(Evergreen.Player);
        }
    }
}
