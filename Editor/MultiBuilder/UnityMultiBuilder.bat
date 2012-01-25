@echo off
set UNITY_APP_PATH=%1
set UNITY_PROJECT_PATH=%2

taskkill  /IM Unity.exe
echo.

for %%i in (%3,%4,%5,%6,%7,%8) do　echo %%iでビルド中です & echo. & %UNITY_APP_PATH% -batchmode -quit -projectPath %UNITY_PROJECT_PATH% -executeMethod %%i 

echo.
echo ビルドが終了しました。Unityを再起動します。

start "" %UNITY_APP_PATH%

