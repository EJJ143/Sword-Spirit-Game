using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathBehavior : StateMachineBehaviour
{
    private audioController speaker;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int animIDDeathType = Animator.StringToHash("DeathType");

        animator.SetInteger(animIDDeathType, 0);

        speaker = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<audioController>();

        speaker.died();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
