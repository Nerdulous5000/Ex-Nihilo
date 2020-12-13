using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities {
    public class Storage : EntityBehaviour
    {
        public List<Item> startingInventory = new List<Item>();
        
        public override void OnSpawn() {
            Inventory = new Inventory(32);
            foreach (Item item in startingInventory) {
                Inventory.Give(new ItemStack(item));
            }
        }

        void Update()
        {
            
        }
    }
}
