#!/bin/bash

dotnet tool restore
exit_code=$?
if [ $exit_code -ne 0 ]; then
  exit $exit_code
fi

dotnet paket restore
exit_code=$?
if [ $exit_code -ne 0 ]; then
  exit $exit_code
fi

packages/FAKE/tools/FAKE.exe $@ build.fsx
