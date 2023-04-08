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

    [SerializeField] private float speed = 1;
    [SerializeField] private int waitTime = 10;
    [SerializeField] private int repeatTime = 20;
    [SerializeField] private float yAxisInput = 1;
    [SerializeField] private float xAxisInput = -1;

    private Coroutine coroutineHolder;
    private Animator animator;
    private bool hasAnimator;
    private int animIDActivite;

    [Header("Player GameObject goes here, needed for tracking")]
    public GameObject player;
    private PlayerActions playerScript;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerScript.removeHealth();

    }

    void Start()
    {
        playerScript = GetComponent<PlayerActions>();

        hasAnimator = TryGetComponent(out animator);

        if (hasAnimator)
            animIDActivite = Animator.StringToHash("Activite");
    }

    private void Update()
    {
        if (activited && !alreadyActivited)
        {
            wakeUp();
            isAliveCurrently = true;
        }  // If The boss hasn't been activited then wake him up
        else if (activited && isAliveCurrently)  // If the boss has been awakened and is still alive keep him fighting
            startMoving();
        else if (!isAliveCurrently)  // If boss is no longer alive then...
            sleep();
    }

    private void OnCollisionEnter(Collision player)
    {
        //if player sword hitbox hits boss then lose health
        //if boss sword hitbox hits player then player lose health
    }

    private void wakeUp()
    {
        animator.SetBool(animIDActivite, true);
    }  // This is what happens when boss is first awakened

    private void sleep()
    {

    }  // This is what happens after boss is defeated

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

    public bool getBossStatus()
    {
        return isAliveCurrently;
    }  // Get if boss is still alive

    public bool getActivitionState()
    {
        return activited;
    }  // Get if boss is activited

    public void setActivitionState()
    {
        activited = true;
    }  // Activte the boss
}
