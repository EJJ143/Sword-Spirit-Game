using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
public class WalkBehavior : StateMachineBehaviour
{
    private Transform player;
    private ThirdPersonController motionScript;

    private Transform boss;
    private BossController bossScript;
    private float speedOfLockOn;
    private float speedTowardTarget;
    private Vector3 directionToFace;
    private Quaternion desiredRotation;
    private Vector3 nextPosition;
    private bool working;

    private float movementSpeed;
    private float playerRotation;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        motionScript = player.GetComponent<ThirdPersonController>();

        boss = GameObject.FindGameObjectWithTag("Boss").transform;
        bossScript = boss.GetChild(0).transform.GetComponent<BossController>();

        working = bossScript.getActivitionState();
        Debug.Log(working); // yesssss

        speedOfLockOn = bossScript.lockOnSpeed;
        speedTowardTarget = bossScript.movementSpeed;

        movement();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
     
    }

    private void movement()
    {
        directionToFace = player.transform.position - boss.transform.position; // Vector that is created between two objects
        
        Debug.DrawRay(boss.transform.position, directionToFace, Color.blue);

        desiredRotation = Quaternion.LookRotation(directionToFace); // Using base object's rotation to find rotation needed to match direction to face     

        desiredRotation = Quaternion.Euler(0, desiredRotation.eulerAngles.y, 0); // Update desired rotation so the object only rotates in y axis to match direction, good in flat surfaces

        boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation, desiredRotation, speedOfLockOn * Time.deltaTime); // begun the actual process of rotating object

        directionToFace.Normalize();

        nextPosition = directionToFace * Time.deltaTime * speedTowardTarget;

        boss.transform.Translate(nextPosition);

     
    }

}
