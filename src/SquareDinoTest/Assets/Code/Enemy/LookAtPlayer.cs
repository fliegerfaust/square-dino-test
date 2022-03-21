using Code.Player;
using UnityEngine;
using Zenject;

namespace Code.Enemy
{
  public class LookAtPlayer : MonoBehaviour
  {
    [SerializeField] private float _speed = 5f;

    private Transform _playerTransform;
    private Vector3 _positionToLook;

    [Inject]
    public void Construct(PlayerMove player) =>
      _playerTransform = player.transform;

    private void Update() =>
      RotateTowardsHero();

    private void RotateTowardsHero()
    {
      UpdatePositionToLookAt();
      transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
    }

    private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook) =>
      Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());

    private Quaternion TargetRotation(Vector3 position) =>
      Quaternion.LookRotation(position);

    private void UpdatePositionToLookAt()
    {
      Vector3 positionDelta = _playerTransform.position - transform.position;
      _positionToLook = new Vector3(positionDelta.x, positionDelta.y, positionDelta.z);
    }

    private float SpeedFactor() =>
      Time.deltaTime * _speed;
  }
}