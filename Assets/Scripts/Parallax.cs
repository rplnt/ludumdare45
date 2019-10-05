using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
    public float speed;
    Vector3 lastPos;

    private void Update() {
        Transform cam = Camera.main.transform;
        transform.position -= ((lastPos - cam.position) * speed);
        lastPos = cam.position;
    }
}
