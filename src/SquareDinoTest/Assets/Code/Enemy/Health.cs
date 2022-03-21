using System;
using UnityEngine;

namespace Code.Enemy
{
  public class Health : MonoBehaviour, IHealth
  {
    public event Action OnChanged;

    [SerializeField] private float _max;
    [SerializeField] private float _current;

    public float Max
    {
      get => _max;
      set => _max = value;
    }

    public float Current
    {
      get => _current;
      set => _current = value;
    }

    public void TakeDamage(float damage)
    {
      Current -= damage;
      OnChanged?.Invoke();
    }
  }
}