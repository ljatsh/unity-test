﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

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
      Assert.That(obj == tex, Is.True);

      // 路径去掉Resources和extension
      Assert.That(Resources.Load("Assets/Tests/Resources/heroIcon_10171.png"), Is.Null);

      // Assets/Tests/heroIcon_10181.png
      Assert.That(Resources.Load("heroIcon_10181"), Is.Null);
    }

    [Test]
    public void TestResourceUnload()
    {
      // TODO
    }
  }
}