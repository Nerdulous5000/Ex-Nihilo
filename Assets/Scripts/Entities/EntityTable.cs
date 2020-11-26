using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EntityTable {
    public static Dictionary<string, EntityBehaviour> Entities = new Dictionary<string, EntityBehaviour>()
    {
        {"AirMiner", Resources.Load<EntityBehaviour>("Entities/AirMiner")},
        {"Storage", Resources.Load<EntityBehaviour>("Entities/Storage")},
        {"Mover", Resources.Load<EntityBehaviour>("Entities/Mover")},
    };
}
