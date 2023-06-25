using BehaviourTree;

using UnityEngine;

public class CheckIfEnemyCanAttack : Node
{

    Transform enemyTransform;
    Transform playertransform;
    RobotBT bt;

    public CheckIfEnemyCanAttack(Transform _enemyTransform, Transform _playerTransform, RobotBT _bt)
    {
        this.enemyTransform = _enemyTransform;
        this.playertransform = _playerTransform;
        this.bt = _bt;
    }

    public override NodeState Evaluate()
    {
        //Check if enemy is in the range to attack player;
        float distance = Vector3.Distance(enemyTransform.position, playertransform.position);
        if(distance <= bt.distanceToAttackplayer)
        {
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }

}
