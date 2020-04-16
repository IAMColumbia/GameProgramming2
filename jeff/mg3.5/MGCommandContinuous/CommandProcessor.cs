using ConsoleCommandWUndo;
using ConsoleCommandWUndo.Commands;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MGCommand
{
    class CommandProcessor : GameComponent
    {

        InputHandler input;
        GameConsole console;

        KeyMap keyMap;

        //List of previously processed commands
        Stack<ICommand> Commands;

        Dictionary<string, GameComponent> componentMap;

        CommandPacMan pacCommandReciever;

        public object CommandWUndo { get; private set; }

        public CommandProcessor(Game game, GameComponent pac) : base (game)
        {
            Commands = new Stack<ICommand>();

            input = (InputHandler)game.Services.GetService<IInputHandler>();
            if(input == null)
            {
                input = new InputHandler(game);
                game.Components.Add(input);
            }
            console = (GameConsole)game.Services.GetService<IGameConsole>();
            if(console == null)
            {
                console = new GameConsole(game);
                game.Components.Add(console);
            }
            keyMap = new KeyMap();
            componentMap = new Dictionary<string, GameComponent>();

            this.pacCommandReciever = (CommandPacMan)pac;
        }

        public override void Update(GameTime gameTime)
        {
            //Check keys in keymap
            //Has Released Keys
            foreach (var item in keyMap.OnReleasedKeyMap)
            {
                if (input.KeyboardState.HasReleasedKey(item.Key))
                {
                    switch (item.Value)
                    {
                        case "Log Queue Size":
                            console.GameConsoleWrite(string.Format("Log size {0}", Commands.Count));
                            break;

                    }
                }
            }

            //Holding Key Down Map
            foreach (var item in keyMap.OnKeyDownMap)
            {
                if (input.KeyboardState.IsHoldingKey(item.Key))
                {
                    console.GameConsoleWrite(string.Format("onKeyDownMap Key held {0}", item.Value.ToString())); //Log key to console
                                                                                                                 /*switch (item.Value)
                                                                                                                 {
                                                                                                                     //nothing

                                                                                                                 }*/

                    //console.GameConsoleWrite(string.Format("onReleasedKeyMap Key released {0}", item.Value.ToString())); //Log key to console
                    Command command = null;
                    switch (item.Value)
                    {
                        case "Move Up":
                            //trigger Move Up command
                            command = new MoveUpCommand(this.Game);
                            break;
                        case "Move Down":
                            //trigger Move Down command
                            command = new MoveDownCommand(this.Game);
                            break;
                        case "Move Left":
                            //trigger Move Left command
                            command = new MoveLeftCommand(this.Game);
                            break;
                        case "Move Right":
                            //trigger Move Down command
                            command = new MoveRightCommand(this.Game);
                            break;
                        case "Undo":
                            if (Commands.Count > 0)
                            {
                                command = (Command)Commands.Pop();
                                if (command is ICommandWithUndo) //if the popped command has an undo command use it
                                {
                                    command = ((ICommandWithUndo)command).UndoCommand;
                                }
                            }
                            break;
                    }
                    if (command != null)
                    {
                        if (command is ICommandWithUndo)
                        {
                            Commands.Push((ICommandWithUndo)command); //only push commands with undo to the stack
                        }
                        command.Execute(pacCommandReciever);
                    }
                }
                if (input.KeyboardState.HasReleasedKey(item.Key))
                {
                    console.GameConsoleWrite(string.Format("onKeyDownMap Key released {0}", item.Value.ToString())); //Log key to console
                    /*switch (item.Value)
                    {
                       //nothing 
                    }*/
                }
            }


            base.Update(gameTime);
        }


        
    }
}
