using Code.StaticData;

namespace Code.Infrastructure.Services.StaticData
{
  public interface IStaticDataService
  {
    void Load();
    LevelStaticData ForLevel(string levelKey);
    PlayerStaticData ForPlayer();
    EnemyStaticData ForEnemy(EnemySpawnId enemySpawnId);
    BulletStaticData ForBullet();
  }
}