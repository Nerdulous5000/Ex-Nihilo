using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum ResourceType {
    None,
    Fire,
    Water,
    Earth,
    Air,
}

public class Resource {
    public static Dictionary<ResourceType, Tile> Tiles = new Dictionary<ResourceType, Tile>()
    {
        { ResourceType.None, null },
        { ResourceType.Fire, Resources.Load<Tile>("Tiles/Resources/Fire") },
        { ResourceType.Water, Resources.Load<Tile>("Tiles/Resources/Water") },
        { ResourceType.Earth, Resources.Load<Tile>("Tiles/Resources/Earth") },
        { ResourceType.Air, Resources.Load<Tile>("Tiles/Resources/Air") },
    };

    public static ResourceType Generate(MapGenerationSettings settings, NoiseResult parameters, BiomeType biome) {
        if (biome == BiomeType.None || biome == BiomeType.Lake || biome == BiomeType.Lake || biome == BiomeType.Chasms) {
            return ResourceType.None;
        }

        if (parameters.Resource > settings.ResourceThreshold) {
            return ResourceType.Air;
        }

        return ResourceType.None;
    }
}
