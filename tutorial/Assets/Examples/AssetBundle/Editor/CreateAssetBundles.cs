
using System.IO;
using UnityEditor;
using UnityEngine;
public class CreateAssetBundles
{
  [MenuItem("Tutorial/AssetBundle/Build All")]
  public static void BuildBundle()
  {
    string path = Path.Combine(Application.streamingAssetsPath, "bundles");
    if (!Directory.Exists(path))
    {
      Directory.CreateDirectory(path);
    }

    AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneOSX);
    foreach (string name in manifest.GetAllAssetBundles())
    {
      Debug.Log(name);
    }
  }
}
