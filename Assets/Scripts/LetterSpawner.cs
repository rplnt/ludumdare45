using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterSpawner : MonoBehaviour {
    public GameObject unit;
    public BoxCollider2D[] spawners;
    public int prespawn;


    public static Dictionary<string, int> ratios = new Dictionary<string, int>{
        {"A", 6},        {"B", 2},        {"C", 2},        {"D", 4},
        {"E", 6},        {"F", 2},        {"G", 3},        {"H", 2},
        {"I", 4},        {"J", 1},        {"K", 1},        {"L", 4},
        {"M", 2},        {"N", 4},        {"O", 4},        {"P", 2},
        {"Q", 1},        {"R", 4},        {"S", 4},        {"T", 4},
        {"U", 4},        {"V", 2},        {"W", 2},        {"X", 1},
        {"Y", 2},        {"Z", 1},
    };

    List<string> picker;

    float total = 0;

    private void Start() {
        picker = new List<string>();
        foreach (KeyValuePair<string, int> letter in ratios) {
            total += letter.Value;
            for(int i=0; i < letter.Value; i++) {
                picker.Add(letter.Key);
            }
        }

        for(int i=0; i<spawners.Length; i++) {
            SpawnRandomBounded(spawners[i].bounds);
            SpawnRandomBounded(spawners[i].bounds);
        }

        Bounds centerBounds = new Bounds(Core.Instance.transform.position, new Vector3(200 * Camera.main.aspect, 200));
        for (int i = 0; i < prespawn; i++) {
            SpawnRandomBounded(centerBounds);
        }
    }

    public void SpawnRandom() {
        SpawnRandomBounded(spawners[Random.Range(0, spawners.Length)].bounds);
    }

    public void SpawnRandomBounded(Bounds bounds) {
        Vector3 pos = Consts.GetPositionWithinBounds(bounds);

        if ((pos - transform.position).magnitude < 5.0f) return;

        Vector3 sizer = new Vector2(8, 8);
        Collider2D coll = Physics2D.OverlapArea(pos - sizer, pos + sizer);
        if (coll != null && !(coll.CompareTag("Environment") || coll.CompareTag("Respawn"))) return;

        int idx = Random.Range(0, picker.Count);
        string letter = picker[idx];

        GameObject u = Instantiate(unit, pos, Quaternion.identity, transform);
        u.name = letter;
        u.transform.Find("Letter").GetComponent<TextMeshPro>().text = letter;
    }
}