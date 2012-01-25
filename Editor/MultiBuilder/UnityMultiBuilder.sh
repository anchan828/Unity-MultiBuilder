#!/bin/sh

# Unityアプリパス
UNITY_APP_PATH=$1
# 対象のUnityプロジェクトパス
UNITY_PROJECT_PATH=$2

# Unity Editor ログファイルパス
UNITY_EDITOR_LOG_PATH=~/Library/Logs/Unity/Editor.log

for i in $3 $4 $5 $6 $7 $8
do
$UNITY_APP_PATH -batchmode -quit -projectPath "${UNITY_PROJECT_PATH}" -executeMethod $i
echo $i でビルド中です
done

# Unityでのbuildに失敗した場合は終了
if [ $? -eq 1 ]; then
cat $UNITY_EDITOR_LOG_PATH
exit 1
fi

# Unity Editorが出力したログを表示する
cat $UNITY_EDITOR_LOG_PATH
