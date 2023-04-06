using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Boss status")]
    [SerializeField] private bool activited = false;
    [SerializeField] private bool isAliveCurrently = true; // wil be updated
    [SerializeField] private float healthBarNumber;

    [SerializeField] private float speed = 1;
    [SerializeField] private int waitTime = 10;
    [SerializeField] private int repeatTime = 20;
    [SerializeField] private float yAxisInput = 1;
    [SerializeField] private float xAxisInput = -1;

    private Coroutine coroutineHolder;

    [Header("Player GameObject goes here, needed for tracking")]
    public GameObject player;

    void Start()
    {       

    }

    private void LateUpdate()
    {
        if (activited)
            wakeUp();
    }

    private void OnCollisionEnter(Collision player)
    {
        //if player sword hitbox hits boss then lose health
        //if boss sword hitbox hits player then player lose health
    }

    public void wakeUp()
    {
        startMoving();
    }

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
    }

    public bool getActivitionState()
    {
        return activited;
    }

    public void setActivitionState()
    {
        activited = true;
    }
}
