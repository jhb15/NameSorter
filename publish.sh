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

export ALL_RELEASE_FILE=$(ls NameSorter/bin/Release/netcoreapp2.1/*All.zip)
export LINUX_RELEASE_FILE=$(ls NameSorter/bin/Release/netcoreapp2.1/*Linux*.zip)
export WIN_RELEASE_FILE=$(ls NameSorter/bin/Release/netcoreapp2.1/*Windows*.zip)

echo "Linux file: $LINUX_RELEASE_FILE \n Win File: $WIN_RELEASE_FILE \n All File: $ALL_RELEASE_FILE"
