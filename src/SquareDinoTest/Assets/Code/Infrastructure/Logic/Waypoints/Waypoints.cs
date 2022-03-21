using UnityEngine;

namespace Code.Infrastructure.Logic.Waypoints
{
  public class Waypoints : MonoBehaviour
  {
    public Transform GetNextWaypoint(Transform currentWaypoint)
    {
      if (currentWaypoint == null)
        return transform.GetChild(0);

      if (currentWaypoint.GetSiblingIndex() < transform.childCount - 1)
        return transform.GetChild(currentWaypoint.GetSiblingIndex() + 1);
      
      return null;
    }
  }
}