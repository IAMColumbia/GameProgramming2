using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MGCommand
{
    class KeyMap
    {
        
        public Dictionary<KeyCode, string> OnReleasedKeyMap, OnKeyDownMap;

        public KeyMap()
        {
            OnReleasedKeyMap = new Dictionary<KeyCode, string>();
            OnKeyDownMap = new Dictionary<KeyCode, string>();
            this.Initalize();
        }

        public virtual void Initalize()
        {

            //Released keys Map May load from text file
            OnReleasedKeyMap.Add(KeyCode.W, "Move Up");
            OnReleasedKeyMap.Add(KeyCode.UpArrow, "Move Up");
            OnReleasedKeyMap.Add(KeyCode.S, "Move Down");
            OnReleasedKeyMap.Add(KeyCode.DownArrow, "Move Down");
            OnReleasedKeyMap.Add(KeyCode.A, "Move Left");
            OnReleasedKeyMap.Add(KeyCode.LeftArrow, "Move Left");
            OnReleasedKeyMap.Add(KeyCode.D, "Move Right");
            OnReleasedKeyMap.Add(KeyCode.RightArrow, "Move Right");
            OnReleasedKeyMap.Add(KeyCode.Z, "Undo");


            //Holding Key map maybe load from testfile
            //onKeyDownMap.Add(Keys.C, "Pac Chomp");

        }

    }
}