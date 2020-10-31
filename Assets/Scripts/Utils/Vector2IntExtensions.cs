using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Vector2IntExtensions
{

    public static bool IsRightFrom(this Vector2Int self, Vector2Int other) {
        return self.x > other.x;
    }
    public static bool IsUpFrom(this Vector2Int self, Vector2Int other) {
        return self.y > other.y;
    }
    public static bool IsLeftFrom(this Vector2Int self, Vector2Int other) {
        return self.x < other.x;
    }
    public static bool IsDownFrom(this Vector2Int self, Vector2Int other) {
        return self.y < other.y;
    }
}
