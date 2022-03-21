using System.Collections.Generic;
using System.Linq;
using Code.StaticData;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Infrastructure.Services.StaticData
{
  [UsedImplicitly]
  public class StaticDataService : IStaticDataService
  {
    private const string StaticDataPlayerPath = "StaticData/PlayerData";
    private const string StaticDataLevelPath = "StaticData/LevelData";
    private const string StaticDataEnemiesPath = "StaticData/Enemies";
    private const string StaticDataBulletPath = "StaticData/BulletData";

    private PlayerStaticData _player;
    private LevelStaticData _level;
    private BulletStaticData _bullet;
    private Dictionary<EnemySpawnId, EnemyStaticData> _enemies;

    public void Load()
    {
      LoadLevel();
      LoadPlayer();
      LoadEnemies();
      LoadBullet();
    }

    public LevelStaticData ForLevel(string levelKey) =>
      _level;

    public PlayerStaticData ForPlayer() =>
      _player;

    public EnemyStaticData ForEnemy(EnemySpawnId typeId) =>
      _enemies.TryGetValue(typeId, out EnemyStaticData staticData) ? staticData : null;

    public BulletStaticData ForBullet() =>
      _bullet;

    private void LoadLevel() =>
      _level = Resources.Load<LevelStaticData>(StaticDataLevelPath);

    private void LoadPlayer() =>
      _player = Resources.Load<PlayerStaticData>(StaticDataPlayerPath);

    private void LoadEnemies() =>
      _enemies = Resources.LoadAll<EnemyStaticData>(StaticDataEnemiesPath)
        .ToDictionary(x => x.EnemySpawnId, x => x);

    private void LoadBullet() =>
      _bullet = Resources.Load<BulletStaticData>(StaticDataBulletPath);
  }
}