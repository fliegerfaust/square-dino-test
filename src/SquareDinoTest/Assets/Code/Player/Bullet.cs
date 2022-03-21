using Code.Enemy;
using Code.Infrastructure.ObjectPooling;
using UnityEngine;
using Zenject;

namespace Code.Player
{
  public class Bullet : MonoBehaviour
  {
    private ObjectPool _objectPool;
    public int Damage { get; set; }
    public float Speed { get; set; }

    [Inject]
    public void Construct(ObjectPool objectPool) =>
      _objectPool = objectPool;

    public void Initialize(Vector3 shootDirection) =>
      GetComponent<Rigidbody>().velocity = shootDirection * Speed;

    private void OnTriggerEnter(Collider other)
    {
      if (other != null)
      {
        if (other.TryGetComponent(out IHealth enemy))
          enemy.TakeDamage(Damage);

        _objectPool.ReturnPoolObject(gameObject);
      }
    }
  }
}