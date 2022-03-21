using JetBrains.Annotations;
using Zenject;

namespace Code.Infrastructure.States
{
  [UsedImplicitly]
  public class GameStateMachineInitializer : IInitializable
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly LoadLevelState _loadLevelState;
    private readonly MoveState _moveState;
    private readonly EndGameState _endGameState;
    private readonly BattleState _battleState;
    private readonly ReadyGameState _readyGameState;

    public GameStateMachineInitializer(IGameStateMachine stateMachine, LoadLevelState loadLevelState,
      MoveState moveState, EndGameState endGameState, BattleState battleState, ReadyGameState readyGameState)
    {
      _stateMachine = stateMachine;
      _loadLevelState = loadLevelState;
      _moveState = moveState;
      _endGameState = endGameState;
      _battleState = battleState;
      _readyGameState = readyGameState;
    }

    public void Initialize()
    {
      RegisterStates();
      EnterLoadLevelState();
    }

    private void RegisterStates()
    {
      _stateMachine.RegisterState(_readyGameState);
      _stateMachine.RegisterState(_loadLevelState);
      _stateMachine.RegisterState(_moveState);
      _stateMachine.RegisterState(_battleState);
      _stateMachine.RegisterState(_endGameState);
    }

    private void EnterLoadLevelState() =>
      _stateMachine.Enter<LoadLevelState>();
  }
}