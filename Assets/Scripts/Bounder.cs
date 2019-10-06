using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounder : MonoBehaviour {
    Starfield sf;
    LetterSpawner ls;
    public BoxCollider2D opposite;

    private void Start() {
        sf = FindObjectOfType<Starfield>();
        ls = FindObjectOfType<LetterSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Environment")) {
            sf.Respawn(collision.gameObject, opposite.bounds);
        } else if (collision.gameObject.CompareTag("Unit")) {
            Destroy(collision.gameObject);
            ls.SpawnRandomBounded(opposite.bounds);
        } else if (collision.gameObject.CompareTag("Enemy")) {
            Vector3 pos = Consts.GetPositionWithinBounds(opposite.bounds);
            collision.transform.position = pos;
        } else {
            Debug.LogError("Unknown trigger");
        }
    }

}
