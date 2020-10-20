using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// https://issuetracker.unity3d.com/issues/flickering-tilemap-seams-between-tile-chunks-are-rendered-when-moving-the-camera?_ga=2.231846873.735204673.1603010697-1788133500.1581482117

[ExecuteAlways]
public class MapManager : MonoBehaviour {
    public Tilemap Tilemap;
    public MapGenerationSettings GenerationSettings;

    [Header("Generation Options")]
    public int Seed = 0;
    public Transform Center;
    public int IngameChunkRadius = 5;

    [Header("Editor Options")]
    public bool AutoUpdateInEditor = false;
    public int EditorChunkRadius = 3;
    public bool ShowChunkBounds = false;

    [HideInInspector]
    public MapGenerator MapGenerator { get; private set; }

    Dictionary<Vector2Int, MapChunk> chunkBucket;
    int calculatedChunkRadius { get => Application.isPlaying ? IngameChunkRadius : EditorChunkRadius; }

    void Awake() {
        Initialize();
    }

    void Update() {
        if (Application.isPlaying) {
            CheckChunkGeneration();
        } else if (AutoUpdateInEditor) {
            // Needed because editor is weird on recompile
            if (chunkBucket == null) {
                Initialize();
            }
            CheckChunkGeneration();
        }
    }

    void Initialize() {
        MapGenerator = new MapGenerator(Seed, GenerationSettings);
        Tilemap.ClearAllTiles();
        chunkBucket = new Dictionary<Vector2Int, MapChunk>();
    }

    void Cleanup() {
        if (MapGenerator != null) {
            MapGenerator.Dispose();
        }
    }

    void CheckChunkGeneration() {
        Vector2Int centerChunk = WorldPositionToChunkPosition(Center.position);
        for (int x = -calculatedChunkRadius; x <= calculatedChunkRadius; x++) {
            for (int y = -calculatedChunkRadius; y <= calculatedChunkRadius; y++) {
                Vector2Int chunkPos = new Vector2Int(centerChunk.x + x, centerChunk.y + y);
                if (!chunkBucket.ContainsKey(chunkPos)) {
                    MapChunk chunk = new MapChunk(this, chunkPos);
                    chunkBucket.Add(chunkPos, chunk);
                }
            }
        }
    }

    public Vector2Int WorldPositionToChunkPosition(Vector3 position) {
        return new Vector2Int(
            Mathf.FloorToInt(position.x / MapChunk.CHUNK_SIZE),
            Mathf.FloorToInt(position.y / MapChunk.CHUNK_SIZE)
        );
    }


    // === EDITOR STUFF === //
    public void OnEditorSettingsUpdate() {
        if (AutoUpdateInEditor) {
            Cleanup();
            Initialize();
        }
    }

    public void OnEditorRegeneratePressed() {
        Cleanup();
        Initialize();
        CheckChunkGeneration();
    }

    void OnDrawGizmos() {
        if (ShowChunkBounds) {
            foreach (var entry in chunkBucket) {
                var chunk = entry.Value;
                Vector3 a = new Vector3(MapChunk.CHUNK_SIZE * chunk.Position.x, MapChunk.CHUNK_SIZE * chunk.Position.y, 0);
                Vector3 b = new Vector3(a.x + MapChunk.CHUNK_SIZE, a.y, 0);
                Vector3 c = new Vector3(a.x + MapChunk.CHUNK_SIZE, a.y + MapChunk.CHUNK_SIZE, 0);
                Vector3 d = new Vector3(a.x, a.y + MapChunk.CHUNK_SIZE, 0);
                Gizmos.color = Color.green;
                Gizmos.DrawLine(a, b);
                Gizmos.DrawLine(b, c);
                Gizmos.DrawLine(c, d);
                Gizmos.DrawLine(d, a);
            }
        }
    }
}
