using MGPacManComponents.Food;
using MGPacManComponents.Pac;
using Microsoft.Xna.Framework;
using MonoGameLibrary.GameFiles;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonFilesystem
{
    class FoodManagerLoadFromText : FoodManager
    {

        public string TextFilePath { get; set; }
        public string LevelText { get; set; }
        protected GameConsole console;



        public FoodManagerLoadFromText(Game game, MonogamePacMan p, string textFile) : base (game,p)
        {
            console = (GameConsole)this.Game.Services.GetService<IGameConsole>();
            if(console == null)
            {
                console = new GameConsole(game);
                this.Game.Components.Add(console);
            }

            this.TextFilePath = textFile;

            //load text file
            LevelText = FileSystem.Instance.ReadTextFile(this.TextFilePath);
            
            console.GameConsoleWrite(LevelText);
        }

        protected override void LoadLevel()
        {
            
            Vector2 startLoc = new Vector2(10, 10);

            xOffset = 50;
            yOffset = 50;

            //linees
            List<string> lines = LevelText.Split('\n').ToList();
            int LineCharsCount = lines[0].Length;

            foodGrid = new Vector2(lines.Count, LineCharsCount);
            bool[,] hasFood = new bool[lines.Count, LineCharsCount];
            //loop throug lines
            for (int i = 0; i < lines.Count-1; i++)
            {
                //loop through Chars
                for (int ii = 0; ii < LineCharsCount-1; ii++)
                {
                    switch((lines[i].ToCharArray())[ii])
                    {
                        case '1':
                            hasFood[i, ii] = true;
                            break;
                        default:
                            hasFood[i, ii] = false;
                            break;


                    }
                    
                }
            }


            for (int i = 0; i < foodGrid.X; i++)
            {
                for (int ii = 0; ii < foodGrid.Y; ii++)
                {
                    if (hasFood[i, ii])
                    {
                        Food f = new Food(this.Game);
                        f.Initialize();
                        f.Location = new Vector2(startLoc.X + (xOffset * i), startLoc.Y + (yOffset * ii));
                        foods.Add(f);
                    }
                }
            }
        }
    
    }
}
