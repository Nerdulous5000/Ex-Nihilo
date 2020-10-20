using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EntityTable {
    public static Dictionary<string, EntityData> Entities = new Dictionary<string, EntityData>()
    {
        {"AirMiner", Resources.Load<EntityData>("Entities/AirMiner")},
        {"Storage", Resources.Load<EntityData>("Entities/Storage")},
    };
}
