using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private TextMeshPro useText;  // The text we will be using to display messages live
    [SerializeField] private new Transform camera;  // The player's camera
    [SerializeField] private float maxUseDistance = 12f;  // The distance at which the player can interact with objects
    [SerializeField] private LayerMask useLayer;  // Which layer the things that the player can interact with, will exist in

    void Update()
    {
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

            if(!hit.collider.TryGetComponent<BossController>(out BossController bossScript))
            {
                if (!bossScript.getActivitionState())
                    useText.SetText("Interact [Right Shift]");
                else if (!bossScript.getBossStatus())
                    useText.SetText("Enemey has been purged");
                else
                    useText.SetText("Enemey is after you");
                setRemainingText(hit);

            }  // If the object we hit has the boss controller script
        }
        else
            useText.gameObject.SetActive(false);  // If we have hit anything then set any text to off
    }  // Check this every frame

    private void setRemainingText(RaycastHit hit)
    {
        useText.gameObject.SetActive(true);  // Activite the text so we can see it
        useText.transform.position = hit.point - (hit.point - camera.position).normalized * 0.01f;  // How far awy the text will be from the door
        useText.transform.rotation = Quaternion.LookRotation(hit.point - camera.position).normalized;  // Have the text move along with camera
    }  // When true the displayed text will be prepared

    public void OnInteract()
    {  // create ray from camera z axis that will return true if hit another collieder in a certian distance in the useable layer
        if (Physics.Raycast(camera.position, camera.forward, out RaycastHit hit, maxUseDistance, useLayer))
        {
            if (hit.collider.TryGetComponent<DoorController>(out DoorController doorScript))
            {
                if (doorScript.isOpen)
                    doorScript.close();
                else
                    doorScript.open(transform.position);

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
        Debug.Log("HI");
    }  // With E button


}
