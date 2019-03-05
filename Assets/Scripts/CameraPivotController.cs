using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivotController : MonoBehaviour
{



    float horizontalAngle;

    public int framesForCircle;


    public delegate void OnComplete();

    public OnComplete onComplete;

    int frameCount;
    Vector3 startRotation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        startRotation = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {

        frameCount++;

        horizontalAngle = ((float)frameCount / framesForCircle) * 360f;

        transform.rotation = Quaternion.Euler(startRotation.x, startRotation.y + horizontalAngle, startRotation.z);

        if (frameCount >= framesForCircle)
        {
            frameCount = 0;
            if (onComplete != null)
            {
                onComplete();
            }
        }

    }

}
