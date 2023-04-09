using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Boss status")]
    [SerializeField] private bool activited = false;
    [SerializeField] private bool alreadyActivited = false;
    [SerializeField] private bool isAliveCurrently = true; // wil be updated
    [SerializeField] private float healthBarNumber;
    [SerializeField] private float health = 250;

    [SerializeField] private float speed = 1;
    [SerializeField] private int waitTime = 10;
    [SerializeField] private int repeatTime = 20;
    [SerializeField] private float yAxisInput = 1;
    [SerializeField] private float xAxisInput = -1;

    private Coroutine coroutineHolder; // Will hold any currently executing method

    private Animator animator;   // Grab the parent animator
    private int animIDActivite;  // Save the variable avtivite here
    private int animIDDied;

    [Header("Player GameObject goes here, needed for tracking")]
    public GameObject player;

    //private PlayerActions playerScript;

    void Start()
    {
        // hasAnimator = TryGetComponent(out animator); Here is the old code to grab animator but only works if the component is in the parent
        //if (hasAnimator)
        //    animIDActivite = Animator.StringToHash("Activite");


        animator = GetComponentInParent<Animator>();  // Since animator is not in the helberd

        if (animator != null)                        // Make sure we can in fact find the animator in the parent
        {
            animIDActivite = Animator.StringToHash("Activite");  // Grab the id for the parameter activite in the general animator
            animIDDied = Animator.StringToHash("Died");
        }


    }  // Things to happen in the first frame

    private void Update()
    {
        if (activited && !alreadyActivited)  // Prevents us from waking the boss for than once
        {
            wakeUp();  // Method to start animations
            isAliveCurrently = true;

        }  // If The boss hasn't been activited then wake him up

        else if (activited && isAliveCurrently)  // Once he's awake and alive
        {
            startMoving();  // Method to move 

        }  // If the boss has been awakened and is still alive keep him fighting
        
        else if (!isAliveCurrently)  // If boss is no longer alive then...
            dying();  // Method to end Boss

    }  // Things to happen every frame

    private void OnCollisionEnter(Collision player)
    {
        //if player sword collider hits the boss's legs then boss loses health
        //    if (other.CompareTag("Player"))
        //        playerScript.removeHealth();

        //if boss halberd top collider hits the player's body then player lose health
        //    if (other.CompareTag("PlayerSword"))
        //        health -= 5;

    }  // Activites when the halberd bottom hits the player which is not what we want........

    private void wakeUp()
    {
        animator.SetBool(animIDActivite, true); // Starts the ready animation

    }  // This is what happens when boss is first awakened

    private void startMoving()
    {
        if (coroutineHolder != null)  //things to check before start moving
            StopCoroutine(coroutineHolder);

        coroutineHolder = StartCoroutine(moveBoss());
    }  //This method will check if move boss method is ready to be called

    IEnumerator moveBoss() //place holder for now
    {
        yield return new WaitForSeconds(waitTime); 
        
        int count = 0;
        while(count <= repeatTime)
        {   
            transform.Translate(Vector3.right * speed * xAxisInput * Time.deltaTime);
            
            count++;
            
            yield return null;
        }

        yield return new WaitForSeconds(2); 

        int count2 = 0;
        while (count2 <= repeatTime)
        {
            transform.Translate(Vector3.up * speed * yAxisInput * Time.deltaTime);
           
            count2++;

            yield return null;
        }
;
        Destroy(gameObject);
    }

    private void dying()
    {
        animator.SetBool(animIDDied, true);  // Start the dying animation

    }  // This is what happens after boss health reaches zero

    public bool getBossStatus()
    {
        return isAliveCurrently;  // Returns an int 

    }  // Get boss's health

    public bool getActivitionState()
    {
        return activited; // This is false by default

    }  // Get activited boolean

    public void setActivitionState()
    {
        activited = true;  // Updates activited to be only true

    }  // Set activited to true
}
