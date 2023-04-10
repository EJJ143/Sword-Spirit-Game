using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
public class WalkBehavior : StateMachineBehaviour
{
    private Transform player;
    private Transform boss;

    private ThirdPersonController motionScript;
    private BossController bossScript;
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
