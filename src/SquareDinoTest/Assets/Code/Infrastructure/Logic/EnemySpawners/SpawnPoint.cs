using Code.Infrastructure.Services.Factory;
using Code.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Logic.EnemySpawners
{
  public class SpawnPoint : MonoBehaviour
  {
    public EnemySpawnId EnemySpawnId;

    private IGameFactory _gameFactory;

    [Inject]
    public void Construct(IGameFactory gameFactory) =>
      _gameFactory = gameFactory;

    private void Start() =>
      Spawn();

    private void Spawn() =>
      _gameFactory.CreateEnemy(EnemySpawnId, transform);
  }
}