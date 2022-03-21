using System;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Services.Input
{
  public class MobileInput : IInputService, ITickable
  {
    public event Action<Vector3> OnClick;

    public void Tick()
    {
      if (UnityEngine.Input.touchCount > 0)
      {
        Touch touch = UnityEngine.Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
          OnClick?.Invoke(touch.position);
      }
    }
  }
}