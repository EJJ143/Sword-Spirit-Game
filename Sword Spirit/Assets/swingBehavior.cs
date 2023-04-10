using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class swingBehavior : StateMachineBehaviour
{
    private Transform player;
    private float distanceBetween;

    private Transform boss;
    private BossController bossScript;
    private Rigidbody bossRigidbody;
    private float speedOfLockOn;
    private float forceApplied;
    private float attackRange;
    private Vector3 directionToFace;
    private Quaternion desiredRotation;
    private Vector3 nextPosition;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        boss = GameObject.FindGameObjectWithTag("Boss").transform;
        bossScript = boss.GetChild(0).transform.GetComponent<BossController>();
        bossRigidbody = boss.GetComponent<Rigidbody>();

        speedOfLockOn = bossScript.lockOnSpeed;
        forceApplied = bossScript.movementSpeed;
        attackRange = bossScript.attackDistance;

        directionToFace = player.transform.position - boss.transform.position; // Vector that is created between two objects

        desiredRotation = Quaternion.LookRotation(directionToFace); // Using base object's rotation to find rotation needed to match direction to face     

        desiredRotation = Quaternion.Euler(0, desiredRotation.eulerAngles.y + 90f, 0); // Update desired rotation so the object only rotates in y axis to match direction, good in flat surfaces

        boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation, desiredRotation, speedOfLockOn* 3f * Time.deltaTime); // begun the actual process of rotating object
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        directionToFace = player.transform.position - boss.transform.position; // Vector that is created between two objects

        desiredRotation = Quaternion.LookRotation(directionToFace); // Using base object's rotation to find rotation needed to match direction to face     

        desiredRotation = Quaternion.Euler(0, desiredRotation.eulerAngles.y + 90f, 0); // Update desired rotation so the object only rotates in y axis to match direction, good in flat surfaces

        boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation, desiredRotation, speedOfLockOn * 4f * Time.deltaTime); // begun the actual process of rotating object
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        directionToFace = player.transform.position - boss.transform.position; // Vector that is created between two objects

        desiredRotation = Quaternion.LookRotation(directionToFace); // Using base object's rotation to find rotation needed to match direction to face     

        desiredRotation = Quaternion.Euler(0, desiredRotation.eulerAngles.y + 90f, 0); // Update desired rotation so the object only rotates in y axis to match direction, good in flat surfaces

        boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation, desiredRotation, speedOfLockOn * 3f * Time.deltaTime); // begun the actual process of rotating object

        bossRigidbody.AddForce(nextPosition);

        distanceBetween = Vector3.Distance(player.transform.position, bossRigidbody.position);

        if (attackRange - .5f <= distanceBetween && distanceBetween <= attackRange + .5f) // when we are close enough to attack
            animator.SetTrigger("AttackAgain");
        //else
        //    animator.ResetTrigger("Attack");
    }

}
