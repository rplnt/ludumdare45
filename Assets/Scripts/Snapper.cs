using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapper : MonoBehaviour {

    Snapper[] adjacent;
    LetterSpawner ls;

    private void Start() {
        adjacent = new Snapper[4];
        ls = FindObjectOfType<LetterSpawner>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        //Debug.Log("Snapper:OnCollisionEnter2D");
        if (collision.gameObject.CompareTag("Unit") && collision.gameObject.GetComponent<Snapper>() == null) {
            bool respawn = SnapOnApplyDirectlyToTheForehead(collision.gameObject);
            if (respawn) {
                ls.SpawnRandom();
            }
        }

        if (collision.gameObject.CompareTag("Enemy")) {
            ContactPoint2D contact = collision.GetContact(0);
            if (contact.otherCollider.gameObject == gameObject) {
                Debug.Log("Killing " + gameObject.name);
                Die();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //Debug.Log("Snapper:OnTriggerEnter2D");
    }

    bool SnapOnApplyDirectlyToTheForehead(GameObject gameObject) {
        Vector3 dir;
        bool snap = GetSnappingDirection(gameObject.transform, out dir);
        if (!snap) return false;

        gameObject.transform.position += dir;

        gameObject.transform.SetParent(Core.Instance.transform);
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Snapper snapper = gameObject.AddComponent<Snapper>();

        Consts.Dir snapDir = Consts.GetDir(dir);
        //Debug.Log(transform.name + " " + snapDir);

        Core.Instance.RegisterUnit(gameObject);
        return true;
    }

    bool GetSnappingDirection(Transform unit, out Vector3 dir) {
        Vector3 distance = unit.position - transform.position;

        float ratio = Mathf.Abs(distance.x) + Mathf.Abs(distance.y);
        if (ratio > 7.0f) {
            dir = Vector3.zero;
            return false;
        }

        if (Mathf.Abs(distance.x) < Mathf.Abs(distance.y)) {
            dir = new Vector3(-distance.x, 0, 0);
        } else {
            dir = new Vector3(0, -distance.y, 0);
        }

        return true;
    }

    void Die() {
        bool destroy = GetComponent<Core>() == null;
        if (destroy) {
            Core.Instance.RemoveUnit(gameObject);
        } else {
            Core.Instance.GameOver();
        }
        StartCoroutine(Explode(0.3f, destroy));
    }

    public IEnumerator Explode(float duration, bool destroy) {
        float elapsed = 0.0f;
        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            float scale = Mathf.Lerp(1.0f, 2.0f, elapsed / duration);
            transform.localScale = new Vector3(scale, scale);
            yield return null;
        }
        if (destroy) Destroy(gameObject);
    }
 }
