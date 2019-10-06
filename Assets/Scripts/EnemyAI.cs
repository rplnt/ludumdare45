using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    float baseSpeed;

    private void Update() {
        float mag = (Core.Instance.transform.position - transform.position).magnitude;
        if (mag > 90.0f) return;
        float speed = 3.5f / mag;
        transform.position = Vector3.MoveTowards(transform.position, Core.Instance.transform.position, speed);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Environment")) return;
        if (collision.gameObject.CompareTag("Unit") || collision.gameObject.CompareTag("Player")) {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Environment")) return;
        if (collision.gameObject.CompareTag("Bullet")) {
            Die();
        }
    }

    void Die() {
        Destroy(gameObject);
    }
}