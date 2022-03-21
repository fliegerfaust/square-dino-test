using Code.Infrastructure.Logic.Waypoints;
using Code.Player;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Infrastructure.States
{
  [UsedImplicitly]
  public class MoveState : IPayloadedState<NavMeshAgent>
  {
    private readonly Waypoints _waypoints;
    private readonly IGameStateMachine _stateMachine;

    private Transform _currentWaypoint;
    private NavMeshAgent _navMeshAgent;

    public MoveState(Waypoints waypoints, IGameStateMachine stateMachine)
    {
      _stateMachine = stateMachine;
      _waypoints = waypoints;
    }

    public void Enter(NavMeshAgent navMeshAgent)
    {
      _navMeshAgent = navMeshAgent;
      _navMeshAgent.GetComponent<PlayerMove>().OnWaypointReached += OnWaypointReached;
      _currentWaypoint = _waypoints.GetNextWaypoint(_currentWaypoint);
      if (CheckWaypointIsNotNull())
        _navMeshAgent.SetDestination(_currentWaypoint.position);
    }

    private bool CheckWaypointIsNotNull() =>
      _currentWaypoint != null;

    private void OnWaypointReached()
    {
      if (CheckWaypointIsNotNull())
        _stateMachine.Enter<BattleState, NavMeshAgent>(_navMeshAgent);
      else
        _stateMachine.Enter<EndGameState>();
    }

    public void Exit() =>
      _navMeshAgent.GetComponent<PlayerMove>().OnWaypointReached -= OnWaypointReached;
  }
}