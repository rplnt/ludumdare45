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

    public float boundsSize;
    public float bigSmallRatio;
    public int initialSpawnPerLayer;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");


        L1 = transform.Find("L1");
        SpawnStars(L1, initialSpawnPerLayer);

        L2 = transform.Find("L2");
        SpawnStars(L2, initialSpawnPerLayer * 5);

        L3 = transform.Find("L3");
        SpawnStars(L3, initialSpawnPerLayer * 10);
    }

    void SpawnStars(Transform layer, int count) {
        Bounds bounds = new Bounds(player.transform.position, new Vector3(boundsSize * Camera.main.aspect, boundsSize));

        for (int i=0; i < count; i++) {
            GameObject spawn = Random.Range(0.0f, 1.0f) < bigSmallRatio ? big : small;
            Instantiate(spawn, Consts.GetPositionWithinBounds(bounds), Quaternion.identity, layer);
        }
    }

    public void Respawn(GameObject original, Bounds bounds) {
        original.transform.position = Consts.GetPositionWithinBounds(bounds);
    }
}
