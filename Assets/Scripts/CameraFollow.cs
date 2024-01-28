using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform follow;
    public BoxCollider2D bounds;


    private float xMin, xMax, yMin, yMax;
    private float camX, camY;
    private float camOrthSize;
    private float cameraRatio;
    private Camera cam;



    // Start is called before the first frame update
    void Start()
    {
        xMin = bounds.bounds.min.x;
        xMax = bounds.bounds.max.x;
        yMin = bounds.bounds.min.y;
        yMax = bounds.bounds.max.y;
        cam= GetComponent<Camera>();
        camOrthSize = cam.orthographicSize;
        cameraRatio = (xMax + camOrthSize) / 2.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        camY = Mathf.Clamp(follow.position.y, yMin + camOrthSize, yMax - camOrthSize);
        camX = Mathf.Clamp(follow.position.x, xMin + camOrthSize, xMax - camOrthSize);
        this.transform.position = new Vector3(camX, camY,this.transform.position.z);
    }
}
