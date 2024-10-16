# Sonification
This is a set of projects that allows different methods of sonification. Currently, the are two projects
in this repository. Both of these projects use naudio (https://github.com/naudio/NAudio) to store MIDI files. 
It is probably possible to use the same library for playback, but WMP is sufficient for our purposes.

## ChessSonification
The first project we started with is ChessSonification. This loads a pgn (chess game notation) file and
parses the possible games. It actually does a great deal more than just loading a set of games. It also
analyzes each move in the list of games and stores it in multiple structures for later use. This project
uses various pgn libraries, including pgn<area>.NET (https://github.com/iigorr/pgn.net/tree/master) and 
PgnToFen (https://github.com/craylton/PgnToFen). 

Launch this project by double-clicking the `__LaunchThis-Chess.bat` file.

## NumberSonification
This is a simplified version of the previous project. The goal here is to have a basic interface that allows
either input of a irrational number or a set of music notes for direct playback. 

Launch this project by double-clicking the `__LaunchThis-Numbers.bat` file.

## Libraries
This folder, set at the top-level of the repository contains the shared files that the projects use. Please
update the files stored in these folders as they are shared and used by multiple projects. In order to easily
share between projects, we use symbolic links within the various project sub-folders that point back to the 
top-level sub-folder. However, dues to the sensitive nature of symbolic links, these are easily broken. This 
especially occurs when cloning from GH (and at other times). Fortunately there is a quick fix for this. Just 
double-click on the `fix_libraries.bat` file. Administrative permissions will probably be requested and the
rest is rather self-explanatory.

## Goal
The final purpose of this project is to generate nice sounding music! Let's have fun with this... 

## Software
### Visual Studio (Community 2019)  
* https://visualstudio.microsoft.com/
* Just C# stuff (".NET desktop development")

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

Project, ProjectName Properties..., Application, Output Type:, Console Application (to also have the console)
Check Tools, Options, Debugging, "Automatically close the console when debugging stops"


### VC Package Manager (VCPKG)
* https://github.com/microsoft/vcpkg
* There is probably no need to download this again. Follow the instructions in the [External Libs](ExternalLibs/README.md) directory to get this working.


## Folder Structure
There are four main folders in this repo:
### ChessSonification
* This is the main folder for the formal chess sonification simulator. There are not many files in this folder. Keep any project-specific files, data files, audio files, etc... in here. Generic libraries should go in the folders below.
### NumberSonification
* This is the main folder for the formal number sonification simulator. There are not many files in this folder. Keep any project-specific files, data files, audio files, etc... in here. Generic libraries should go in the folders below.
### Library/Chess
* Keep all custom libraries here. There is a pretty clear folder structure. It is possible to have multiple simulator projects running. These libraries are generic to any simulator.
### Library/MIDI
### PgnToFen
* Mostly this is VCPKG. But it also can be a place to play with/test external libraries.
* Stuff to read!

## Milestones and Tasks
I will keep a task list on GitHub in Milestone categories. Specifically:
1. (need to fill this in!)
2. 
3. 


