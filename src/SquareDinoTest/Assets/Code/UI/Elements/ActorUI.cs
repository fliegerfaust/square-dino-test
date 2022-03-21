using Code.Enemy;
using UnityEngine;
using Zenject;

namespace Code.UI.Elements
{
  public class ActorUI : MonoBehaviour
  {
    [SerializeField] private HpBar _hpBar;
    private IHealth _health;

    public void Construct(IHealth health)
    {
      _health = health;
      _health.OnChanged += UpdateHpBar;
    }

    private void Start()
    {
      IHealth health = GetComponent<IHealth>();

      if (health != null)
        Construct(health);
    }

    private void OnDestroy()
    {
      if (_health != null)
        _health.OnChanged -= UpdateHpBar;
    }

    private void UpdateHpBar() =>
      _hpBar.SetValue(_health.Current, _health.Max);
  }
}