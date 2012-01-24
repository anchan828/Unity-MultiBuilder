
set UNITY_APP_PATH=%1
set UNITY_PROJECT_PATH=%2


for %%i in (%3,%4,%5,%6,%7,%8) do echo "%UNITY_APP_PATH%" -batchmode -quit -projectPath "%UNITY_PROJECT_PATH%" -executeMethod %%i