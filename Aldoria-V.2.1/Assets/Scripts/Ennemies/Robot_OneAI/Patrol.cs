using BehaviourTree;

using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Patrol : Node
{

    Transform transform;
    RobotBT bt;
    PatrolZone patrolZoneScript;

    float goalX;
    float goalZ;

    float currentTime;

    public Patrol(Transform _transform, RobotBT _bt)
    {
        transform = _transform;
        bt = _bt;
        patrolZoneScript = bt.zone.GetComponent<PatrolZone>();

        SelectPointToGo();
    }

    public override NodeState Evaluate()
    {
        //Timer between patrols
        if(bt.isWaiting && currentTime + bt.timeBetweenPatrols <= Time.time)
        {
            bt.isWaiting = false;
            SelectPointToGo();
        }

        //Detection if the enemy has reached goal point
        if (!bt.agent.pathPending && bt.agent.remainingDistance <= 0.5f && !bt.isWaiting)
        {
            Debug.Log("Wait for next patrol");
            bt.isWaiting = true;
            currentTime = Time.time;
            bt.animator.SetBool("isWalking", false);

        }
        
        
        state = NodeState.RUNNING;
        return state;
    }

    //Fonction to choose a point to go for the patrol
    //Randon in a zone defined
    private void SelectPointToGo()
    {
        goalX = UnityEngine.Random.Range(bt.zone.transform.position.x - patrolZoneScript.zoneSize.x / 2, bt.zone.transform.position.x + patrolZoneScript.zoneSize.x / 2);
        goalZ = UnityEngine.Random.Range(bt.zone.transform.position.z - patrolZoneScript.zoneSize.z / 2, bt.zone.transform.position.z + patrolZoneScript.zoneSize.z / 2);

        Collider[] collidersPoint = Physics.OverlapSphere(new Vector3(goalX, transform.position.y + 3, goalZ), 1f);

        if(collidersPoint.Count() != 0)
        {
            SelectPointToGo();
            return;
        }
        else
        {
            PerformPatrolMovement();
        }
    }


    //Perform the movement
    private void PerformPatrolMovement()
    {
        bt.animator.SetBool("isWalking", true);
        bt.agent.SetDestination(new Vector3(goalX, transform.position.y, goalZ));
    }


}
