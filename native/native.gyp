{
  'targets': [
    {
      'target_name': 'native',
      'product_name': 'native',
      'type': 'loadable_module',
      'mac_bundle': 1,
      'sources': [ 'test.c', 'test_cpp.cpp'],
      'xcode_settings': {
        'INFOPLIST_FILE': 'Info.plist',
        'DEPLOYMENT_LOCATION': 'YES',
        'DSTROOT' : '$(PROJECT_DIR)/../tutorial/Assets',
        'INSTALL_PATH' : '/Plugins',
        'SKIP_INSTALL' : 'NO'
      },
    }
  ]
}

# gyp: Target native.gyp:native#target has an invalid target type 'bundle'.  Must be one of executable/loadable_module/static_library/shared_library/mac_kernel_extension/none/windows_driver.
