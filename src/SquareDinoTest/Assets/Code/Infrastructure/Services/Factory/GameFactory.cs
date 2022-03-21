using Code.Enemy;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Logic.EnemySpawners;
using Code.Infrastructure.ObjectPooling;
using Code.Infrastructure.Services.StaticData;
using Code.Player;
using Code.StaticData;
using Code.UI.Elements;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Services.Factory
{
  [UsedImplicitly]
  public class GameFactory : IGameFactory
  {
    private readonly IStaticDataService _staticData;
    private readonly IAssets _assets;
    private readonly DiContainer _diContainer;
    private readonly ObjectPool _objectPool;

    public GameFactory(IStaticDataService staticData, IAssets assets, DiContainer diContainer, ObjectPool objectPool)
    {
      _diContainer = diContainer;
      _staticData = staticData;
      _assets = assets;
      _objectPool = objectPool;
    }

    public GameObject CreatePlayer(Vector3 at)
    {
      GameObject player = _assets.Instantiate(AssetPath.PlayerPath, at);
      PlayerMove playerMove = player.GetComponent<PlayerMove>();

      _diContainer.Bind<PlayerMove>().FromInstance(playerMove);
      _diContainer.InjectGameObject(player);

      return player;
    }

    public GameObject CreateThirdPersonCamera() =>
      _assets.Instantiate(AssetPath.ThirdPersonCameraPath);

    public void CreateSpawner(Vector3 at, EnemySpawnId enemySpawnId)
    {
      GameObject prefab = _assets.Instantiate(AssetPath.SpawnerPath, at);
      _diContainer.InjectGameObject(prefab);

      SpawnPoint spawner = prefab.GetComponent<SpawnPoint>();
      spawner.EnemySpawnId = enemySpawnId;
    }

    public GameObject CreateEnemy(EnemySpawnId enemySpawnId, Transform parent)
    {
      EnemyStaticData enemyData = _staticData.ForEnemy(enemySpawnId);
      GameObject enemy = Object.Instantiate(enemyData.Prefab, parent.position, Quaternion.identity, parent);

      IHealth health = enemy.GetComponent<IHealth>();
      health.Current = enemyData.Hp;
      health.Max = enemyData.Hp;

      enemy.GetComponent<ActorUI>().Construct(health);

      // _diContainer.BindInterfacesAndSelfTo<IHealth>().FromInstance(health);
      _diContainer.InjectGameObject(enemy);

      return enemy;
    }

    public GameObject CreateBullet(Vector3 at)
    {
      BulletStaticData bulletData = _staticData.ForBullet();

      GameObject bullet = _objectPool.GetPoolObject();
      bullet.transform.SetPositionAndRotation(at, Quaternion.identity);

      Bullet setup = bullet.GetComponent<Bullet>();
      setup.Damage = bulletData.Damage;
      setup.Speed = bulletData.Speed;

      _diContainer.InjectGameObject(bullet);

      return bullet;
    }

    public GameObject CreateHud() =>
      _assets.Instantiate(AssetPath.HudPath);
  }
}