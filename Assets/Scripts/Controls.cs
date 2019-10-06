using System;
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
        Shoot();
    }

    void Move() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 mov = new Vector2(h, v);
        rb.AddForce(mov * speed);
    }

    void Shoot() {
        foreach (KeyCode key in Enum.GetValues(typeof(KeyCode))) {
            if (key < KeyCode.A || key > KeyCode.Z) continue;
            if (Input.GetKeyDown(key)) {
                Core.Instance.Fire(key.ToString());
            }
        }
    }
}


