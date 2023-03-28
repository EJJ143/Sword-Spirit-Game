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
    void Start()
    {
        StartCoroutine(moveBoss());       
    }

    IEnumerator moveBoss()
    {
        yield return new WaitForSeconds(waitTime); // wait
        
        int count = 0;
        while(count <= repeatTime)
        {   
            //transform.TransformDirection
            transform.Translate(Vector3.right * speed * xAxisInput * Time.deltaTime);
            
            Debug.Log("pos " + transform.position);
            Debug.Log("local pos " + transform.localPosition);

            count++;
            
            yield return null;
        }

        yield return new WaitForSeconds(2); // wait 

        int count2 = 0;
        while (count2 <= repeatTime)
        {
            transform.Translate(Vector3.up * speed * yAxisInput * Time.deltaTime);
           
            Debug.Log("pos " + transform.position);
            Debug.Log("local pos " + transform.localPosition);
            
            count2++;

            yield return null;
        }
;
        Destroy(gameObject);
    }
}
