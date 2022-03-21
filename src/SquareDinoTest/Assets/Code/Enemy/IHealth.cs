using System;

namespace Code.Enemy
{
  public interface IHealth
  {
    event Action OnChanged;
    float Max { get; set; }
    float Current { get; set; }
    void TakeDamage(float damage);
  }
}