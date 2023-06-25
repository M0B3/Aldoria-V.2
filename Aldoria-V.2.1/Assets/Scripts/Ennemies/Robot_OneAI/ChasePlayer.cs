using BehaviourTree;

using UnityEngine;


public class ChasePlayer : Node
{

    RobotBT bt;
    Transform playerTransform;

    public ChasePlayer(RobotBT _bt, Transform _playerTransform)
    {
        this.bt = _bt;
        this.playerTransform = _playerTransform;
    }

    public override NodeState Evaluate()
    {
        //Change the animations
        Debug.Log("Chase player");
        bt.animator.SetBool("IsRunning", true);
        bt.animator.SetBool("IsShooting", false);

        //Set the destination to the player
        bt.agent.speed = bt.chaseSpeed;
        bt.agent.SetDestination(new Vector3(playerTransform.position.x, bt.gameObject.transform.position.y, playerTransform.position.z));

        state = NodeState.RUNNING;
        return state;
    }
}
