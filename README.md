# NameSorter [![Build Status](https://travis-ci.com/jhb15/NameSorter.svg?branch=master)](https://travis-ci.com/jhb15/NameSorter)
Simple cmd line tool for sorting names in a given input .txt file.

The Windows version of the application is built for Windows 10 but has ran on Windows 7 \
The Linux version of the application is built for Ubuntu 16.10 but has ran on other distributions

#### To Download Application:
>
>> Step 1: Navigate to the release page of this GitHub page and find the lates one,
 or follow this link. [Latest Release](https://github.com/jhb15/NameSorter/releases/latest)

>
>> Step 2: You should have a list of zip files in front of you, download the most relevant .zip for you 'NameSorter_Linux_.zip' for linux systems and 'NameSorter_Windows_.zip' for windows systems. There is also a zip containing both options 'NameSorter_All.zip'.
>
>> Step 3: Extract the zip file or files you have downloaded.
>

#### To Run This App:

>
>> Step 1: Once you have extracted the zip file open a terminal or cmd 
window and navigate to the directory that contains the cntent of the zip file.
>
>> Step 2: To run the application enter this into the terminal or cmd window:
>>
>>> Windows: 'NameSorter.exe ./names.txt'
>>
>>> Linux: './NameSorter ./names.txt'
>
>> Step 3: You should now see the names that where in the file printed 
and also then the names printed again once sorted.
>
>> Step 4: You can then find a file called './sortednames.txt' in the 
directory where you ran the application.
> 

#### Alternative Inputs

>
>> You can also input multiple files into the program, this is done by 
just adding another file path when running the program. Here is an 
example of inputting 3 files: 'NameSorter ./file1.txt ./file2.txt ./file3.txt'
>
>> The default format for the name file input is in order, but it is 
common to have names in other formats, you can use 2 other name formats 
in this program by using the -f flag. 
>>> Here is an example: './NameSorter -f SurnameFirst ./file1.txt'
>>
>>> Supported Formats {InOrder, SurnameFirst, Backwards}
>
>> By default this program sorts into ascending order, but if you wish 
to you can specify the order using the '-d' and '-a' flags when running 
the program. An example of this in use is `NameSorter -d ./file1.txt` 
this would sort 'file1.txt' into descending order.
>>> '-a' this flag specifies ascending order.
>>
>>> '-d' this flag specifies descending order.
>
