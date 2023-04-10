using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class DodgeBehavior : StateMachineBehaviour
{
    private Transform player;

    private ThirdPersonController motionScript;
    private float movementSpeed;
    private float playerRotation;

    private CharacterController characterController;
    private float radius;
    private float height;
    private float ycenter;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        motionScript = player.GetComponent<ThirdPersonController>();
        characterController = player.GetComponent<CharacterController>();

        Debug.Log("If this works you are awesome and the mc is dodging");

        movementSpeed = motionScript.MoveSpeed;
        playerRotation = motionScript.RotationSmoothTime;

        radius = characterController.radius;
        height = characterController.height;
        ycenter = characterController.center.y;
        motionScript.resetDodgeTimerDelta();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        motionScript.MoveSpeed = movementSpeed + 20;

        characterController.radius = .3f;
        characterController.height = 1;
        characterController.center = new Vector3(0, .6f, 0);
        motionScript.resetDodgeTimerDelta();

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        motionScript.MoveSpeed = movementSpeed;

        characterController.radius = radius;
        characterController.height = height;
        characterController.center = new Vector3(0, ycenter, 0);

        motionScript.resetDodgeTimerDelta();
    }
}
