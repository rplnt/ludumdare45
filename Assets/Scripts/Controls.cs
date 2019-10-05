using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {
    public float speed;
    Rigidbody2D rb;


    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Move();
    }

    void Move() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 mov = new Vector2(h, v);
        rb.AddForce(mov * speed);
    }
}


