using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EntityTable {
    public static Dictionary<string, Entity> Entities = new Dictionary<string, Entity>()
    {
        {"AirMiner", Resources.Load<Entity>("Entities/AirMiner")},
        {"Storage", Resources.Load<Entity>("Entities/Storage")},
    };
}
