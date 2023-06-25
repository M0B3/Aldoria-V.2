using BehaviourTree;

using UnityEngine;

public class RobotAttack : Node
{
    RobotBT bt;

    public RobotAttack(RobotBT _bt)
    {
        this.bt = _bt;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Attack player");
        bt.animator.SetBool("IsShooting", true);

        bt.agent.isStopped = true;
        bt.agent.ResetPath();


        state = NodeState.RUNNING;
        return state;
    }
}
