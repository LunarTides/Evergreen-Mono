﻿using System.Collections.Generic;

namespace Evergreen {
    public abstract class Inventory {
        public static Dictionary<uint, List<Item>> Items = [];

        public static void Add(Item item) {
            // TODO: Calculate index properly.
            uint index = (uint)Items.Count;

            if (Items.TryGetValue(index, out List<Item> value)) {
                value.Add(item);
            } else {
                Items.Add(index, [item]);
            }
        }
    }
}
