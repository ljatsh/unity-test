
Table of Contents
=================

* [Tips](#tips)
* [Reference](#reference)
* [TODO](#todo)

Tips
----

* Asset的GUID隐藏了具体的文件路径， 对相互依赖很友好。简单地实验下来，移动文件并没有改变GUID，至于Unity的具体处理实现，没有必要再去研究了
* 定义自己的的AssetImporter是很有意思的，可以方便的从其他格式适配到Unity内置格式
* Scene引用的对象和Resources里面的资源， 为了减小内容，在build时候会进入最终的资源
* 可以通过[Resources.FindObjectsOfTypeAll](https://docs.unity3d.com/ScriptReference/Resources.FindObjectsOfTypeAll.html)来列举已经载入的对象
* AssetStudio工具很好用
* Assembly Definitions，有下面几个原因，需要拆分并自定义Assembly
  * 减少编译时间
  * 做单元测试
  * Assembly Definitions下面的Editor也会进入Assembly， 需要更改

Reference
---------

* [tutorial](https://learn.unity.com/tutorial/assets-resources-and-assetbundles#5c7f8528edbc2a002053b5a5)
* [Database](https://docs.unity3d.com/Manual/AssetDatabase.html)
* [AssetImporter](https://docs.unity3d.com/Manual/ScriptedImporters.html)
* [BuildPipeline](https://docs.unity3d.com/Manual/BuildPlayerPipeline.html)
* [Resources](https://docs.unity3d.com/ScriptReference/Resources.html)
* [AssetStudio](https://github.com/Perfare/AssetStudio)
* [AssetBundle](https://docs.unity3d.com/Manual/AssetBundlesIntro.html)
* [AssetBundle Management](https://docs.unity3d.com/Manual/AssetBundles-Native.html) Resource能卸载来自于Bundle的object
* [Assembly Definitions](https://docs.unity3d.com/Manual/ScriptCompilationAssemblyDefinitionFiles.html)
* [Special Folder Names](https://docs.unity3d.com/Manual/SpecialFolders.html)

TODO
-----

1. 创建自定义的Asset， 譬如Shape Factory.asset
2. [SerializedObject](https://docs.unity3d.com/ScriptReference/SerializedObject.html)
3. MonoScripts and MonoBehavior
4. 如何平衡资源读取的速度和内存占用?
5. Application.backgroundLoadingPriority

[Back to TOC](#table-of-contents)

```csharp
public static void LogMessage(string msg)
{
    WriteMessage?.Invoke(msg);
}
// null conditional operator (?.) short-circuits
```

