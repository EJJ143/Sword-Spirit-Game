using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Amount to rotate by")]
    [SerializeField] private float stopAngle = 90;

    [Header("To rotate the opposite way, add a negative sign")]
    [SerializeField] private int directionOfRotation = 1;

    [SerializeField] private float numberOfDegreesToRotateBy = 1;
    [SerializeField] private float speedOfRotation = 9.0f;
    [SerializeField] private float currentAngle;
    [SerializeField] private float forwardDirection = 0;

    private Vector3 startRotation;
    private Vector3 forward;

    private Coroutine animationCoroutine;

    public bool IsOpen = false;

    private void Awake() // Activate when scene is created 
    {
        startRotation = transform.rotation.eulerAngles; // Gives starting rotation
        forward = transform.forward;  // Get the door's axis vector of which way you want the door to open, our case when want z axis but negative
    }


    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(Vector3.up, directionOfRotation); // If we dont do this then, 
        currentAngle = transform.rotation.eulerAngles.y; // Initialize the current angle
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

    public void open(Vector3 playerPosition)
    {
        if (!IsOpen) // Basically if door IS NOT open, then
        {
            if (animationCoroutine != null) // And if animationCorountine hasn't been assigned yet, then
                StopCoroutine(animationCoroutine); // Stop animation 

      //      if (IsRotatingDoor)
            {
                float dot = Vector3.Dot(forward, (playerPosition - transform.position).normalized);
                Debug.Log($"Dot: {dot.ToString("N3")}"); // I think it means get dot info to the three decimal place
        //        animationCoroutine = StartCoroutine(DoRotationOpen(dot));
            }
        }


    }

    //private IEnumerator DoRotationOpen()
   
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
