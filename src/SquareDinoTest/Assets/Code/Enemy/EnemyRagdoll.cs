using UnityEngine;

namespace Code.Enemy
{
  public class EnemyRagdoll : MonoBehaviour
  {
    [SerializeField] private Rigidbody[] _rigidbodies;
    [SerializeField] private EnemyDeath _enemyDeath;

    private void Awake()
    {
      foreach (Rigidbody rb in _rigidbodies)
        rb.isKinematic = true;
      _enemyDeath.Happened += ActivateRagdoll;
    }

    private void ActivateRagdoll()
    {
      foreach (Rigidbody rb in _rigidbodies)
        rb.isKinematic = false;
    }

    private void OnDestroy() =>
      _enemyDeath.Happened -= ActivateRagdoll;
  }
}