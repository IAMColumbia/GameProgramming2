using ConsoleCommandWUndo;
using MGAudioWCommandSingleton;
using MGAudioWCommandSingleton.Commands;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public object CommandWUndo { get; private set; }

        public CommandProcessor(Game game) : base (game)
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
            componentMap = new Dictionary<string, GameComponent>(); //TODO
            
        }

        public override void Initialize()
        {
            keyMap.Initalize();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            //Check keys in keymap
            //Has Released Keys
            foreach (var item in keyMap.OnReleasedKeyMap)
            {
                if (input.KeyboardState.HasReleasedKey(item.Key))
                {
                    console.GameConsoleWrite(string.Format("onReleasedKeyMap Key released {0}", item.Value.ToString())); //Log key to console
                    Command command = null;
                    switch (item.Value)
                    {
                        case "Volume Up":

                            command = new AudioCommand((AudioCommandProcessor)this.Game.Services.GetService<IAudioCommandProcessor>(),
                                 AudioSoundType.Command, AudioCommandType.Text, "Volume Up");
                            ((AudioCommand)command).Execute("Song Volume Up");
                            break;
                        case "Play GAMEBEGINNING":
                            command = new AudioCommand((AudioCommandProcessor)this.Game.Services.GetService<IAudioCommandProcessor>(),
                                AudioSoundType.SoundEffect, AudioCommandType.PlayReplace, "GAMEBEGINNING");
                            ((AudioCommand)command).Execute();
                            break;
                        case "Play Killed":
                            command = new AudioCommand((AudioCommandProcessor)this.Game.Services.GetService<IAudioCommandProcessor>(),
                                AudioSoundType.SoundEffect, AudioCommandType.PlayOneShot, "killed");
                            ((AudioCommand)command).Execute();
                            break;
                        default:
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
