using UnityEngine;
using System.Collections;

// Adapted from https://gist.github.com/jstanden/1489447
public class SimplexNoise {
    const float ONE_THIRD = 0.333333333f;
    const float ONE_SIXTH = 0.166666667f;

    public int Seed { get; private set; }

    int[] A = new int[3];
    float s, u, v, w;
    int i, j, k;
    int[] T;

    public SimplexNoise() {
        System.Random rand = new System.Random();
        T = new int[8];
        for (int q = 0; q < 8; q++)
            T[q] = rand.Next();
    }

    public SimplexNoise(int seed) {
        System.Random rand = new System.Random(seed);
        T = new int[8];
        for (int q = 0; q < 8; q++)
            T[q] = rand.Next();
    }

    public float Sample(
        Vector3 position = new Vector3(),
        float scale = 1f,
        Vector3 offset = new Vector3(),
        float amplitude = 1f,
        int octaves = 1,
        float lacunarity = 2,
        float persistence = 0.9f
    ) {
        float val = 0f;
        for (int n = 0; n < octaves; n++) {
            Vector3 v3 = position / scale + offset;
            val += noise(v3.x, v3.y, v3.z) * amplitude;
            scale *= lacunarity;
            amplitude *= persistence;
        }
        return normalize(val);
    }

    public int GetDensity(Vector3 loc) {
        float val = Sample(loc);
        return (int)Mathf.Lerp(0, 255, val);
    }

    float normalize(float val) {
        return val * 1.5f + 0.5f;
    }

    // Simplex Noise Generator
    float noise(float x, float y, float z) {
        s = (x + y + z) * ONE_THIRD;
        i = fastfloor(x + s);
        j = fastfloor(y + s);
        k = fastfloor(z + s);

        s = (i + j + k) * ONE_SIXTH;
        u = x - i + s;
        v = y - j + s;
        w = z - k + s;

        A[0] = 0; A[1] = 0; A[2] = 0;

        int hi = u >= w ? u >= v ? 0 : 1 : v >= w ? 1 : 2;
        int lo = u < w ? u < v ? 0 : 1 : v < w ? 1 : 2;

        return kay(hi) + kay(3 - hi - lo) + kay(lo) + kay(0);
    }

    float kay(int a) {
        s = (A[0] + A[1] + A[2]) * ONE_SIXTH;
        float x = u - A[0] + s;
        float y = v - A[1] + s;
        float z = w - A[2] + s;
        float t = 0.6f - x * x - y * y - z * z;
        int h = shuffle(i + A[0], j + A[1], k + A[2]);
        A[a]++;
        if (t < 0) return 0;
        int b5 = h >> 5 & 1;
        int b4 = h >> 4 & 1;
        int b3 = h >> 3 & 1;
        int b2 = h >> 2 & 1;
        int b1 = h & 3;

        float p = b1 == 1 ? x : b1 == 2 ? y : z;
        float q = b1 == 1 ? y : b1 == 2 ? z : x;
        float r = b1 == 1 ? z : b1 == 2 ? x : y;

        p = b5 == b3 ? -p : p;
        q = b5 == b4 ? -q : q;
        r = b5 != (b4 ^ b3) ? -r : r;
        t *= t;
        return 8 * t * t * (p + (b1 == 0 ? q + r : b2 == 0 ? q : r));
    }

    int shuffle(int i, int j, int k) {
        return b(i, j, k, 0) + b(j, k, i, 1) + b(k, i, j, 2) + b(i, j, k, 3) + b(j, k, i, 4) + b(k, i, j, 5) + b(i, j, k, 6) + b(j, k, i, 7);
    }

    int b(int i, int j, int k, int B) {
        return T[b(i, B) << 2 | b(j, B) << 1 | b(k, B)];
    }

    int b(int N, int B) {
        return N >> B & 1;
    }

    int fastfloor(float n) {
        return n > 0 ? (int)n : (int)n - 1;
    }
}
