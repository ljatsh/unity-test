
Table of Contents
=================

* [Tips](#tips)
* [Reference](#reference)
* [TODO](#todo)

Tips
----

* Asset的GUID隐藏了具体的文件路径， 对相互依赖很友好。简单地实验下来，移动文件并没有改变GUID，至于Unity的具体处理实现，没有必要再去研究了
* 定义自己的的AssetImporter是很有意思的，可以方便的从其他格式适配到Unity内置格式

Reference
---------

* [tutorial](https://learn.unity.com/tutorial/assets-resources-and-assetbundles#5c7f8528edbc2a002053b5a5)
* [Database](https://docs.unity3d.com/Manual/AssetDatabase.html)
* [AssetImporter](https://docs.unity3d.com/Manual/ScriptedImporters.html)

TODO
-----

1. 创建自定义的Asset， 譬如Shape Factory.asset
2. [BuildPipeline](https://docs.unity3d.com/Manual/BuildPlayerPipeline.html)
3. [SerializedObject](https://docs.unity3d.com/ScriptReference/SerializedObject.html)
4. [Custome Managed Assemblies](https://docs.unity3d.com/Manual/ScriptCompilationAssemblyDefinitionFiles.html)
5. MonoScripts and MonoBehavior
6. 如何平衡资源读取的速度和内存占用?
7. [AssetBundle](https://docs.unity3d.com/Manual/AssetBundlesIntro.html)
8. [AssetBundle Management](https://docs.unity3d.com/Manual/AssetBundles-Native.html) Resource能卸载来自于Bundle的object
9. Application.backgroundLoadingPriority

[Back to TOC](#table-of-contents)
