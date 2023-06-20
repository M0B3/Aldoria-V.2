using UnityEngine;

using BehaviourTree;
using System.Security.Cryptography;
using UnityEngine.AI;

public class RobotBT : BehaviourTree.Tree
{

    [Header("References")]
    public GameObject zone;
    public NavMeshAgent agent;
    public Animator animator;

    [Header("Parameters")]
    public float speed = 2f;
    public float timeBetweenPatrols = 5f;


    //for scripts
    [HideInInspector] public bool isWaiting = false;

    protected override Node SetupTree()
    {
        agent.speed = speed;

        //Node root = new Taskpatrol(parameters);

        //Node root = new Selector(new List<Node>
        //{
        //    new Sequence(new List<Node>
        //    {
        //        //new CheckInFOVRange(),
        //        //new TaskToGoTarget(),
        //    })
        //    //new Taskpatrol(parameters)
        //});

        Node root = new Patrol(transform, this);

        return root;
    }
}
