using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bullet : MonoBehaviour {
    TextMeshPro area;
    public float duration;
    public float size;

    private void Start() {
        area = GetComponentInChildren<TextMeshPro>();
        StartCoroutine(Expand());
    }

    public IEnumerator Expand() {
        float elapsed = 0.0f;
        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            area.color = new Color(1, 1, 1, Mathf.Clamp01(1.25f - elapsed / duration));
            float scale = Mathf.Lerp(1.0f, size, elapsed / duration);
            transform.localScale = new Vector3(scale, scale);
            yield return null;
        }
        Destroy(gameObject);
    }
}