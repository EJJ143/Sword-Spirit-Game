using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private float xAxisInput;
    [SerializeField] private float yAxisInput;
    [SerializeField] private float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        xAxisInput = Input.GetAxis("Horizontal");
        yAxisInput = Input.GetAxis("vertical");

        transform.Translate(Vector3.right * speed * xAxisInput * Time.deltaTime);
        transform.Translate(Vector3.up * speed * yAxisInput * Time.deltaTime);
    }
}
