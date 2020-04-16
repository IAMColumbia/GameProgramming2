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
            //this.Initalize();
        }

        public virtual void Initalize()
        {

            //Released keys Map May load from text file
            OnReleasedKeyMap.Add(Keys.U, "Volume Up");
            OnReleasedKeyMap.Add(Keys.D, "Volume Down");
            OnReleasedKeyMap.Add(Keys.S, "Play GAMEBEGINNING");
            OnReleasedKeyMap.Add(Keys.K, "Play Killed");


            //Holding Key map maybe load from testfile
            //onKeyDownMap.Add(Keys.C, "Pac Chomp");

        }

    }
}