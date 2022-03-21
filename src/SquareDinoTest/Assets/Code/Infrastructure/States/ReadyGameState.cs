using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Services.Input;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Infrastructure.States
{
  [UsedImplicitly]
  public class ReadyGameState : IPayloadedState<GameObject>
  {
    private readonly IInputService _inputService;
    private readonly IGameStateMachine _stateMachine;
    private readonly IAssets _assets;

    private NavMeshAgent _navMeshAgent;
    private GameObject _hud;

    public ReadyGameState(IInputService inputService, IGameStateMachine stateMachine, IAssets assets)
    {
      _inputService = inputService;
      _stateMachine = stateMachine;
      _assets = assets;
    }

    public void Enter(GameObject player)
    {
      _navMeshAgent = player.GetComponent<NavMeshAgent>();
      _hud = _assets.Instantiate(AssetPath.HudPath);

      _inputService.OnClick += OnClick;
    }

    private void OnClick(Vector3 position)
    {
      _hud.SetActive(false);
      _stateMachine.Enter<MoveState, NavMeshAgent>(_navMeshAgent);
    }

    public void Exit() =>
      _inputService.OnClick -= OnClick;
  }
}