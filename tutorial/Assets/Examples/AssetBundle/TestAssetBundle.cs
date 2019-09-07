
using System.IO;
using UnityEngine;

public class TestAssetBundle : MonoBehaviour
{
  private Transform sample;
  
  void Start()
  {
    var path = Path.Combine(Application.streamingAssetsPath, "bundles", "test");
    var bundle = AssetBundle.LoadFromFile(path);
    if (bundle == null)
    {
      Debug.LogFormat("failed to load bundle {0}", path);
      return;
    }

    foreach (string name in bundle.GetAllAssetNames())
    {
      Debug.LogFormat("name {0}, contains {1}", name, bundle.Contains(name));
    }

    var objSample = bundle.LoadAsset<GameObject>("Assets/Examples/AssetBundle/Cube.prefab");
    sample = objSample.transform;

    Transform obj = GameObject.Instantiate(sample, gameObject.transform);
    obj.position = Vector3.left;
    obj.localScale = Vector3.one * 3;
    obj.name = "left";

    obj = GameObject.Instantiate(sample, gameObject.transform);
    obj.position = Vector3.up * 2;
    obj.localScale = Vector3.one * 2;
    obj.name = "top";
  }
}
