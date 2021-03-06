﻿
using System.IO;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;

// Tips
// https://docs.unity3d.com/ScriptReference/Resources.html
namespace UnityTest
{
  public class UnityAsset
  {
    // 编辑器引用的对象和Resources里面的资源， 为了减小内容，在build时候会进入最终的资源
    [Test]
    public void TestResourceLoad()
    {
      // Assets/Tests/Resources/heroIcon_10171.png
      Object obj = Resources.Load("heroIcon_10171");
      Texture2D tex = Resources.Load<Texture2D>("heroIcon_10171");
      Assert.That(obj, Is.Not.Null);
      Assert.That(tex, Is.Not.Null);
      Assert.That(obj == tex, Is.True, "实际类型是确定的");

      // 路径去掉Resources和extension
      Assert.That(Resources.Load("Assets/Tests/Resources/heroIcon_10171.png"), Is.Null);

      // Assets/Tests/heroIcon_10181.png
      Assert.That(Resources.Load("heroIcon_10181"), Is.Null);
    }

    [Test]
    public void TestMultipleObjectsFrom1Asset()
    {
      // heroIcon_10171 是sprite，通过UnityStudio观察，有两个object
      Object[] objects = Resources.LoadAll("heroIcon_10171");
      Assert.That(objects.Length, Is.EqualTo(2));
      Assert.That(objects[0] as Texture2D, Is.Not.Null, "Texture2D is the main asset");
      Assert.That(objects[1] as Sprite, Is.Not.Null);
    }

    [Test]
    public void TestResourceUnload()
    {
      Object obj = Resources.Load("heroIcon_10171");
      Resources.UnloadAsset(obj);
      Resources.UnloadAsset(obj);
      Assert.That(true, "重复卸载貌似正常");
    }

    [Test]
    public void TestAssetDatabase()
    {
      Object obj1 = AssetDatabase.LoadAssetAtPath("Assets/Tests/heroIcon_10181.png", typeof(Object));
      Assert.That(obj1, Is.Not.Null);
      obj1.name = "AssetDatabaseTest";
      Object obj2 = AssetDatabase.LoadAssetAtPath("Assets/Tests/heroIcon_10181.png", typeof(Object));
      Assert.That(System.Object.ReferenceEquals(obj1, obj2), "Unity的Object不会重复创建，除非bundle模式下接触关系");
      Assert.That(obj2.name, Is.EqualTo("AssetDatabaseTest"));
    }

    [Test]
    public void TestAssetBundle()
    {
      string name = "Assets/Examples/AssetBundle/Cube.prefab";
      var bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "bundles", "test"));
      Assert.That(bundle, Is.Not.Null);
      var obj = bundle.LoadAsset<GameObject>(name);
      Assert.That(obj, Is.Not.Null);
      Assert.That(bundle.LoadAsset<GameObject>(name), Is.EqualTo(obj), "不会重复创建");
      bundle.Unload(false);
      Assert.Throws<MissingReferenceException>(()=> { bundle.LoadAsset<GameObject>(name); }, "卸载后不能再创建");

      var bundleNew = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "bundles", "test"));
      Assert.That(bundleNew, Is.Not.EqualTo(bundle));
      var objNew = bundleNew.LoadAsset<GameObject>(name);
      Assert.That(objNew, Is.Not.Null);
      Assert.That(objNew, Is.Not.EqualTo(obj));
      bundleNew.Unload(true);
      Assert.Throws<MissingReferenceException>(()=> { bundleNew.LoadAsset<GameObject>(name); }, "彻底的卸载后也不能再创建");
    }
  }
}
