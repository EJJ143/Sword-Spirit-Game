using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private float xAxisInput;
    [SerializeField] private float zAxisInput;
    [SerializeField] private float speed = 30;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
      //  xAxisInput = Input.GetAxis("Horizontal");
        //zAxisInput = Input.GetAxis("Vertical");

        //transform.Translate(Vector3.forward * speed * xAxisInput * Time.deltaTime);
        //transform.Translate(Vector3.right * speed * zAxisInput * Time.deltaTime);
    }
}
