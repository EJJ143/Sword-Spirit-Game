using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private int directionOfRotation = 1;
    [SerializeField] private float numberOfDegreesToRotateBy = 1;
    [SerializeField] private float speedOfRotation = 9.0f;
    [SerializeField] private float stopAngle = 90;
    [SerializeField] private float currentAngle;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(Vector3.up, directionOfRotation); // If we dont do this then, 
        currentAngle = transform.rotation.eulerAngles.y; // Initialize the current angle
        Debug.Log("local "+transform.localPosition);
        Debug.Log("world " + transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        currentAngle = transform.rotation.eulerAngles.y;  // Update the current angle

        if (directionOfRotation == 1)
            positiveRotation();
        else
            negativeRotation();
    }

    void positiveRotation()  // This method will rotate the object around y axis
    {
        if (currentAngle <= stopAngle)  // Checks if we rotated to our desired angle
            transform.Rotate(Vector3.up, directionOfRotation * numberOfDegreesToRotateBy * speedOfRotation * Time.deltaTime); // If not, keep rotating
    }

    void negativeRotation()
    {
        if (stopAngle <= currentAngle)  // Checks if we rotated to our desired angle
            transform.Rotate(Vector3.up, directionOfRotation * numberOfDegreesToRotateBy * speedOfRotation * Time.deltaTime); // If not, keep rotating
    }
}
