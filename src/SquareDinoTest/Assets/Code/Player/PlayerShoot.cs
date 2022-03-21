using Code.Infrastructure.Services.Factory;
using Code.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.Player
{
  public class PlayerShoot : MonoBehaviour
  {
    [SerializeField] private Transform _bulletSpawnPoint;

    private IGameFactory _gameFactory;
    private IInputService _inputService;
    private Camera _mainCamera;

    [Inject]
    public void Construct(IGameFactory gameFactory, IInputService inputService, Camera mainCamera)
    {
      _inputService = inputService;
      _mainCamera = mainCamera;
      _gameFactory = gameFactory;
    }

    public void EnableInput() => _inputService.OnClick += OnClick;
    public void DisableInput() => _inputService.OnClick -= OnClick;

    private void OnClick(Vector3 position) =>
      Fire(position);

    private void Fire(Vector3 position)
    {
      Physics.Raycast(_mainCamera.ScreenPointToRay(position), out RaycastHit ray);

      Vector3 bulletSpawnPoint = _bulletSpawnPoint.position;
      Vector3 shootDirection = (ray.point - bulletSpawnPoint).normalized;

      GameObject bullet = _gameFactory.CreateBullet(bulletSpawnPoint);
      bullet.GetComponent<Bullet>().Initialize(shootDirection);
    }
  }
}