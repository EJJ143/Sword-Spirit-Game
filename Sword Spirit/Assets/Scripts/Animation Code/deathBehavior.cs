using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class deathBehavior : StateMachineBehaviour
{
    private audioController speaker;
    private PlayerActions player;
    private ThirdPersonController motionScript;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int animIDDeathType = Animator.StringToHash("DeathType");

        animator.SetInteger(animIDDeathType, 0);

        speaker = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<audioController>();
        player = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerActions>();
        motionScript = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<ThirdPersonController>();
        speaker.died();

        motionScript.MoveSpeed = 0;
        motionScript.RotationSmoothTime = 0;

        //Debug.Log("If this works you are awesome / mc isn't moving");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        motionScript.MoveSpeed = 0;
        motionScript.RotationSmoothTime = 0;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        motionScript.MoveSpeed = 0;
        motionScript.RotationSmoothTime = 0;
        player.restartGame();
    }
}
