using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class AnimationBehavior : StateMachineBehaviour
{
    private Transform player;
    private ThirdPersonController motionScript;
    //private PlayerActions playerScript;
    private float movementSpeed;
    private float playerRotation;
    //private int animIDHealth;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        motionScript = player.GetComponents<ThirdPersonController>()[0];
        //playerScript = player.GetComponent<PlayerActions>();

       // Debug.Log("If this works you are awesome / mc isn't moving");

        movementSpeed = motionScript.MoveSpeed;
        playerRotation = motionScript.RotationSmoothTime;
       // animIDHealth = Animator.StringToHash("PlayerHealth");
        animator.ResetTrigger("Attack");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if (stateInfo.IsName("Starting Animation"))
            motionScript.MoveSpeed = 0;
        motionScript.RotationSmoothTime = 4.5f;

       // animator.SetFloat(animIDHealth, playerScript.getHealth());  // Recently added
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        motionScript.MoveSpeed = movementSpeed;
        motionScript.RotationSmoothTime = playerRotation;

        animator.ResetTrigger("Attack");
    }
}
