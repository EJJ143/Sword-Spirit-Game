using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryBehavior : StateMachineBehaviour
{
    private AudioSource playerCam;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(Animator.StringToHash("extra"), false);
        AudioSource playerCam = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<AudioSource>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerCam.volume = 0.03f;
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerCam.volume = 0;
    }

}
