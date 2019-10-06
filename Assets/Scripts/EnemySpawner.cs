using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemy;
    public BoxCollider2D[] spawners;
    public int prespawn;

    List<string> picker;

    float lastSpawn;
    public float spawnDelay;

    string[] enemies = { "@", "#", "%", "$" };

    private void Start() {
        for(int i=0; i<spawners.Length; i++) {
            SpawnEnemy(spawners[i].bounds);
            SpawnEnemy(spawners[i].bounds);
        }

        Bounds centerBounds = new Bounds(Core.Instance.transform.position, new Vector3(200 * Camera.main.aspect, 200));
        for (int i = 0; i < prespawn; i++) {
            SpawnEnemy(centerBounds);
        }
    }

    public void Update() {
        if (Time.time < lastSpawn + spawnDelay) return;
        Bounds centerBounds = new Bounds(Core.Instance.transform.position, new Vector3(200 * Camera.main.aspect, 200));
        SpawnEnemy(centerBounds);
    }

    public void SpawnEnemy(Bounds bounds) {
        Vector3 pos = Consts.GetPositionWithinBounds(bounds);

        if ((pos - transform.position).magnitude < 50.0f) return;

        Vector3 sizer = new Vector2(10, 10);
        Collider2D coll = Physics2D.OverlapArea(pos - sizer, pos + sizer);
        if (coll != null && !(coll.CompareTag("Environment") || coll.CompareTag("Respawn"))) return;

        GameObject u = Instantiate(enemy, pos, Quaternion.identity, transform);
        u.transform.GetComponentInChildren<TextMeshPro>().text = enemies[Random.Range(0, enemies.Length)];
        lastSpawn = Time.time;
    }

}