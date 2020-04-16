using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCommand
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool playingGame = true;    //Bool used to loop unti Q is presse
            //Face Game Component
            GameComponent fakeComponentReceiver = new GameComponent();
            while (playingGame) //really bad game loop
            {
                //Read a key from a console prompt
                ConsoleKeyInfo keyI = AskForCommand();

                //Gets command based on keypress
                Command command = GetCommandFromKey(ref playingGame, keyI);
                if (command != null)
                {
                    command.Execute(fakeComponentReceiver);
                }
                else
                {
                    Console.WriteLine("Sorry I don't know that command");
                }

                Console.WriteLine(fakeComponentReceiver.About());
            }
        }

        private static Command GetCommandFromKey(ref bool playingGame, ConsoleKeyInfo keyI)
        {
            Command command = null;
            switch (keyI.Key)
            {
                case ConsoleKey.A:          //Move left
                case ConsoleKey.LeftArrow:
                    command = new Commands.MoveLeftCommand();
                    break;
                case ConsoleKey.D:          //Move right
                case ConsoleKey.RightArrow:
                    command = new Commands.MoveRightCommand();
                    break;
                case ConsoleKey.W:          //Move up
                case ConsoleKey.UpArrow:
                    command = new Commands.MoveUpCommand();
                    break;
                case ConsoleKey.S:          //Move down
                case ConsoleKey.DownArrow:
                    command = new Commands.MoveDownCommand();
                    break;
                case ConsoleKey.Escape:     //Exit game
                case ConsoleKey.Q:
                    playingGame = false;
                    break;

            }
            return command;
        }

        private static ConsoleKeyInfo AskForCommand()
        {
            Console.Write("Please enter a key:");
            ConsoleKeyInfo ki = Console.ReadKey();
            Console.WriteLine(""); //new line
            return ki;
        }
    }
}
