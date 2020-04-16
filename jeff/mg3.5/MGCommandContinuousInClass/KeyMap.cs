using ConsoleCommandWUndo;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGCommand
{
    class KeyMap
    {
        public Dictionary<Keys, CommName> OnReleasedKeyMap, OnKeyDownMap;

        public KeyMap()
        {
            OnReleasedKeyMap = new Dictionary<Keys, CommName>();
            OnKeyDownMap = new Dictionary<Keys, CommName>();
            this.Initalize();
        }

        public virtual void Initalize()
        {

            OnReleasedKeyMap.Add(Keys.L, CommName.LogQueueSize);

            //Released keys Map May load from text file
            OnKeyDownMap.Add(Keys.W, CommName.MoveUp);
            OnKeyDownMap.Add(Keys.Up, CommName.MoveUp);
            OnKeyDownMap.Add(Keys.S, CommName.MoveDown);
            OnKeyDownMap.Add(Keys.Down, CommName.MoveDown);
            OnKeyDownMap.Add(Keys.A, CommName.MoveLeft);
            OnKeyDownMap.Add(Keys.Left, CommName.MoveLeft);
            OnKeyDownMap.Add(Keys.D, CommName.MoveRight);
            OnKeyDownMap.Add(Keys.Right, CommName.MoveRight);
            OnKeyDownMap.Add(Keys.Z, CommName.Undo);


            //Holding Key map maybe load from testfile
            //onKeyDownMap.Add(Keys.C, "Pac Chomp");

        }

    }
}