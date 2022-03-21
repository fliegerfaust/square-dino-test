using System;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Services.Input
{
  public class MouseInput : IInputService, ITickable
  {
    private const int LeftMouseButton = 0;
    public event Action<Vector3> OnClick;

    public void Tick()
    {
      if (UnityEngine.Input.GetMouseButtonDown(LeftMouseButton))
        OnClick?.Invoke(UnityEngine.Input.mousePosition);
    }
  }
}