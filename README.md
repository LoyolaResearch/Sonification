# Chess Sonification
Feel free to edit and update this file with notes and issues.
This file needs to be updated!!!

## Goal
The final purpose of this project is to .... 

## Software
### Visual Studio (Community 2019)  
* https://visualstudio.microsoft.com/
* Just C# stuff ("")

* Installation
Create a new project in Visual Studio
 -must be "Windows Forms App (.NET Framework)
 -set the Framework to close to 4.5 (for pgn reader to work!)
 -might as well put the solution and project in the same directory

Add your buttons and such.

Add naudio/pgn
 -right-click on solution and "Manage NuGet Packages..."
 -Browse for naudio and install
 -rinse and repeat for pgn

Install Windozze Media Player
 -Tools, Choose Toolbox Items..., COM Components
 -Scroll down to Windows Media Player and check it
 -Find it under Common Controls and drop it in and name and hide it
 -Just set the URL to play and player. to stop


Files go in the exe directory in bin (fix this for later)

Project, [Project] Properties..., Application, Output Type:, Console Application (to also have the console)
Check Tools, Options, Debugging, "Automatically close the console when debugging stops"


### VC Package Manager (VCPKG)
* https://github.com/microsoft/vcpkg
* There is probably no need to download this again. Follow the instructions in the [External Libs](ExternalLibs/README.md) directory to get this working.


## Folder Structure
There are three main folders in this repo:
### ChessSonification
* This is the main folder for the formal simulator. There are not many files in this folder. Keep any project-specific files, data files, audio files, etc... in here. Generic libraries should go in the folders below.
### Library/Chess
* Keep all custom libraries here. There is a pretty clear folder stucture. It is possible to have multiple simulator projects running. These libraries are generic to any simulator.
### Library/MIDI
### PgnToFen
* Mostly this is VCPKG. But it also can be a place to play with/test external libraries.
* Stuff to read!

## Milestones and Tasks
I will keep a task list on GitHub in Milestone categories. Specifically:
1. (need to fill this in!))
2. 
3. 




## References
(more soon)



## ignore this...
---
---
---
---
---


## References

