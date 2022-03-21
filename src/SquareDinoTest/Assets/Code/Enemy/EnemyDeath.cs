using System;
using UnityEngine;
using Zenject;

namespace Code.Enemy
{
  [RequireComponent(typeof(Health))]
  public class EnemyDeath : MonoBehaviour
  {
    [SerializeField] private Health _health;
    [SerializeField] private Animator _animator;
    [SerializeField] private LookAtPlayer _lookAtPlayer;
    private SignalBus _signalBus;

    public event Action Happened;

    [Inject]
    public void Construct(SignalBus signalBus) =>
      _signalBus = signalBus;

    private void Start() =>
      _health.OnChanged += HealthChanged;

    private void HealthChanged()
    {
      if (_health.Current <= 0)
        Die();
    }

    private void Die()
    {
      _animator.enabled = false;
      _health.OnChanged -= HealthChanged;
      Happened?.Invoke();
      _signalBus.Fire(new EnemyDeadSignal());
      _lookAtPlayer.enabled = false;
      Destroy(gameObject, 2f);
    }
  }
}