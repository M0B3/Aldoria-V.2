using UnityEngine;

using BehaviourTree;
using System.Security.Cryptography;
using UnityEngine.AI;
using System.Collections.Generic;

public class RobotBT : BehaviourTree.Tree
{

    [Header("References")]
    public GameObject zone;
    public Transform playerTransform;
    public NavMeshAgent agent;
    public Animator animator;

    [Header("Parameters")]
    public float speed = 2f;
    public float chaseSpeed = 4f;
    public float timeBetweenPatrols = 5f;
    public float distanceToAttackplayer = 10f;
    public int magazineSize = 30;
    [Range(0f, 100f)] public float precision;


    //for scripts
    [HideInInspector] public bool isWaiting = false;

    protected override Node SetupTree()
    {
        agent.speed = speed;


        Node root = new Selector(new List<Node>
       {
            new Sequence(new List<Node>
            {
                new CheckPlayerInFOVRange(this.gameObject),
                new Selector(new List<Node>
                {
                    new Sequence(new List<Node>
                    {
                        new CheckIfEnemyCanAttack(this.transform, playerTransform, this),
                        new RobotAttack(this),
                    }),
                    new ChasePlayer(this, playerTransform),
                })

            }),
            new Patrol(transform, this)
       });
        



        return root;
    }
}
