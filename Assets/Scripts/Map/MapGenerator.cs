using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : IDisposable {
    const float MAX_NOISE_OFFSET = 1e3f;
    const int NUM_THREADS_PER_AXIS = 8;
    const int THREAD_GROUPS_PER_AXIS = MapChunk.CHUNK_SIZE / NUM_THREADS_PER_AXIS;
    const int BUFFER_SIZE = MapChunk.CHUNK_SIZE * MapChunk.CHUNK_SIZE;
    const int KERNEL = 0;

    public int Seed { get; private set; }

    MapGenerationSettings settings;
    System.Random rng;

    ComputeShader noiseShader;
    ComputeBuffer noiseResultsBuffer;
    NoiseResult[] noiseResults;

    ComputeBuffer offsetsBuffer;
    ComputeBuffer biomeOptionsBuffer;
    ComputeBuffer resourceOptionsBuffer;
    ComputeBuffer heightmapOptionsBuffer;

    public MapGenerator(int seed, MapGenerationSettings settings) {
        Seed = seed;
        this.settings = settings;
        rng = new System.Random(Seed);
        Initialize();
    }

    ~MapGenerator() {
        Dispose();
    }

    void Initialize() {
        InitShader();
        IgnoreComputeBufferWarnings();
    }

    public void Dispose() {
        noiseResultsBuffer.Dispose();
        offsetsBuffer.Dispose();
        biomeOptionsBuffer.Dispose();
        resourceOptionsBuffer.Dispose();
        heightmapOptionsBuffer.Dispose();
    }

    void InitShader() {
        noiseShader = Resources.Load("ComputeShaders/TerrainGen") as ComputeShader;

        // Output buffer
        noiseResultsBuffer = new ComputeBuffer(BUFFER_SIZE, NoiseResult.Size);
        noiseShader.SetBuffer(KERNEL, "NoiseResults", noiseResultsBuffer);


        // Randomized offsets for each layer's noise
        NoiseOffsets noiseOffsets = new NoiseOffsets() {
            Fire = NewRandomOffset(),
            Water = NewRandomOffset(),
            Earth = NewRandomOffset(),
            Air = NewRandomOffset(),
            Light = NewRandomOffset(),
            Dark = NewRandomOffset(),
            Resource = NewRandomOffset(),
            Heightmap = NewRandomOffset(),
        };
        offsetsBuffer = new ComputeBuffer(1, NoiseOffsets.Size);
        offsetsBuffer.SetData(new NoiseOffsets[1] { noiseOffsets });
        noiseShader.SetBuffer(KERNEL, "NoiseOffsets", offsetsBuffer);


        // Noise options for biome generation
        NoiseOptions biomeOptions = new NoiseOptions() {
            Scale = settings.BiomeSettings.Scale,
            Amplitude = settings.BiomeSettings.Amplitude,
            Octaves = settings.BiomeSettings.Octaves,
            Lacunarity = settings.BiomeSettings.Lacunarity,
            Persistence = settings.BiomeSettings.Persistence,
        };
        biomeOptionsBuffer = new ComputeBuffer(1, NoiseOptions.Size);
        biomeOptionsBuffer.SetData(new NoiseOptions[1] { biomeOptions });
        noiseShader.SetBuffer(KERNEL, "BiomeOptions", biomeOptionsBuffer);


        // Noise options for resource generation
        NoiseOptions resourceOptions = new NoiseOptions() {
            Scale = settings.ResourceSettings.Scale,
            Amplitude = settings.ResourceSettings.Amplitude,
            Octaves = settings.ResourceSettings.Octaves,
            Lacunarity = settings.ResourceSettings.Lacunarity,
            Persistence = settings.ResourceSettings.Persistence,
        };
        resourceOptionsBuffer = new ComputeBuffer(1, NoiseOptions.Size);
        resourceOptionsBuffer.SetData(new NoiseOptions[1] { resourceOptions });
        noiseShader.SetBuffer(KERNEL, "ResourceOptions", resourceOptionsBuffer);


        // Noise options for heightmap generation
        NoiseOptions heightmapOptions = new NoiseOptions() {
            Scale = settings.HeightmapSettings.Scale,
            Amplitude = settings.HeightmapSettings.Amplitude,
            Octaves = settings.HeightmapSettings.Octaves,
            Lacunarity = settings.HeightmapSettings.Lacunarity,
            Persistence = settings.HeightmapSettings.Persistence,
        };
        heightmapOptionsBuffer = new ComputeBuffer(1, NoiseOptions.Size);
        heightmapOptionsBuffer.SetData(new NoiseOptions[1] { heightmapOptions });
        noiseShader.SetBuffer(KERNEL, "HeightmapOptions", heightmapOptionsBuffer);
    }

    void IgnoreComputeBufferWarnings() {
        GC.SuppressFinalize(noiseResultsBuffer);
        GC.SuppressFinalize(offsetsBuffer);
        GC.SuppressFinalize(biomeOptionsBuffer);
        GC.SuppressFinalize(resourceOptionsBuffer);
        GC.SuppressFinalize(heightmapOptionsBuffer);
    }

    void DispatchShader(Vector2Int position) {
        noiseShader.SetVector("ChunkPosition", new Vector4(position.x, position.y, 0, MapChunk.CHUNK_SIZE));
        noiseShader.Dispatch(KERNEL, THREAD_GROUPS_PER_AXIS, THREAD_GROUPS_PER_AXIS, 1);
        noiseResults = new NoiseResult[BUFFER_SIZE];
        noiseResultsBuffer.GetData(noiseResults);
    }

    public Dictionary<Vector2Int, TileData> GenerateChunk(MapChunk chunk, out Vector3Int[] positions, out Tile[] tilemapTiles) {
        DispatchShader(chunk.Position);

        Dictionary<Vector2Int, TileData> tileData = new Dictionary<Vector2Int, TileData>();
        positions = new Vector3Int[BUFFER_SIZE];
        tilemapTiles = new Tile[BUFFER_SIZE];
        for (int y = 0; y < MapChunk.CHUNK_SIZE; y++) {
            for (int x = 0; x < MapChunk.CHUNK_SIZE; x++) {
                int index = y * MapChunk.CHUNK_SIZE + x;
                Vector2Int chunkCoords = new Vector2Int(x, y);

                NoiseResult tileParameters = noiseResults[index];
                BiomeType biomeType = Biome.Generate(settings, tileParameters);
                ResourceType resourceType = Resource.Generate(settings, tileParameters, biomeType);
                TileData tile = new TileData(
                    chunk,
                    chunkCoords,
                    new Vector2Int((int)tileParameters.Position.x, (int)tileParameters.Position.y),
                    biomeType,
                    resourceType
                );

                tileData[chunkCoords] = tile;
                positions[index] = tile.TilemapPosition;
                tilemapTiles[index] = tile.ResourceTile ?? tile.BiomeTile;
            }
        }
        return tileData;
    }

    Vector2 NewRandomOffset() {
        return new Vector2(
            2 * ((float)rng.NextDouble() - 0.5f) * MAX_NOISE_OFFSET,
            2 * ((float)rng.NextDouble() - 0.5f) * MAX_NOISE_OFFSET
        );
    }
}

public struct NoiseOptions {
    public float Scale;
    public float Amplitude;
    public int Octaves;
    public float Lacunarity;
    public float Persistence;

    public const int Size = 4 * sizeof(float) + sizeof(int);
}

public struct NoiseOffsets {
    public Vector2 Fire;
    public Vector2 Water;
    public Vector2 Earth;
    public Vector2 Air;
    public Vector2 Light;
    public Vector2 Dark;
    public Vector2 Resource;
    public Vector2 Heightmap;

    public const int Size = 8 * 2 * sizeof(float);
}

public struct NoiseResult {
    public Vector2 Position;
    public float Fire;
    public float Water;
    public float Earth;
    public float Air;
    public float Light;
    public float Dark;
    public float Resource;
    public float Height;

    public const int Size = 2 * sizeof(float) + 8 * sizeof(float);
}
