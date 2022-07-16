using System;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIFollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Animator _animator;

    [SerializeField] private float distanceToHit;

    [SerializeField] private AnimationClip attack;
    
    private bool hasSeenPlayer = false;
    private NavMeshAgent navMeshAgent;
    

    private static int _animMoveSpeed = Animator.StringToHash("moveSpeed");
    private static int _animAttack = Animator.StringToHash("attack");
    void Start()
    {
        if (!player)
        {
            player = GameObject.FindWithTag("Player");
            if (player == null)
            {
                enabled = false;
                throw new Exception("Failed to find gameObject with tag 'Player'");
            }
        }

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = true;
    }

    private bool CloseEnoughToHit => Vector3.Distance(transform.position, player.transform.position) <= distanceToHit;

    void Update()
    {
        CheckLineOfSight();
        CheckMoveTowardPlayer();
        CheckAttack();
    }

    private void CheckAttack()
    {
        if (hasSeenPlayer && CloseEnoughToHit)
        {
            _animator.SetTrigger(_animAttack);
        }
    }

    private void CheckLineOfSight()
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        float angleBetweenForwardAndPlayerVectors = Vector3.Angle(transform.forward, directionToPlayer);
        if (angleBetweenForwardAndPlayerVectors < 45f)
        {
            hasSeenPlayer = true;
        }
    }

    private void CheckMoveTowardPlayer()
    {
        if (hasSeenPlayer && !CloseEnoughToHit)
        {
            MoveTowardPlayer();
        }
        else
        {
            navMeshAgent.velocity = Vector3.zero;
        }
    }

    void MoveTowardPlayer()
    {
        navMeshAgent.destination = player.transform.position;
        _animator.SetFloat(_animMoveSpeed, navMeshAgent.velocity.magnitude);
    }
}