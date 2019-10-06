using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {
    public GameObject bullet;
    public float cooldown;
    float lastFiredAt = 0.0f;

    public void Fire() {
        if (Time.time < lastFiredAt + cooldown) return;
        lastFiredAt = Time.time;
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}
