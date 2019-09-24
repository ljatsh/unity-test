using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;

// Tips
// https://docs.unity3d.com/ScriptReference/Resources.html
namespace Tests
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
      // TODO
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
  }
}
