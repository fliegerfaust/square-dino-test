using Code.Infrastructure.Logic.EnemySpawners;
using Code.StaticData;
using UnityEngine;

namespace Code.Infrastructure.Services.Factory
{
  public interface IGameFactory
  {
    GameObject CreatePlayer(Vector3 at);
    GameObject CreateHud();
    GameObject CreateThirdPersonCamera();
    GameObject CreateEnemy(EnemySpawnId enemySpawnId, Transform transform);
    void CreateSpawner(Vector3 position, EnemySpawnId enemySpawnId);
    GameObject CreateBullet(Vector3 at);
  }
}