using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileData {
    public MapChunk Chunk { get; private set; }
    public Vector2Int PositionInChunk { get; private set; }
    public Vector2Int PositionInWorld { get; private set; }
    public Vector3Int TilemapPosition { get => new Vector3Int(PositionInWorld.x, PositionInWorld.y, 0); }

    public BiomeType BiomeType { get; private set; }
    public Tile BiomeTile { get; private set; }
    public ResourceType ResourceType { get; private set; }
    public Tile ResourceTile { get; private set; }

    public TileData(
        MapChunk chunk,
        Vector2Int positionInChunk,
        Vector2Int positionInWorld,
        BiomeType biome,
        ResourceType resource
    ) {
        Chunk = chunk;
        PositionInChunk = positionInChunk;
        PositionInWorld = positionInWorld;

        BiomeType = biome;
        BiomeTile = Biome.Tiles[BiomeType];
        ResourceType = resource;
        ResourceTile = Resource.Tiles[ResourceType];
    }
}
