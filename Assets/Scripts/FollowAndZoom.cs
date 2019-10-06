using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAndZoom : MonoBehaviour {
    public Transform player;
    public float smoothness;
    Vector3 speed = Vector3.zero;
    float baseSize;
    float targetSize;

    private void Start() {
        baseSize = Camera.main.orthographicSize;
        targetSize = baseSize;
        Core.Instance.Collected += UpdateZoom;
        UpdateZoom(0, 0);
    }

    private void LateUpdate() {
        float x = player.position.x;
        float y = player.position.y;

        Vector3 pos = new Vector3(x, y, transform.position.z);
        transform.position = pos;
        //transform.position = Vector3.SmoothDamp(transform.position, pos, ref speed, smoothness);
        //transform.position = Vector3.Lerp(transform.position, pos, smoothness);

        //Debug.Log(targetSize);
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetSize, 0.025f);
    }

    void UpdateZoom(int score, int size) {
        var span = Core.Instance.GetSpan();
        var add = span.max.x > (span.max.y * Camera.main.aspect) ? span.max.x : span.max.y;
        float newSize = baseSize + add;
        targetSize = Mathf.Max(baseSize, Mathf.Min(60.0f, newSize));
    }
}
