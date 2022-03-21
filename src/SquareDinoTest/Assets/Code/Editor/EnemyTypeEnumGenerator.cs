using System.Collections.Generic;
using System.IO;
using System.Linq;
using Code.StaticData;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
  public static class EnemyTypeEnumGenerator
  {
    [MenuItem("Tools/GenerateEnemyTypeEnum")]
    public static void Generate()
    {
      const string enumName = "EnemySpawnId";
      const string filePathAndName = "Assets/Code/StaticData/" + enumName + ".cs";
      EnemyStaticData[] enemiesEnumEntries = Resources.LoadAll<EnemyStaticData>("StaticData/Enemies");
      List<string> enums = enemiesEnumEntries.Select(enemiesEnumEntry => enemiesEnumEntry.name).ToList();

      using (StreamWriter streamWriter = new StreamWriter(filePathAndName))
      {
        streamWriter.WriteLine("namespace Code.StaticData");
        streamWriter.WriteLine("{");
        streamWriter.WriteLine($"\tpublic enum {enumName}");
        streamWriter.WriteLine("\t{");
        for (int i = 0; i < enums.Count; i++)
        {
          string tEnum = enums[i];
          streamWriter.WriteLine($"\t \t {tEnum} = {i},");
        }

        streamWriter.WriteLine("\t}");
        streamWriter.WriteLine("}");
      }

      AssetDatabase.Refresh();
    }
  }
}