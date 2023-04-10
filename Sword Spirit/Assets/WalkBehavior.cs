using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
public class WalkBehavior : StateMachineBehaviour
{
    private Transform player;
    private ThirdPersonController motionScript;

    private float distanceBetween;

    private Transform boss;
    private BossController bossScript;
    private Rigidbody bossRigidbody;
    private bool working;
    private float speedOfLockOn;
    private float forceApplied;
    private float attackRange;
    private Vector3 directionToFace;
    private Quaternion desiredRotation;
    private Vector3 nextPosition;


    private float movementSpeed;
    private float playerRotation;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        motionScript = player.GetComponent<ThirdPersonController>();

        boss = GameObject.FindGameObjectWithTag("Boss").transform;
        bossScript = boss.GetChild(0).transform.GetComponent<BossController>();
        bossRigidbody = boss.GetComponent<Rigidbody>();

        working = bossScript.getActivitionState();
        Debug.Log(working); // yesssss

        speedOfLockOn = bossScript.lockOnSpeed;
        forceApplied = bossScript.movementSpeed;
        attackRange = bossScript.attackDistance;

        directionToFace = player.transform.position - boss.transform.position; // Vector that is created between two objects

        desiredRotation = Quaternion.LookRotation(directionToFace); // Using base object's rotation to find rotation needed to match direction to face     

        desiredRotation = Quaternion.Euler(0, desiredRotation.eulerAngles.y + 90f, 0); // Update desired rotation so the object only rotates in y axis to match direction, good in flat surfaces

        boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation, desiredRotation, speedOfLockOn * Time.deltaTime); // begun the actual process of rotating object


        distanceBetween = Vector3.Distance(player.transform.position, bossRigidbody.position);

        Debug.Log("distance between " + distanceBetween);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        directionToFace = player.transform.position - boss.transform.position; // Vector that is created between two objects

        desiredRotation = Quaternion.LookRotation(directionToFace); // Using base object's rotation to find rotation needed to match direction to face     

        desiredRotation = Quaternion.Euler(0, desiredRotation.eulerAngles.y + 90f, 0); // Update desired rotation so the object only rotates in y axis to match direction, good in flat surfaces

        boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation, desiredRotation, speedOfLockOn * Time.deltaTime); // begun the actual process of rotating object

        distanceBetween = Vector3.Distance(player.transform.position, bossRigidbody.position);

        Debug.Log("distance between " + distanceBetween);

        if (attackRange - .5f <= distanceBetween && distanceBetween <= attackRange + .5f) // when we are close enough to attack
        {
            animator.SetTrigger("Attack");
            animator.SetInteger("AttackType", Random.Range(0, 11));
        }

        else if(distanceBetween < attackRange - .5f) // then player is too damn close 
        {
            directionToFace.Normalize();
            nextPosition = -new Vector3(directionToFace.x, 0, directionToFace.z) * Time.deltaTime * forceApplied * 2.3f;  // the next position to move in unit vector, by a small increament neagtive
            bossRigidbody.AddForce(nextPosition);
        }

        else                                   // then player is too far away, keep approching him
        {
            directionToFace.Normalize();
            nextPosition = new Vector3(directionToFace.x, 0, directionToFace.z) * Time.deltaTime * forceApplied;  // the next position to move in unit vector, by a small increament positive
            bossRigidbody.AddForce(nextPosition);
        }

        boss.transform.position = new Vector3(boss.transform.position.x, 5.2f, boss.transform.position.z);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        directionToFace = player.transform.position - boss.transform.position; // Vector that is created between two objects

        desiredRotation = Quaternion.LookRotation(directionToFace); // Using base object's rotation to find rotation needed to match direction to face     

        desiredRotation = Quaternion.Euler(0, desiredRotation.eulerAngles.y + 90f, 0); // Update desired rotation so the object only rotates in y axis to match direction, good in flat surfaces

        boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation, desiredRotation, speedOfLockOn * Time.deltaTime); // begun the actual process of rotating object

        animator.ResetTrigger("Attack");
        animator.ResetTrigger("AttackAgain");
    }

    private void movement()
    {
        directionToFace = player.transform.position - boss.transform.position; // Vector that is created between two objects
        
        Debug.DrawRay(boss.transform.position, directionToFace, Color.blue);

        desiredRotation = Quaternion.LookRotation(directionToFace); // Using base object's rotation to find rotation needed to match direction to face     

        desiredRotation = Quaternion.Euler(0, desiredRotation.eulerAngles.y, 0); // Update desired rotation so the object only rotates in y axis to match direction, good in flat surfaces

        boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation, desiredRotation, speedOfLockOn * Time.deltaTime); // begun the actual process of rotating object

        directionToFace.Normalize();

        nextPosition = directionToFace * Time.deltaTime * forceApplied;

        boss.transform.Translate(nextPosition);
    }
}
