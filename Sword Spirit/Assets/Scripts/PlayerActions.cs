using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private TextMeshPro useText;
    [SerializeField] private new Transform camera;
    [SerializeField] private float maxUseDistance = 5f;
    [SerializeField] private LayerMask useLayer;

    public void OnInteract()
    {  // create ray from camera z axis that will return true if hit another collieder in a certian distance
        if (Physics.Raycast(camera.position, camera.forward, out RaycastHit hit, maxUseDistance, useLayer)) // will be true if ray created
            if (hit.collider.TryGetComponent<DoorController>(out DoorController doorScript))
            {
                if (doorScript.isOpen)
                    doorScript.close();
                else
                    doorScript.open(transform.position);

            } //if object we hit has the door controller script
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(camera.position, camera.forward, out RaycastHit hit, maxUseDistance, useLayer)
            && hit.collider.TryGetComponent<DoorController>(out DoorController doorscript))
        {
            if (doorscript.isOpen)
                useText.SetText("Close Door [Right Shift]");
            else
                useText.SetText("Open Door [Right Shift]");

            useText.gameObject.SetActive(true);
            useText.transform.position = hit.point - (hit.point - camera.position).normalized * 0.01f;
            useText.transform.rotation = Quaternion.LookRotation(hit.point - camera.position).normalized;
        }

        else
        {
            useText.gameObject.SetActive(false);
        }

    }
}
