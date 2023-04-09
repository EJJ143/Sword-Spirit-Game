using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("Player attributes")]
    [SerializeField] private float health = 100;  // The player's health duh
    [SerializeField] private bool hasSword = false;  // The player has picked up the sword in the room
    [SerializeField] private float maxUseDistance = 20f;  // The distance at which the player can interact with objects
 
    private Animator animator;  // The player animator 
    private bool hasAnimator;   // If the player has an animator
    private int animIDPressedAttack;  // id for parameters in the player's animator to update

    [Header("The layer that defines what objects the player can interact with")]
    public LayerMask useLayer;  // Which layer the things that the player can interact with, will exist in
    [Header("The text object that the player will call upon")]
    public TextMeshPro useText;  // The text we will be using to display messages live
    [Header("The main camera of the player")]
    public new Transform camera;  // The player's camera

    private void Start()
    {
        hasAnimator = TryGetComponent(out animator);  // If animator can be grabbed then assign it to our animator variable, and assign a bool to hasanimator

        if(hasAnimator)  
        {
            animIDPressedAttack = Animator.StringToHash("PressedAttack");  // Here we grab the animator parameter
        }

    }  // Things to do in the first frame
 
    void Update()
    {
        //animator.SetBool(animIDDoorActive, false); 

        // create ray from camera z axis that will return true if hit another collieder in a certian distance in the useable layer
        if (Physics.Raycast(camera.position, camera.forward, out RaycastHit hit, maxUseDistance, useLayer))
        {
            if (hit.collider.TryGetComponent<DoorController>(out DoorController doorScript))
            {
                if (doorScript.isOpen)
                    useText.SetText("Close Door [Right Shift]");  // Write this on the usetext
                else
                    useText.SetText("Open Door [Right Shift]");  // Wirte this on the usetext
                setRemainingText(hit);
             
            }  // If object we hit has the door controller script

            if(hit.collider.TryGetComponent<BossController>(out BossController bossScript))
            {
                Debug.Log("Looking at boss");

                if (!bossScript.getActivitionState())
                    useText.SetText("Interact [Right Shift]");
                else if (!bossScript.getBossStatus())
                    useText.SetText("Enemey has been purged");
                else
                    useText.SetText("Enemey is after you");
                setRemainingText(hit);

            }  // If the object we hit has the boss controller script

            Debug.DrawLine(camera.position, useText.transform.position, Color.red);
        }
        else
            useText.gameObject.SetActive(false);  // If we have hit anything then set any text to off
    }  // Check this every frame

    private void setRemainingText(RaycastHit hit)
    {
        useText.gameObject.SetActive(true);  // Activite the text so we can see it
        useText.transform.position = hit.point - (hit.point - camera.position).normalized * .04f + new Vector3(-1.9f, 0, 0);  // How far awy the text will be from the door ew .01
        useText.transform.rotation = Quaternion.LookRotation(hit.point - camera.position).normalized;  // Have the text move along with camera        

    }  // When true the displayed text will be prepared

    public void OnInteract()
    {  // create ray from camera z axis that will return true if hit another collieder in a certian distance in the useable layer
        if (Physics.Raycast(camera.position, camera.forward, out RaycastHit hit, maxUseDistance, useLayer))
        {
            if (hit.collider.TryGetComponent<DoorController>(out DoorController doorScript))
            {
                //animator.SetBool(animIDDoorActive, true);
                //Debug.Log("animation should play");

                if (doorScript.isOpen)
                    doorScript.close();
                else
                    doorScript.open(transform.position);

                //if (OnInteractWithDoor == null)
                //    OnInteractWithDoor(this, EventArgs.Empty);
                // To get the same result as bottom do
                // OnInteractWithDoor?.Invoke();

            } // If object we hit has the door controller script

            if (hit.collider.TryGetComponent<BossController>(out BossController bossScript) // If the object we hit has the boss controller script
                && bossScript.getBossStatus() && !bossScript.getActivitionState())
            {
                bossScript.setActivitionState(); // Set the activtied state to true, this will wake up the boss
            }   // and is alive and hasn't been activited then...
        } 
    } // With right shift button

    public void OnAttack()
    {
        Debug.Log("Attack!");
        animator.SetBool(animIDPressedAttack, true);  // Here we updated the PressedAtttack parameter in the animator

    }  // With right mose click button

    public void OnDodge()
    {
        Debug.Log("Moved around some");


    }  // With space button

    public void removeHealth()
    {
        health -= 10;
    }

    public void removeHealth(string typeOfAttack)
    {
        if (typeOfAttack.CompareTo("specialAttack") == 0)
            health -= 40;
    }
}
