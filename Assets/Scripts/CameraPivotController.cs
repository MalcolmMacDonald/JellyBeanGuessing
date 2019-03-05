using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivotController : MonoBehaviour
{



    float horizontalAngle;
    float horizontalVelocity;
    float horizontalDestinationVelocity;




    public float maxDragVelocity;
    public float dragStrength;
    public float velocityChangeRate;
    public float decayRate;
    public float maxVelocity;


    Vector3 startingRotation;

    Vector2 previousMousePos;

    public AnimationCurve dragStrengthCurve;
    public AnimationCurve decayRateCurve;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        startingRotation = transform.rotation.eulerAngles;
        previousMousePos = (Vector2)Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            float dragVelocity = ((Vector2)Input.mousePosition - previousMousePos).x;
            horizontalDestinationVelocity += dragStrengthCurve.Evaluate(Mathf.Clamp01(Mathf.Abs(dragVelocity) / maxDragVelocity)) * dragStrength * Mathf.Sign(dragVelocity);
        }


        float normalizedVelocity = Mathf.Clamp01(Mathf.Abs(horizontalDestinationVelocity) / maxVelocity);

        if (horizontalDestinationVelocity > 0)
        {
            horizontalDestinationVelocity = Mathf.Clamp(horizontalDestinationVelocity - decayRate * Time.deltaTime * decayRateCurve.Evaluate(normalizedVelocity), 0, horizontalDestinationVelocity);
        }
        else
        {
            horizontalDestinationVelocity = Mathf.Clamp(horizontalDestinationVelocity + decayRate * Time.deltaTime * decayRateCurve.Evaluate(normalizedVelocity), horizontalDestinationVelocity, 0);
        }


        horizontalDestinationVelocity = Mathf.Sign(horizontalDestinationVelocity) * Mathf.Clamp(Mathf.Abs(horizontalDestinationVelocity), 0, maxVelocity);

        horizontalVelocity = Mathf.MoveTowards(horizontalVelocity, horizontalDestinationVelocity, Time.deltaTime * velocityChangeRate);

        horizontalAngle += horizontalVelocity * Time.deltaTime;

        transform.rotation = Quaternion.Euler(startingRotation.x, horizontalAngle, startingRotation.z);
        previousMousePos = (Vector2)Input.mousePosition;

    }

    void HorizontalMovement()
    {

    }

}
