using JetBrains.Annotations;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.States
{
  [UsedImplicitly]
  public class EndGameState : IState
  {
    private const string SceneName = "Demo";
    private readonly IGameStateMachine _stateMachine;

    public EndGameState(IGameStateMachine stateMachine) =>
      _stateMachine = stateMachine;

    public void Enter() =>
      ReloadGame();

    private static void ReloadGame() =>
      SceneManager.LoadScene(SceneName);

    public void Exit()
    {
    }
  }
}