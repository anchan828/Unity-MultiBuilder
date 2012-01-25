
set UNITY_APP_PATH=%1
set UNITY_PROJECT_PATH=%2

taskkill  /IM Unity.exe

for %%i in (%3,%4,%5,%6,%7,%8) do %UNITY_APP_PATH% -batchmode -quit -projectPath %UNITY_PROJECT_PATH% -executeMethod %%i

start "" %UNITY_APP_PATH%


