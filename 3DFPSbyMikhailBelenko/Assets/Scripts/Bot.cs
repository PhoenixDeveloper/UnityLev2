using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(ThirdPersonCharacter))]
public class Bot : Unit
{
    private NavMeshAgent _navAgent;
    private ThirdPersonCharacter _controller;
    private Transform _playerPosition;

    protected override void Awake()
    {
        base.Awake();
        _navAgent = GetComponent<NavMeshAgent>();
        _controller = GetComponent<ThirdPersonCharacter>();
        _playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        _navAgent.updatePosition = true;
        _navAgent.updateRotation = true;
    }

    void Update()
    {
        if (_navAgent)
        {
            _navAgent.SetDestination(_playerPosition.position);
            if (_navAgent.remainingDistance > _navAgent.stoppingDistance)
            {
                _controller.Move(_navAgent.desiredVelocity, false, false);
                Animator.SetBool("isMove", true);
            }
            else
            {
                _controller.Move(Vector3.zero, false, false);
                Animator.SetBool("isMove", false);
            }
        }
    }
}
