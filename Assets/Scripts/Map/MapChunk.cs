using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapChunk {
    public const int CHUNK_SIZE = 16;

    public Vector2Int Position { get; private set; }

    MapManager manager;
    Dictionary<Vector2Int, TileData> tiles;

    public MapChunk(MapManager manager, Vector2Int position) {
        Position = position;
        this.manager = manager;
        GenerateTiles();
    }

    void GenerateTiles() {
        tiles = manager.MapGenerator.GenerateChunk(
            this,
            out Vector3Int[] positions,
            out Tile[] tilemapTiles
        );
        manager.Tilemap.SetTiles(positions, tilemapTiles);
    }

    public void SetTile(Vector2Int index, TileData tile) {
        tiles[index] = tile;
        Tile displayTile = tile.ResourceTile ?? tile.BiomeTile;
        manager.Tilemap.SetTile(tile.TilemapPosition, displayTile);
    }

    public TileData GetTile(Vector2Int index) {
        if (!tiles.ContainsKey(index)) {
            return null;
        }
        return tiles[index];
    }
}
