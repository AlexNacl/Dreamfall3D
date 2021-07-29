using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject target;
    public Transform targetTranform;
    public bool useOffset;
    public float rotateSpeed;

    public Vector3 cameraOffset;

    void Start()
    {
        target = GameObject.Find("Player");
        targetTranform = target.transform;
        if (!useOffset)
        {
            cameraOffset = targetTranform.position - this.transform.position;
        }
    }


    void Update()
    {
        // float targetHorizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        // targetTranform.Rotate(0, targetHorizontal, 0);

        // float targetVertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        // targetTranform.Rotate(targetVertical, 0, 0);

        // float cameraYAngle = targetTranform.eulerAngles.y;
        // float cameraXAngle = targetTranform.eulerAngles.x;

        // Quaternion rotation = Quaternion.Euler(cameraXAngle, cameraYAngle, 0);
        // this.transform.position = targetTranform.position - (rotation * cameraOffset);

        // transform.LookAt(targetTranform);
    }
}
