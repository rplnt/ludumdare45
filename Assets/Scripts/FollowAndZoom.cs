using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAndZoom : MonoBehaviour {
    public Transform player;
    public float smoothness;
    Vector3 speed = Vector3.zero;

    private void LateUpdate() {
        float x = player.position.x;
        float y = player.position.y;

        Vector3 pos = new Vector3(x, y, transform.position.z);
        //transform.position = pos;
        //transform.position = Vector3.SmoothDamp(transform.position, pos, ref speed, smoothness);
        transform.position = Vector3.Lerp(transform.position, pos, smoothness);

        // zoom out based on size
        // TODO
    }
}
