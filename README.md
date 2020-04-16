# GameProgramming2
Game Progrmamming 2 @IAMColumbia

The jeff folder contains all my samples for Design patterns for Game Programming

- mg3.5 Monogame 3.5 and Windows command line samples 
- unity Unity 5.4.x samples I try to include a unity package for each sample

The monogame samples all rely on the MonogameLibrary built in https://github.com/dsp56001/GameProgramming
Some of the Pacman and Ghost samples form this course are encasultaed in the https://github.com/dsp56001/GameProgramming2/tree/master/jeff/mg3.5/MGPacManComponents project this allows wasy access to the components to simplify some the monogame samples

##Command Pattern

The console samples demostrate simple move commands invoked by the console command processor. In monogame this becomes a a game component and in unity this becomes a Monobehaviour. The commands in the console sample are invoked on a fake game component class. In Monogame this becomes a Monogame game component in Unity this is refactored to be a GameObject

Console Samples

There are two console samples. The first is a simple keymap the invoked move commands on a fake game object. The CommandWUndo add another type of command that can be undone. The command processor can proccess both the original command and the command with undo.

* https://github.com/dsp56001/GameProgramming2/tree/master/jeff/mg3.5/ConsoleCommand
* https://github.com/dsp56001/GameProgramming2/tree/master/jeff/mg3.5/ConsoleCommandWUndo

Monogame Sample

This sample swaps a real gamecomponent in Monogame for the fake in the console version. The code is slightly refactored for the monogame game component.

* https://github.com/dsp56001/GameProgramming2/tree/master/jeff/mg3.5/MGCommand

Unity Sample

This sample swaps a unity GameOject for the fake game component in the condsole version. The code is slightly refactores for the game object

* https://github.com/dsp56001/GameProgramming2/tree/master/jeff/unity/UnityCommand

##Strategy Pattern

Console Sample

Sample the makes chracter classes and weapons interchangable.


* https://github.com/dsp56001/GameProgramming2/tree/master/jeff/mg3.5/ConsoleApplicationStrategyWeapons

Monogame Sample

This is the similar classes to the console but allows pacman to pickup colored food weapons that change the pacmans color. 

* https://github.com/dsp56001/GameProgramming2/tree/master/jeff/mg3.5/PacManWeaponsStrategy

Unity Sample
* TODO

###Singleton

*Console Sample

*Monogame Sample

*Unity Sample

###Monogame touch samples

=======
# GameProgramming2
Game Progrmamming 2 @IAMColumbia
