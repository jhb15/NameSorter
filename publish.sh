ReleaseDir="./NameSorter/bin/Release"

revision=${TRAVIS_JOB_ID}
#revision=${TRAVIS_JOB_ID:=1}
#revision=$(printf "%04d" $revision)

echo "#################################################################"
echo $revision
echo "#################################################################"

cd NameSorter

#dotnet publish -c Release
dotnet publish -c Release -r win10-x64 
dotnet publish -c Release -r ubuntu.16.10-x64

cd ../$ReleaseDir/netcoreapp2.1

zip -r NameSorter_Windows_$revision.zip win10-x64/*
zip -r NameSorter_Linux_$revision.zip ubuntu.16.10-x64/*
zip -r NameSorter_All.zip NameSorter_Windows_$revision.zip NameSorter_Linux_$revision.zip

#cp -r NameSorter.zip ../../../../
cd ../../../../

export RELEASE_PKG_FILE=$(ls NameSorter/bin/Release/netcoreapp2.1/*.zip)
echo "Deploying $RELEASE_PKG_FILE to GitHub as a Tag"
