
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TestAssetBundle : MonoBehaviour
{
  private Transform sample;

  public GameObject image;
  
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

    Transform tf = GameObject.Instantiate(sample, gameObject.transform);
    tf.position = Vector3.left;
    tf.localScale = Vector3.one * 3;
    tf.name = "left";

    tf = GameObject.Instantiate(sample, gameObject.transform);
    tf.position = Vector3.up * 2;
    tf.localScale = Vector3.one * 2;
    tf.name = "top";

    Image img = image.GetComponent<Image>();
    bool flag = true;
    if (flag) // 这种方式最简单
      img.sprite = Resources.Load<Sprite>("heroIcon_10171");
    else
    {
      Texture2D tex = Resources.Load<Texture2D>("heroIcon_10171");
      Sprite sprite = img.sprite;
      Sprite sprite_new = Sprite.Create(tex, sprite.rect, sprite.pivot);
      img.sprite = sprite_new;
    }

    StringBuilder names = new StringBuilder();
    Object[] objs = Resources.FindObjectsOfTypeAll(typeof(Object));
    int count = 0;
    foreach (Object obj in objs)
    {
      names.Append(obj.name);
      count++;

      if (count == 4)
      {
        names.AppendLine();
        count = 0;
      }
      else
      {
        names.Append("\t");
      }
    }
    Debug.Log(names);
  }
}
