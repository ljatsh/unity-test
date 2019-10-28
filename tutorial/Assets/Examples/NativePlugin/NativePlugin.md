
Table of Contents
=================

* [Mac Key Points](#mac-key-points)
* [TODO](#todo)

Mac Key Points
--------------

* gyp的type不能为shared_library或者static_library或者executable, 随便填写一个， 脚本会给出提示。 GYP的手册不熟悉
* bundle的info.plist必须提供， 否则bundle不会被code sign， DllImport会报DllNotFoundException
* cpp的函数需要extern "C", 否则DllImport会报EntryPointNotFoundException
* ```bash gyp -f xcode --depth . native.gyp```

TODO
----

* [bundle](https://developer.apple.com/library/archive/documentation/CoreFoundation/Conceptual/CFBundles/Introduction/Introduction.html#//apple_ref/doc/uid/10000123i-CH1-SW1)
* 0000000000000fad T __Z7mutipleii vs 0000000000000fad T _mutiple (mangling)
* 如何删除native.bundle， 否则会导致build的code sign失败(#'shellPath': '/bin/sh',
        #'shellScript': 'rm -fr ${PROJECT_DIR}/../tutorial/Assets/Plugins/native.bundle\n')