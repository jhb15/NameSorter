set -e

ReleaseDir="./NameSorter/bin/Release"

if [ -d $ReleaseDir ]; then
  rm -R $ReleaseDir
fi

dotnet restore
dotnet build
dotnet test #For some reason this fails on a frst attempt, but manages a second time.

revision=${TRAVIS_JOB_ID}
#revision=${TRAVIS_JOB_ID:=1}
#revision=$(printf "%04d" $revision)

echo "#################################################################"
echo $revision
echo "#################################################################"

cd NameSorter

dotnet publish -c Release
dotnet publish -c Release -r win10-x64 
dotnet publish -c Release -r ubuntu.16.10-x64

cd ../$ReleaseDir/netcoreapp2.1

zip -r NameSorter_Windos$revision.zip win10-x64/*
zip -r NameSorter_Linux_$revision.zip ubuntu.16.10-x64/*
zip -r NameSorter.zip NameSorter_Windos$revision.zip NameSorter_Linux_$revision.zip

cd ../../../../
