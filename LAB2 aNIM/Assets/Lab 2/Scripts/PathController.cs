using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PathController : MonoBehaviour
{
    [SerializeField]
    public PathManager pathManager;

    List<WayPoint> thePath;
    WayPoint target;

    public float MoveSpeed;
    public float RotateSpeed;

    public Animator animator;
    bool isWalking;

    private void Start()
    {
        thePath = pathManager.GetPath();
        if (thePath != null && thePath.Count > 0)
        {
            //set starting target to the first waypoint
            target = thePath[0];
        }
        isWalking = false;
        animator.SetBool("isWalking", isWalking);
    }
    private void rotateTowaardsTarget()
    {
        float stepSize = RotateSpeed * Time.deltaTime;

        Vector3 targetDir = target.pos - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, stepSize, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    void moveFoward()
    {
        float stepSize = Time.deltaTime * MoveSpeed;
        float distanceToTarget = Vector3.Distance(transform.position, target.pos);
        if (distanceToTarget < stepSize)
        {
            // we will over shoot the target
            // so we should do somthing smarter here 
            return;
        }
        //take a step forward
        Vector3 moveDir = Vector3.forward;
        transform.Translate(moveDir * stepSize);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            //toggle if any key is pressed 
            isWalking = !isWalking;
            animator.SetBool("isWalking", isWalking );
        }
        if (isWalking)
        {
            rotateTowaardsTarget();
            moveFoward();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // switch to next target 
        target = pathManager.GetNextTarget();
    }
}
