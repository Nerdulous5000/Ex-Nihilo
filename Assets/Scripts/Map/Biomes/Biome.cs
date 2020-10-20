using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum BiomeType {
    None,
    Lake,
    Field,
    Forest,
    Swamp,
    Desert,
    Volcano,
    Lava,
    Wasteland,
    Sky,
    Chasms,
    Spooky,
    Holy,
}

public class Biome {
    public static Dictionary<BiomeType, Tile> Tiles = new Dictionary<BiomeType, Tile>()
    {
        { BiomeType.None, null },
        { BiomeType.Lake, Resources.Load<Tile>("Tiles/Biomes/Lake") },
        { BiomeType.Field, Resources.Load<Tile>("Tiles/Biomes/Field") },
        { BiomeType.Forest, Resources.Load<Tile>("Tiles/Biomes/Forest") },
        { BiomeType.Swamp, Resources.Load<Tile>("Tiles/Biomes/Swamp") },
        { BiomeType.Desert, Resources.Load<Tile>("Tiles/Biomes/Desert") },
        { BiomeType.Volcano, Resources.Load<Tile>("Tiles/Biomes/Volcano") },
        { BiomeType.Lava, Resources.Load<Tile>("Tiles/Biomes/Lava") },
        { BiomeType.Wasteland, Resources.Load<Tile>("Tiles/Biomes/Wasteland") },
        { BiomeType.Sky, Resources.Load<Tile>("Tiles/Biomes/Sky") },
        { BiomeType.Chasms, Resources.Load<Tile>("Tiles/Biomes/Chasms") },
        { BiomeType.Spooky, Resources.Load<Tile>("Tiles/Biomes/Spooky") },
        { BiomeType.Holy, Resources.Load<Tile>("Tiles/Biomes/Holy") },
    };

    public static BiomeType Generate(MapGenerationSettings settings, NoiseResult parameters) {
        if (parameters.Height < settings.SeaLevel) {
            return BiomeType.Lake;
        }

        // if (parameters.Fire > 0.7)
        // {
        //     if (parameters.Fire > 1.0)
        //     {
        //         return BiomeType.Lava;
        //     }
        //     return BiomeType.Volcano;
        // }

        // if (parameters.Fire > 0.3 && parameters.Air > 0.5)
        // {
        //     return BiomeType.Wasteland;
        // }

        // if (parameters.Air > 0.7)
        // {
        //     if (parameters.Air > 1.0)
        //     {
        //         return BiomeType.Chasms;
        //     }
        //     return BiomeType.Sky;
        // }

        if (parameters.Water < -0.5) {
            return BiomeType.Desert;
        }

        // if (parameters.Earth > 0.5)
        // {
        //     return BiomeType.Forest;
        // }

        // if (parameters.Earth > 0.3 && parameters.Water > 0.5)
        // {
        //     return BiomeType.Swamp;
        // }

        return BiomeType.Field;
    }
}
