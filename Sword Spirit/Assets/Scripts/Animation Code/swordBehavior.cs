using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordBehavior : StateMachineBehaviour
{
    private Transform sword;
    private BoxCollider hitBox;

    private Transform boss;
    private BossController bossScript;

    private audioController speaker;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        sword = GameObject.FindGameObjectWithTag("sword").transform;
        hitBox = sword.GetComponent<BoxCollider>();

        boss = GameObject.FindGameObjectWithTag("Boss").transform;
        bossScript = boss.GetChild(0).transform.GetComponent<BossController>();

        speaker = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<audioController>();

        if (animator.GetInteger("AttackType") > 5)
            speaker.attack1();
        else
            speaker.attack2();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Collider[] otherCollider = Physics.OverlapBox(hitBox.bounds.center, hitBox.bounds.extents, hitBox.transform.rotation, LayerMask.GetMask("Hit and Hurt boxes")); // grab everything that hits with collidier

        for (int x = 0; x < otherCollider.Length; x++)
            if (otherCollider[x].CompareTag("Boss hurtbox"))
            {
                bossScript.removeBossHealth();
                Debug.Log(otherCollider[x].name);
                Debug.Log("Boss's current health "+bossScript.getBossHealth());
                break;
            }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
