
using System.IO;
using System.Diagnostics;

using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;

public class ScriptBatch
{
  [MenuItem("Tutorial/Build/BuildClock")]
  public static void BuildClock()
  {
    //string path = EditorUtility.SaveFilePanel("BuildClock", Application.dataPath, "tutorial_clock", "");
    string path = "~/Desktop/tutorial_clock";

    string[] levels = new string[] {"Assets/Scenes/Clock.unity"};

    BuildReport report = BuildPipeline.BuildPlayer(levels, Path.Combine(path, "clock"), BuildTarget.StandaloneOSX, BuildOptions.None);
    foreach(var file in report.files)
      UnityEngine.Debug.Log(file);
    foreach(var step in report.steps)
      UnityEngine.Debug.Log(step);

    if (report.summary.result == BuildResult.Succeeded)
    {
      // 测试下post script
      string dest = Path.Combine(path, "topics");
      if (Directory.Exists(dest))
      {
        Directory.Delete(dest, true);
      }
      FileUtil.CopyFileOrDirectory(Path.Combine(Application.dataPath, "../topics"), dest);

      Process proc = new Process();
      proc.StartInfo.FileName = Path.Combine(path, "clock.app/Contents/MacOS/clock");
      UnityEngine.Debug.Log(proc.StartInfo.FileName);
      proc.Start();
    }
  }

  [MenuItem("Tutorial/Build/AssetTest")]
  public static void BulidAssetTest()
  {
    string path = "~/Desktop/tutorial_asset";
    string[] levels = new string[] {"Assets/Examples/AssetBundle/AssetBundle.unity"};

    BuildPipeline.BuildPlayer(levels, Path.Combine(path, "clock"), BuildTarget.StandaloneOSX, BuildOptions.None);
  }
}
