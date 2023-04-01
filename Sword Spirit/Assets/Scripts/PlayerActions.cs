using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private TextMeshPro useText;
    [SerializeField] private new Transform camera;
    [SerializeField] private float maxUseDistance = 12f;
    [SerializeField] private LayerMask useLayer;
    [SerializeField] private bool hasntBeenActivited = true;
  
    public void OnInteract()
    {  // create ray from camera z axis that will return true if hit another collieder in a certian distance
        if (Physics.Raycast(camera.position, camera.forward, out RaycastHit hit, maxUseDistance, useLayer)) // will be true if ray created
        {
            if (hit.collider.TryGetComponent<DoorController>(out DoorController doorScript))
            {
                if (doorScript.isOpen)
                    doorScript.close();
                else
                    doorScript.open(transform.position);

            } //if object we hit has the door controller script

            if (hit.collider.TryGetComponent<BossController>(out BossController bossScript) && bossScript.isAliveCurrent)
            {
                bossScript.wakeUp();
                hasntBeenActivited = false;
            }
        }
    } // With right shift button

    public void OnAttack()
    {
        Debug.Log("HI");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if(Physics.Raycast(camera.position, camera.forward, out RaycastHit hit, maxUseDistance, useLayer))
        {
            if (hit.collider.TryGetComponent<DoorController>(out DoorController doorScript))
            {
                if (doorScript.isOpen)
                    useText.SetText("Close Door [Right Shift]");
                else
                    useText.SetText("Open Door [Right Shift]");

                useText.gameObject.SetActive(true);
                useText.transform.position = hit.point - (hit.point - camera.position).normalized * 0.01f;
                useText.transform.rotation = Quaternion.LookRotation(hit.point - camera.position).normalized;
            }

            if(hit.collider.TryGetComponent<BossController>(out BossController bossScript))
            {
                if (bossScript.isAliveCurrent && hasntBeenActivited) // once interacted with 'has been activited' is set to false 
                    useText.SetText("Interact [Right Shift]");
                else
                    useText.SetText("Enemey has been purged");

                useText.gameObject.SetActive(true);
                useText.transform.position = hit.point - (hit.point - camera.position).normalized * 0.01f;
                useText.transform.rotation = Quaternion.LookRotation(hit.point - camera.position).normalized;
            }
        }
        else
        {
            useText.gameObject.SetActive(false);
        }
    }
}
