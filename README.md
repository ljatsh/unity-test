
Table Of Contents
=================

* [Unity Section](#unity-section)
  * [UI](#ui)
    * [IMGUI](#imgui)
  * [Debug](#debug)
* [TODO](#todo)

Unity Section
=============

训练时间线

[UI](https://kapeli.com/dash_share?docset_file=Unity%203D&docset_name=Unity%203D&path=docs.unity3d.com/2022.1/Documentation/Manual/UIToolkits.html&platform=unity3d&repo=Main&source=docs.unity3d.com/2022.1/Documentation/Manual/UIToolkits.html&version=2022.1)
-------------------------------------------------------------------------------------------------------------

IMGUI
-----

UIElements Debugger

* 菜单 文件打开保存
* docker window

Debug
-----

Debugger for unity 3.0.2不能触发断点, 报error while processing request 'stackTrace' (exception: This request is not supported by the protocol)。删除.csproj和.sln不能解决问题。 降级到2.7.5可行

TODO
====

脚本的接口回调机制; 脚本的初始化机制; 生命周期等