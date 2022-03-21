using System.Collections.Generic;
using Code.Infrastructure.Services.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.ObjectPooling
{
  public class ObjectPool : MonoBehaviour
  {
    [SerializeField] private int _poolSize;

    private GameObject _objectPrefab;
    private readonly Queue<GameObject> _objectPool = new Queue<GameObject>();
    private IStaticDataService _staticData;

    [Inject]
    public void Construct(IStaticDataService staticData) =>
      _staticData = staticData;

    private void Start()
    {
      _objectPrefab = _staticData.ForBullet().Prefab;

      for (int i = 0; i < _poolSize; i++)
      {
        GameObject poolObject = Instantiate(_objectPrefab, transform, true);
        _objectPool.Enqueue(poolObject);
        poolObject.SetActive(false);
      }
    }

    public GameObject GetPoolObject()
    {
      if (_objectPool.Count > 0)
      {
        GameObject poolObject = _objectPool.Dequeue();
        poolObject.SetActive(true);
        return poolObject;
      }
      else
      {
        GameObject poolObject = Instantiate(_objectPrefab, transform, true);
        return poolObject;
      }
    }

    public void ReturnPoolObject(GameObject poolObject)
    {
      _objectPool.Enqueue(poolObject);
      poolObject.SetActive(false);
    }
  }
}