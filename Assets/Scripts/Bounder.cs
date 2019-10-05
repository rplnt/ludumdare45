using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounder : MonoBehaviour {
    Starfield sf;
    public BoxCollider2D opposite;

    private void Start() {
        sf = FindObjectOfType<Starfield>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        sf.Respawn(collision.gameObject, opposite.bounds);
    }
}
