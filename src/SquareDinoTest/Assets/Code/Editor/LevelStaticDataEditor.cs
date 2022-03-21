using System.Collections.Generic;
using System.Linq;
using Code.Infrastructure.Logic.EnemySpawners;
using Code.Infrastructure.Logic.Waypoints;
using Code.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Editor
{
  [CustomEditor(typeof(LevelStaticData))]
  public class LevelStaticDataEditor : UnityEditor.Editor
  {
    private const string InitialPointTag = "PlayerInitialPoint";
    private const float WaypointRadius = 2f;

    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      LevelStaticData levelData = (LevelStaticData) target;

      if (GUILayout.Button("Collect"))
      {
        levelData.EnemySpawners = FindObjectsOfType<SpawnMarker>()
          .Select(x =>
            new EnemySpawnerData(x.EnemySpawnId, x.transform.position))
          .ToList();

        levelData.LevelKey = SceneManager.GetActiveScene().name;
        levelData.PlayerInitialPosition = GameObject.FindWithTag(InitialPointTag).transform.position;

        Transform waypoints = FindObjectOfType<Waypoints>().transform;
        levelData.EnemiesPerWaypoint = new int[waypoints.childCount];
        for (int i = 0; i < waypoints.childCount; i++)
        {
          Transform point = waypoints.GetChild(i);
          int count = FindObjectsOfType<SpawnMarker>()
            .Select(x => x.transform.position)
            .Count(x => Vector3.Distance(point.position, x) <= WaypointRadius);
          levelData.EnemiesPerWaypoint[i] = count;
          ;
        }
      }

      EditorUtility.SetDirty(target);
    }
  }
}