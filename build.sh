#!/bin/bash

# 命令行脚本可以看到稍详细的信息
# https://docs.unity3d.com/Manual/CommandLineArguments.html

Unity=/Applications/Unity/Hub/Editor/2019.1.8f1/Unity.app/Contents/MacOS/Unity
LiceFile=unity_2019.alf

#$Unity -batchmode -createManualActivationFile -logfile
#$Unity -batchmode -manualLicenseFile $LiceFile -logfile -
$Unity -batchmode -projectPath tutorial -executeMethod ScriptBatch.BulidAssetTest -logfile -
