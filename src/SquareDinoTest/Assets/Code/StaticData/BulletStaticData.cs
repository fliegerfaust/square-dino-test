using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(fileName = "BulletData", menuName = "StaticData/Bullet", order = 3)]
  public class BulletStaticData : ScriptableObject
  {
    [Range(1, 20)] public float Speed = 5f;
    [Range(1, 100)] public int Damage = 20;
    public GameObject Prefab;
  }
}