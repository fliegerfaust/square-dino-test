using System;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Player
{
  public class PlayerMove : MonoBehaviour
  {
    public event Action OnWaypointReached;

    private const float Epsilon = 0.001f;
    private static readonly int Speed = Animator.StringToHash("Speed");

    private Animator _animator;
    private NavMeshAgent _agent;

    private void Start()
    {
      _agent = GetComponent<NavMeshAgent>();
      _animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
      if (IsWaypointReached())
        OnWaypointReached?.Invoke();

      SetAnimatorValues();
    }

    private bool IsWaypointReached() =>
      _agent.remainingDistance < Epsilon;

    private void SetAnimatorValues() =>
      _animator.SetFloat(Speed, _agent.desiredVelocity.magnitude);
  }
}