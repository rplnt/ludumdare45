using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starfield : MonoBehaviour {
    GameObject player;

    public GameObject big;
    public GameObject small;
    Transform L1;
    Transform L2;
    Transform L3;

    GameObject[] starsL1;
    GameObject[] starsL2;
    GameObject[] starsL3;

    public float boundsSize;
    public float bigSmallRatio;
    public int initialSpawnPerLayer;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");


        L1 = transform.Find("L1");
        SpawnStars(L1, starsL1, initialSpawnPerLayer);

        L2 = transform.Find("L2");
        SpawnStars(L2, starsL2, initialSpawnPerLayer * 4);

        L3 = transform.Find("L3");
        SpawnStars(L3, starsL3, initialSpawnPerLayer * 8);
    }

    void SpawnStars(Transform layer, GameObject[] container, int count) {
        Bounds bounds = new Bounds(player.transform.position, new Vector3(boundsSize * Camera.main.aspect, boundsSize));

        for (int i=0; i < count; i++) {
            GameObject spawn = Random.Range(0.0f, 1.0f) < bigSmallRatio ? big : small;
            Instantiate(spawn, GetPositionWithinBounds(bounds), Quaternion.identity, layer);
        }
    }

    Vector3 GetPositionWithinBounds(Bounds bounds) {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector3(x, y);
    }

    public void Respawn(GameObject original, Bounds bounds) {
        original.transform.position = GetPositionWithinBounds(bounds);
    }
}
