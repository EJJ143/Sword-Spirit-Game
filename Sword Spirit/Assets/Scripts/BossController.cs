using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private float yAxisInput = 1;
    [SerializeField] private float xAxisInput = -1;
    [SerializeField] private float speed = 1;
    [SerializeField] private int waitTime = 10;
    [SerializeField] private int repeatTime = 20;
    [SerializeField] private float healthBarNumber;

    private Coroutine coroutineHolder;

    public bool isAliveCurrent = true;

    [Header("Player GameObject goes here, needed for tracking")]
    public GameObject player;

    void Start()
    {       
    }

      private void OnCollisionEnter(Collision player)
    {
        //if player sword hitbox hits boss then lose health
        //if boss sword hitbox hits player then player lose health
    }

    public void wakeUp()
    {
        //This method will set things up
        startMoving();
    }

    private void startMoving()
    {
        //things to check before start moving

        if (coroutineHolder != null)
            StopCoroutine(coroutineHolder);

        coroutineHolder = StartCoroutine(moveBoss());
    }

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
}
