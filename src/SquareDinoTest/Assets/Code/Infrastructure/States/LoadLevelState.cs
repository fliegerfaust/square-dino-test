using Code.CameraLogic;
using Code.Infrastructure.Services.Factory;
using Code.Infrastructure.Services.StaticData;
using Code.StaticData;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.States
{
  [UsedImplicitly]
  public class LoadLevelState : IState
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly IGameFactory _gameFactory;
    private readonly IStaticDataService _staticData;

    public LoadLevelState(IGameStateMachine stateMachine, IGameFactory gameFactory, IStaticDataService staticData)
    {
      _stateMachine = stateMachine;
      _gameFactory = gameFactory;
      _staticData = staticData;
    }

    public void Enter()
    {
      LoadData();
      InitGameWorld();
    }

    private void LoadData() =>
      _staticData.Load();

    private void InitGameWorld()
    {
      LevelStaticData levelData = LevelStaticData();
      GameObject player = InitPlayer(levelData);

      InitCamera(player);
      InitSpawners(levelData);

      StartGame(player);
    }

    private void StartGame(GameObject player) =>
      _stateMachine.Enter<ReadyGameState, GameObject>(player);

    private GameObject InitPlayer(LevelStaticData levelData) =>
      _gameFactory.CreatePlayer(levelData.PlayerInitialPosition);

    private void InitCamera(GameObject player)
    {
      GameObject thirdPersonCamera = _gameFactory.CreateThirdPersonCamera();
      thirdPersonCamera.GetComponent<CameraFollow>().Follow(player.transform);
    }

    private void InitSpawners(LevelStaticData levelData)
    {
      foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
        _gameFactory.CreateSpawner(spawnerData.Position, spawnerData.EnemySpawnId);
    }

    public void Exit()
    {
    }

    private LevelStaticData LevelStaticData() =>
      _staticData.ForLevel(SceneManager.GetActiveScene().name);
  }
}