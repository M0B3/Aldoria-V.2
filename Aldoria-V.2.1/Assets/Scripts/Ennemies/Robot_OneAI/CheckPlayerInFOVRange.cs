using BehaviourTree;

using UnityEngine;


public class CheckPlayerInFOVRange : Node
{
    private GameObject currentEnemy;

    public CheckPlayerInFOVRange(GameObject enemy)
    {
        currentEnemy = enemy;
    }

    public override NodeState Evaluate()
    {
        //Check if the player is seen by the enemy
        if(currentEnemy.GetComponent<FieldOfView>().canSeePlayer)
        {
            state = NodeState.SUCCESS;
            return state;
        }


        state = NodeState.FAILURE;
        return state;
    }
}
