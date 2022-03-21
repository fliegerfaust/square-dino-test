using Code.Enemy;
using Code.Infrastructure.Services.StaticData;
using Code.Player;
using JetBrains.Annotations;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Infrastructure.States
{
  [UsedImplicitly]
  public class BattleState : IPayloadedState<NavMeshAgent>
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly IStaticDataService _staticData;
    private readonly SignalBus _signalBus;

    private NavMeshAgent _navMeshAgent;
    private PlayerShoot _playerShoot;
    private int _currentWaypointId = 0;
    private int _enemiesRemain;

    public BattleState(IGameStateMachine stateMachine, IStaticDataService staticData, SignalBus signalBus)
    {
      _stateMachine = stateMachine;
      _staticData = staticData;
      _signalBus = signalBus;
    }

    public void Enter(NavMeshAgent navMeshAgent)
    {
      _signalBus.Subscribe<EnemyDeadSignal>(OnEnemyDeath);
      _enemiesRemain = WaypointEnemiesNumber();

      _navMeshAgent = navMeshAgent;

      _playerShoot = _navMeshAgent.GetComponent<PlayerShoot>();
      _playerShoot.EnableInput();
    }

    private void OnEnemyDeath()
    {
      _enemiesRemain -= 1;
      if (_enemiesRemain == 0)
        _stateMachine.Enter<MoveState, NavMeshAgent>(_navMeshAgent);
    }

    public void Exit()
    {
      _playerShoot.DisableInput();
      _currentWaypointId += 1;
      _signalBus.Unsubscribe<EnemyDeadSignal>(OnEnemyDeath);
    }

    private int WaypointEnemiesNumber() =>
      _staticData.ForLevel(SceneManager.GetActiveScene().name).EnemiesPerWaypoint[_currentWaypointId];
  }
}