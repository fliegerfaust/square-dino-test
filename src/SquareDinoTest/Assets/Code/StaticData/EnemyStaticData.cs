using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Enemies", order = 2)]
  public class EnemyStaticData : ScriptableObject
  {
    public EnemySpawnId EnemySpawnId;

    [Range(1, 100)] public float Hp;

    public GameObject Prefab;
  }
}