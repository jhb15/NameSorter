# NameSorter [![Build Status](https://travis-ci.com/jhb15/NameSorter.svg?branch=master)](https://travis-ci.com/jhb15/NameSorter)
Simple cmd line tool for sorting names in a given input .txt file.

#### To Download Application:
>
>> Step 1: Go to the this tagged release, https://github.com/jhb15/NameSorter/releases/tag/1.0
>
>> Step 2: You should have a list of zip files in front of you, download the most relevant .zip for you 'NameSorter_Linux_.zip' for linux systems and 'NameSorter_Windows_.zip' for windows systems. There is also a zip containing both options 'NameSorter_All.zip'.
>
>> Step 3: Extract the zip file or files you have downloaded.
>

#### To Run This App 'Windows':

>
>> Step 1: Once you have extracted the zip file open a terminal or cmd window and navigate to the directory that contains the cntent of the zip file.
>
>> Step 2: To run the application enter this into the terminal or cmd window:
>
>>> Windows: 'NameSorter.exe ./names.txt'
>
>>> Linux: 'NameSorter ./names.txt'
>
>> Step 3: You should now see the names that where in the file printed and also then the names printed again once sorted.
>
>> Step 4: You can then find a file called './sorted_names.txt' in the directory where you ran the application.
> 

#### Alternative Inputs

>
>> You can also input multiple files into the program, this is done by just adding another file path when running the program. Here is an example of inputting 3 files: 'NameSorter ./file1.txt ./file2.txt ./file3.txt'
>
>> The default format for the name file input is in order, but it is common to have names in other formats, you can use 2 other name formats in this program by using the -f flag. Here is an example: 'NameSorter -f SurnameFirst ./file1.txt'
>>> Supported Formats {InOrder, SurnameFirst, Backwards}
>
