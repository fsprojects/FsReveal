@echo off
cls

dotnet tool restore
if errorlevel 1 (
  exit /b %errorlevel%
)

dotnet paket restore
if errorlevel 1 (
  exit /b %errorlevel%
)

packages\FAKE\tools\FAKE.exe build.fsx %*
