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
        public Dictionary<Keys, string> OnReleasedKeyMap, OnKeyDownMap;

        public KeyMap()
        {
            OnReleasedKeyMap = new Dictionary<Keys, string>();
            OnKeyDownMap = new Dictionary<Keys, string>();
            this.Initalize();
        }

        public virtual void Initalize()
        {

            OnReleasedKeyMap.Add(Keys.L, "Log Queue Size");

            //Released keys Map May load from text file
            OnKeyDownMap.Add(Keys.W, "Move Up");
            OnKeyDownMap.Add(Keys.Up, "Move Up");
            OnKeyDownMap.Add(Keys.S, "Move Down");
            OnKeyDownMap.Add(Keys.Down, "Move Down");
            OnKeyDownMap.Add(Keys.A, "Move Left");
            OnKeyDownMap.Add(Keys.Left, "Move Left");
            OnKeyDownMap.Add(Keys.D, "Move Right");
            OnKeyDownMap.Add(Keys.Right, "Move Right");
            OnKeyDownMap.Add(Keys.Z, "Undo");


            //Holding Key map maybe load from testfile
            //onKeyDownMap.Add(Keys.C, "Pac Chomp");

        }

    }
}