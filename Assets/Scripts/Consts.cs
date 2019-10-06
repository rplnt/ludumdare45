using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Consts {
    public enum Dir {
        N=0, E, S, W
    };

    public static Dir GetDir(Vector3 heading) {
        float x = heading.x;
        float y = heading.y;

        if (Mathf.Abs(x) > Mathf.Abs(y)) {
            if (x > 0) {
                return Dir.N;
            } else {
                return Dir.S;
            }
        } else {
            if (y > 0) {
                return Dir.E;
            } else {
                return Dir.W;
            }
        }
    }

    public static Vector3 GetPositionWithinBounds(Bounds bounds) {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector3(x, y);
    }
}
