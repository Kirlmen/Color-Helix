using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float cameraZ;

    // Update is called once per frame
    void Update()
    {
        cameraZ = BallHandler.GetZ() - 2.95f;
        transform.position = new Vector3(0, 2.2f, cameraZ);
    }
}
