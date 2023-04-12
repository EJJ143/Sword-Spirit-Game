using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class swingBehavior : StateMachineBehaviour
{
    private Transform player;
    private PlayerActions playerScript;
    private float distanceBetween;

    private Transform boss;
    private BossController bossScript;
    private Rigidbody bossRigidbody;
    private MeshCollider bossSword;
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
        playerScript = player.GetComponent<PlayerActions>();

        boss = GameObject.FindGameObjectWithTag("Boss").transform;
        bossSword = boss.GetChild(1).GetComponent<MeshCollider>();  // grab the collider for the halberd
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
        Collider[] otherCollider = Physics.OverlapBox(bossSword.bounds.center, bossSword.bounds.extents, bossSword.transform.rotation, LayerMask.GetMask("Hit and Hurt boxes")); // grab everything that hits with collidier

        for (int x = 0; x < otherCollider.Length; x++)
            if (otherCollider[x].CompareTag("Player"))  //otherCollider[x].name.Equals("PlayerBody")
            {
                playerScript.removeHealth();
              //  Debug.Log(otherCollider[x].name);
                break;
            }

      //  Debug.Log("Player curret health " + playerScript.getHealth());

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
