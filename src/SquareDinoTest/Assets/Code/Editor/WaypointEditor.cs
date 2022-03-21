using Code.Infrastructure.Logic.Waypoints;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
  [CustomEditor(typeof(WaypointForEditor))]
  public class WaypointEditor : UnityEditor.Editor
  {
    private const float SphereRadius = 2f;

    [DrawGizmo(GizmoType.Active)]
    public static void RenderCustomGizmo(WaypointForEditor waypoint, GizmoType gizmo)
    {
      Gizmos.color = Color.green;
      Vector3 position = waypoint.transform.position;
      Gizmos.DrawWireSphere(position, SphereRadius);
    }
  }
}