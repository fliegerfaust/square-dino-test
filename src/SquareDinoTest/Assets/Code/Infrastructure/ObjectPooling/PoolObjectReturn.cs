using UnityEngine;
using Zenject;

namespace Code.Infrastructure.ObjectPooling
{
  public class PoolObjectReturn : MonoBehaviour
  {
    private ObjectPool _objectPool;

    [Inject]
    public void Construct(ObjectPool objectPool) =>
      _objectPool = objectPool;

    private void OnDisable()
    {
      if (_objectPool != null)
        _objectPool.ReturnPoolObject(gameObject);
    }
  }
}