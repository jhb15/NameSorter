set -e

ReleaseDir="./NameSorter/bin/Release"

if [ -d $ReleaseDir ]; then
  rm -R $ReleaseDir
fi

dotnet restore
dotnet build
dotnet test #For some reason this fails on a frst attempt, but manages a second time.
