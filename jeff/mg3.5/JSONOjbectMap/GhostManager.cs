using MGPacManComponents.Ghost;
using MGPacManComponents.Pac;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONOjbectMap
{
    public class GhostManager : GameComponent
    {

        JSONFileParser<JSONDTOMap> jsonGhostParser;
        XMLFileParser<XMLDTOMap> xmlGhostParser;
        List<MonogameGhost> Ghosts;
        List<MonogameGhost> ghostsToRemove;
        JSONDTOMap JSONmap;
        XMLDTOMap XMLmap;


        MonogamePacMan pac;

        //Services
        GameConsole console;
        InputHandler input;

        public enum GhostManagerState {  Loading, Loaded, Setup, SetupFinished, Editing, Save, Saving, Saved };
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

        public GhostManager(Game game, MonogamePacMan pac) : base(game)
        {
            jsonGhostParser = new JSONFileParser<JSONDTOMap>(this.Game);
            xmlGhostParser = new XMLFileParser<XMLDTOMap>(this.Game);
            this.Ghosts = new List<MonogameGhost>();
            this.ghostsToRemove = new List<MonogameGhost>();
            this.pac = pac;
            this.State = GhostManagerState.Loading;
            this.StateChanged += GhostManager_StateChanged;

            console = (GameConsole)this.Game.Services.GetService<IGameConsole>();
            if (console == null)
            {
                console = new GameConsole(game);
                this.Game.Components.Add(console);
                console.GameConsoleWrite($"Game console added by {this}");
            }
            input = (InputHandler)this.Game.Services.GetService<IInputHandler>();
            if (input == null)
            {
                input = new InputHandler(game);
                this.Game.Components.Add(input);
                console.GameConsoleWrite($"InputHandler added by {this}");
            }

        }

        private void GhostManager_StateChanged(object sender, GhostManagerStateEventArgs e)
        {
            console.GameConsoleWrite($"GhostManager_StateChanged:{e.OldState}->{e.NewState}");
        }

        public override void Initialize()
        {
            base.Initialize();
            if (this.State == GhostManagerState.Loading)
            {
                LoadGhostsFromJSON();
                LoadGhostsFromXML();
                SetupGhostsJSON();
                SetupGhostsXML();
            }  
        }

        public override void Update(GameTime gameTime)
        {
            switch(this.State)
            {
                case GhostManagerState.Loading:
                    LoadGhostsFromJSON();
                    this.State = GhostManagerState.Loaded;
                    SetupGhostsJSON();
                    break;
                case GhostManagerState.Loaded:
                    CheckForEdit();
                    CheckForLoad();
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
            
            base.Update(gameTime);
        }

        private void CheckForLoad()
        {
#if DEBUG
            if (input.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.L))
            {
                this.State = GhostManagerState.Loading;
            }

        }
#endif

        private void CheckForSave()
        {
#if DEBUG
            if (input.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.S))
            {
                this.State = GhostManagerState.Save;
            }
#endif
        }

        /// <summary>
        /// Check to change to edit mode
        /// </summary>
        private void CheckForEdit()
        {
#if DEBUG
            if (input.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.E))
            {
                this.State = GhostManagerState.Editing;
            }
#endif
        }

        private void SetupGhostsJSON()
        {
            if (JSONmap == null) throw new Exception("Map is null exception. Please Load a map.");
            this.State = GhostManagerState.Setup;
            foreach (Sprite s in JSONmap.Sprites )
            {
                MonogameGhost ghost = new MonogameGhost(this.Game, pac);
                
                ghost.Initialize();
                ghost.Ghost.State = GhostState.Roving;
                //Set parameters from JSON DTO object if they exist
                if (s.Location != null)
                {
                    ghost.Location = new Vector2(s.Location.X, s.Location.Y);
                }
                else
                {
                    //ghost location is already random
                }
                if(s.Direction != null)
                {
                    ghost.Direction = new Vector2(s.Direction.X, s.Direction.Y);
                }
                else
                {
                    ghost.Direction = new Vector2(0, 0);
                }
                ghost.Speed = s.Speed;
                
                this.Game.Components.Add(ghost);
                this.Ghosts.Add(ghost);
            }
            this.State = GhostManagerState.SetupFinished;
        }

        private void SetupGhostsXML()
        {
            if (XMLmap == null) throw new Exception("Map is null exception. Please Load a map.");
            this.State = GhostManagerState.Setup;
            foreach (XMLDTOMapSprite s in XMLmap.Sprite)
            {
                MonogameGhost ghost = new MonogameGhost(this.Game, pac);

                ghost.Initialize();
                ghost.Ghost.State = GhostState.Roving;
                //Set parameters from JSON DTO object if they exist
                if (s.Location != null)
                {
                    ghost.Location = new Vector2(s.Location.X, s.Location.Y);
                }
                else
                {
                    //ghost location is already random
                }
                if (s.Direction != null)
                {
                    ghost.Direction = new Vector2(s.Direction.X, s.Direction.Y);
                }
                else
                {
                    ghost.Direction = new Vector2(0, 0);
                }
                ghost.Speed = s.Speed;

                this.Game.Components.Add(ghost);
                this.Ghosts.Add(ghost);
            }
            this.State = GhostManagerState.SetupFinished;
        }

        private void LoadGhostsFromJSON()
        {
            this.State = GhostManagerState.Loading;
            JSONmap = jsonGhostParser.LoadFromJSON("JSONMap.json");
            this.State = GhostManagerState.Loaded;
        }

        private void LoadGhostsFromXML()
        {
            this.State = GhostManagerState.Loading;
            XMLmap = xmlGhostParser.LoadFromXML("XMLMap.xml");
            this.State = GhostManagerState.Loaded;
        }

        #region JSON DTO Classes
        public class JSONDTOMap
        {
            public Sprite[] Sprites { get; set; }
        }

        public class Sprite
        {
            public Location Location { get; set; }
            public Location Direction { get; set; }
            public int Speed { get; set; }
        }

        public class Location
        {
            public float X { get; set; }
            public float Y { get; set; }
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