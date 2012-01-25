#!/bin/sh

# Unityアプリパス
UNITY_APP_PATH=$1
# 対象のUnityプロジェクトパス
UNITY_PROJECT_PATH=$2
# バッチモードで起動後に呼び出すメソッド
#UNITY_BATCH_EXECUTE_METHOD=$1
#UNITY_BATCH_EXECUTE_METHOD2=BatchBuild.BuildFlashPlayer
#UNITY_BATCH_EXECUTE_METHOD3=BatchBuild.BuildiOS
#UNITY_BATCH_EXECUTE_METHOD4=BatchBuild.BuildAndroid

# Unity Editor ログファイルパス
UNITY_EDITOR_LOG_PATH=~/Library/Logs/Unity/Editor.log

# 指定のUnityプロジェクトをバッチモード起動させて、指定のメソッド(UnityScript)を呼び出す
for i in $3 $4 $5 $6 $7 $8
do
$UNITY_APP_PATH -batchmode -quit -projectPath "${UNITY_PROJECT_PATH}" -executeMethod $i
done

# Unityでのbuildに失敗した場合は終了
if [ $? -eq 1 ]; then
cat $UNITY_EDITOR_LOG_PATH
exit 1
fi

# Unity Editorが出力したログを表示する
cat $UNITY_EDITOR_LOG_PATH
