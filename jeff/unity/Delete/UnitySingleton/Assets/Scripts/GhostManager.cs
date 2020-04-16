
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace JSONOjbectMap
{
    public class GhostManager 
    {

        JSONFileParser<JSONDTOMap> jsonGhostParser;
        XMLFileParser<XMLDTOMap> xmlGhostParser;
        //List<MonogameGhost> Ghosts;
        //List<MonogameGhost> ghostsToRemove;
        JSONDTOMap JSONmap;
        XMLDTOMap XMLmap;

       
        public enum GhostManagerState {  Start, StartAuto, Loading, Loaded, Setup, SetupFinished, Editing, Save, Saving, Saved };
        public delegate void GhostManagerStateChangedHandler(object sender,
                        GhostManagerStateEventArgs ghostState);
        public event GhostManagerStateChangedHandler StateChanged;

        GhostManagerState _state;
        public GhostManagerState State
        {
            get { return _state; }
            set
            {
                if(this._state != value)
                {
                    StateChanged.Invoke(this, new GhostManagerStateEventArgs() { NewState = value, OldState = this._state });
                    this._state = value;
                    
                }
            }
        }

        public GhostManager()
        {
            //this.Ghosts = new List<MonogameGhost>();
            //this.ghostsToRemove = new List<MonogameGhost>();
            //this.pac = pac;
            this.State = GhostManagerState.Start;
            this.StateChanged += GhostManager_StateChanged;

            jsonGhostParser = new JSONFileParser<JSONDTOMap>();
            //JSONDTOMap JSONmap = jsonGhostParser.LoadFromJSON("JSONMap.json", "Assets\\Resources\\");

            xmlGhostParser = new XMLFileParser<XMLDTOMap>();
            //XMLDTOMap XMLmap = xmlGhostParser.LoadFromXML("XMLMap.xml", "Assets\\Resources\\");

        }

        private void GhostManager_StateChanged(object sender, GhostManagerStateEventArgs e)
        {
            UnityEngine.Debug.Log($"GhostManager_StateChanged:{e.OldState}->{e.NewState}");
        }

        public  void Initialize()
        {
            
            if (this.State == GhostManagerState.Loading)
            {
                LoadGhostsFromJSON();
                LoadGhostsFromXML();
                SetupGhostsJSON();
                SetupGhostsXML();
            }  
        }

        public void Update()
        {
            switch(this.State)
            {
                case GhostManagerState.Start:
                    break;
                case GhostManagerState.StartAuto:
                    this.State = GhostManagerState.Loading;
                    break;
                case GhostManagerState.Loading:
                    LoadGhostsFromJSON();
                    this.State = GhostManagerState.Loaded;
                    SetupGhostsJSON();
                    break;
                case GhostManagerState.Loaded:
                    //CheckForEdit();
                    //CheckForLoad();
                    //nothing
                    break;
                case GhostManagerState.Setup:
                case GhostManagerState.SetupFinished:
                    CheckForEdit();
                    break;
                case GhostManagerState.Editing:
                    CheckForSave();
                    break;
                case GhostManagerState.Save:
                    
                    this.State = GhostManagerState.Saving;
                    break;
                case GhostManagerState.Saving:
                    //Save
                    this.State = GhostManagerState.Saved;
                    break;
                case GhostManagerState.Saved:
                    this.State = GhostManagerState.Editing;
                    break;
            }
            
        }

        private void CheckForLoad()
        {
#if DEBUG
            //if (input.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.L))
            //{
                this.State = GhostManagerState.Loading;
            //}

        }
#endif

        private void CheckForSave()
        {
#if DEBUG
            //if (input.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.S))
            //{
                this.State = GhostManagerState.Save;
            //}
#endif
        }

        /// <summary>
        /// Check to change to edit mode
        /// </summary>
        private void CheckForEdit()
        {
#if DEBUG
            //if (input.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.E))
            //{
                this.State = GhostManagerState.Editing;
            //}
#endif
        }

        private void SetupGhostsJSON()
        {
            if (JSONmap == null) throw new Exception("Map is null exception. Please Load a map.");
            this.State = GhostManagerState.Setup;
            foreach (Sprite s in JSONmap.Sprites )
            {
                UnityEngine.Debug.Log(s.ToString());

            //    MonogameGhost ghost = new MonogameGhost(this.Game, pac);
                
            //    ghost.Initialize();
            //    ghost.Ghost.State = GhostState.Roving;
            //    //Set parameters from JSON DTO object if they exist
            //    if (s.Location != null)
            //    {
            //        ghost.Location = new Vector2(s.Location.X, s.Location.Y);
            //    }
            //    else
            //    {
            //        //ghost location is already random
            //    }
            //    if(s.Direction != null)
            //    {
            //        ghost.Direction = new Vector2(s.Direction.X, s.Direction.Y);
            //    }
            //    else
            //    {
            //        ghost.Direction = new Vector2(0, 0);
            //    }
            //    ghost.Speed = s.Speed;
                
            //    this.Game.Components.Add(ghost);
            //    this.Ghosts.Add(ghost);
            }
            this.State = GhostManagerState.SetupFinished;
        }

        private void SetupGhostsXML()
        {
            //if (XMLmap == null) throw new Exception("Map is null exception. Please Load a map.");
            //this.State = GhostManagerState.Setup;
            //foreach (XMLDTOMapSprite s in XMLmap.Sprite)
            //{
            //    MonogameGhost ghost = new MonogameGhost(this.Game, pac);

            //    ghost.Initialize();
            //    ghost.Ghost.State = GhostState.Roving;
            //    //Set parameters from JSON DTO object if they exist
            //    if (s.Location != null)
            //    {
            //        ghost.Location = new Vector2(s.Location.X, s.Location.Y);
            //    }
            //    else
            //    {
            //        //ghost location is already random
            //    }
            //    if (s.Direction != null)
            //    {
            //        ghost.Direction = new Vector2(s.Direction.X, s.Direction.Y);
            //    }
            //    else
            //    {
            //        ghost.Direction = new Vector2(0, 0);
            //    }
            //    ghost.Speed = s.Speed;

            //    this.Game.Components.Add(ghost);
            //    this.Ghosts.Add(ghost);
            //}
            //this.State = GhostManagerState.SetupFinished;
        }

        private void LoadGhostsFromJSON()
        {
            this.State = GhostManagerState.Loading;
            JSONmap = jsonGhostParser.LoadFromJSON("JSONMap.json", "Assets\\Resources");
            
            //Sprite test2 = JsonUtility.FromJson<Sprite>("{\"Location\": {\"X\": \"100\",\"Y\": \"100\"},\"Direction\": {\"X\": \"0\",\"Y\": \"1\"},\"Speed\":  \"20\"}");
            this.State = GhostManagerState.Loaded;
        }

        private void LoadGhostsFromXML()
        {
            this.State = GhostManagerState.Loading;
            XMLmap = xmlGhostParser.LoadFromXML("XMLMap.xml", "Assets\\Resources");
            this.State = GhostManagerState.Loaded;
        }

        
        #region JSON DTO Classes
        [System.Serializable]
        public class JSONDTOMap
        {
            public Sprite[] Sprites;
        }

        [System.Serializable]
        public class Sprite
        {
            public Location Location;
            public Location Direction;
            public int Speed;
        }
        [System.Serializable]
        public class Location
        {
            public float X;
            public float Y;
        }

        public class GhostManagerStateEventArgs : EventArgs
        {
            public GhostManagerState OldState { get; set; }
            public GhostManagerState NewState { get; set; }
        }



        #endregion
        #region DTO XML CLasses


        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class XMLDTOMap
        {

            private XMLDTOMapSprite[] spriteField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Sprite")]
            public XMLDTOMapSprite[] Sprite
            {
                get
                {
                    return this.spriteField;
                }
                set
                {
                    this.spriteField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class XMLDTOMapSprite
        {

            private XMLDTOMapSpriteLocation locationField;

            private XMLDTOMapSpriteDirection directionField;

            private byte speedField;

            /// <remarks/>
            public XMLDTOMapSpriteLocation Location
            {
                get
                {
                    return this.locationField;
                }
                set
                {
                    this.locationField = value;
                }
            }

            /// <remarks/>
            public XMLDTOMapSpriteDirection Direction
            {
                get
                {
                    return this.directionField;
                }
                set
                {
                    this.directionField = value;
                }
            }

            /// <remarks/>
            public byte Speed
            {
                get
                {
                    return this.speedField;
                }
                set
                {
                    this.speedField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class XMLDTOMapSpriteLocation
        {

            private byte xField;

            private ushort yField;

            /// <remarks/>
            public byte X
            {
                get
                {
                    return this.xField;
                }
                set
                {
                    this.xField = value;
                }
            }

            /// <remarks/>
            public ushort Y
            {
                get
                {
                    return this.yField;
                }
                set
                {
                    this.yField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class XMLDTOMapSpriteDirection
        {

            private byte xField;

            private byte yField;

            /// <remarks/>
            public byte X
            {
                get
                {
                    return this.xField;
                }
                set
                {
                    this.xField = value;
                }
            }

            /// <remarks/>
            public byte Y
            {
                get
                {
                    return this.yField;
                }
                set
                {
                    this.yField = value;
                }
            }
        }


        #endregion

    }
}