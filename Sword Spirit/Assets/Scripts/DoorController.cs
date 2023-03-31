using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script to determine when a door should open, when it should close, and in which direction to open 
 * that door*/
public class DoorController : MonoBehaviour 
{
    [Header("Amount to rotate by")]
    [SerializeField] private float rotationAmount = 90; 

    //[Header("To rotate the opposite way, add a negative sign")]
    //[SerializeField] private int directionOfRotation = 1;

    [SerializeField] private bool isRotatingDoor = true;
    [SerializeField] private float speedOfAnimation = .1f;
   // [SerializeField] private float numberOfDegreesToRotateBy = 1f;
    //[SerializeField] private float speedOfRotation = 9.0f;
  //  [SerializeField] private float currentAngle;
    [SerializeField] private float forwardDirection = 0f;

    private Vector3 startRotationN; // Stores the current rotation of object
    private Vector3 forward;  // Store axis that we want to compare with player position to

    private Coroutine animationCoroutine; // This store a reference to an open or close door method 

    public bool isOpen = false;  // Keeps track if door is open or closed

    private void Awake()
    {
        startRotationN = transform.rotation.eulerAngles; // Get the starting rotation of object
        forward = transform.forward;  // Get the door's axis vector of which way you want the door to open, our case when want z axis since it's perpeduclur to door
    }  // Activate when scene is created 

    //void Start()
    //{
    //    transform.Rotate(Vector3.up, directionOfRotation); // If we dont do this then, 
    //    currentAngle = transform.rotation.eulerAngles.y; // Initialize the current angle
    //}  // Start is called before the first frame update

    //void Update()
    //{
    //    currentAngle = transform.rotation.eulerAngles.y;  // Update the current angle

    //    if (directionOfRotation == 1)
    //        positiveRotation();
    //    else
    //        negativeRotation();
    //}  // Update is called once per frame

    public void open(Vector3 playerPosition)
    {
        if (!isOpen)
        {
            if (animationCoroutine != null) /* This is ment to catch an existing open or close methods that still exist, they could interfere with a new open or close method */
                StopCoroutine(animationCoroutine); // Stop that open or close method

            if (isRotatingDoor)
            {
                float dot = Vector3.Dot(forward, (playerPosition - transform.position).normalized); // So based on our chosen door vector we compare it to the vector created by the distance between the door and the player, but since every is actually in quaternions, we need to normalize the quaternion ever so often. Normalizing the quaternion means
                Debug.Log($"Dot: {dot.ToString("N3")}"); // I think it means get dot info to the three decimal place
                animationCoroutine = StartCoroutine(DoRotationOpen(dot)); // This starts the Method plus sets a reference to it 
            }  // Check to see if animation should be able to play on this door
        } // In order to open door, it must first be opened
    } // Checks before calling the actually open door method , can be seen by other scripts

    public void close() 
    {
        if(isOpen)
        {
            if (animationCoroutine != null) /* This is ment to catch an existing open or close methods that still exist, they could interfere with a new open or close method */ 
                StopCoroutine(animationCoroutine); // Stop any existing animation coroutine 
            if(isRotatingDoor)  // Check to see if animation should be able to play on this door
                animationCoroutine = StartCoroutine(DoRotationClose()); // This starts the Method plus sets a reference to it 
        } // In order to close the door it must first be opened
    } // Checks before calling actually close door method can be seen by other scripts

    private IEnumerator DoRotationOpen(float forwardAmount) 
    {
        Quaternion startRotation = transform.rotation;  // Grab the current rotation of game object, where we will begin to rotate from
        Quaternion endRotation; // Where we should stop the rotation, we get this from subtracting the start y rotation from the amount we will need to rotate 

        if(forwardAmount >= forwardDirection)  // Here scalar is positive, player is infront of our chosen axis, but the door must be opened away from player, not towards, so we must open door away from that axis
            endRotation = Quaternion.Euler(0, startRotationN.y - rotationAmount, 0); // Tells us which way to rotate the door around the y aixs. here n scaler is negative, so door can open away from player
        else                                 // Here scalar is nagative, player is behind the chosen axis, so door can be opened towards that axis
            endRotation = Quaternion.Euler(new Vector3(0, startRotationN.y + rotationAmount, 0)); // Get quaternion with wanted rotation 

        isOpen = true; // Now that we will begin to open the door, we dont want it interupted during the animation

        float time = 0;  // Start Time for animation 
        while(time < 0)  
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time); // Get the door to rotate via slerp from start rotation to end rotation
            yield return null; // Wait till next frame

            time += Time.deltaTime * speedOfAnimation; // Time by which the door should rotate in
        } // Loop updates the door's rotation to open it, can control with speed of animation
    }  // Open door method, forward amount refer to where the position of the play is in repect to axis we choose, in the form of a scalar from 1 t0 -1

    private IEnumerator DoRotationClose()  // Close door method
    {
        Quaternion tempStartRotation = transform.rotation;
        Quaternion tempEndRotation = Quaternion.Euler(startRotationN);

        isOpen = false;

        float time = 0;
        while(time < 1)
        {
            transform.rotation = Quaternion.Slerp(tempStartRotation, tempEndRotation, time);
            yield return null;

            time += Time.deltaTime * speedOfAnimation;
        } // Loop updates the door rotation, can control with speed of animation
    }
   
    //void positiveRotation()  // This method will rotate the object around y axis
    //{
    //    if (currentAngle <= rotationAmount)  // Checks if we rotated to our desired angle
    //        transform.Rotate(Vector3.up, directionOfRotation * numberOfDegreesToRotateBy * speedOfRotation * Time.deltaTime); // If not, keep rotating
    //}

    //void negativeRotation()
    //{
    //    if (rotationAmount <= currentAngle)  // Checks if we rotated to our desired angle
    //        transform.Rotate(Vector3.up, directionOfRotation * numberOfDegreesToRotateBy * speedOfRotation * Time.deltaTime); // If not, keep rotating
    //}
}
