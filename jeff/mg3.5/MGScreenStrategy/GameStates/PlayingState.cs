#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Util;
using MonoGameLibrary.State;
using MGPacManComponents.Pac;
using MGPacManComponents.Ghost;
#endregion

namespace Screenz
{
    public interface IPlayingState : IGameState { }

    class PlayingState : BaseGameState, IPlayingState
    {
        SpriteFont font;
        MonogamePacMan pacMan;
        MonogameGhost ghost;
        

        PausedState pausedState;

        DialogState initalDialog;
        
        public PlayingState(Game game, IGameStateManager manager, IPausedState pausedState)
            : base(game, manager)
        {
            game.Services.AddService(typeof(IPlayingState), this);

            pacMan = new MonogamePacMan(this.Game);
            pacMan.ShowMarkers = true;
            this.Game.Components.Add(pacMan);
            pacMan.Visible = false;

            initalDialog = new DialogState(this.Game, manager, "This is a dialog.\nPac is close to Ghost.\nPress ESC to dimsiss\nPress H for Help");

            ghost = new MonogameGhost(this.Game, pacMan);
            this.Game.Components.Add(ghost);
            ghost.Visible = false;
            ghost.Enabled = false;

            this.pausedState = (PausedState)pausedState;


        }

        public override void Update(GameTime gameTime)
        {
            if (Input.WasPressed(0, InputHandler.ButtonType.Back, Keys.Escape))
                GameManager.PushState(pausedState.Value);

            if (Input.WasPressed(0, InputHandler.ButtonType.A, Keys.R))
                initalDialog.ResetDialog(); //just a test to rest digalog


            //Check ghost proximity to pacman show dialong when they are close
            if (Vector2.Distance(pacMan.Location, ghost.Location) < 100)
            {
                if(initalDialog.DialogStatus == GameDialogStatus.None) //Only show the dialog once
                    GameManager.PushState(initalDialog.Value);
            }

            console.Log("dist", Vector2.Distance(pacMan.Location, ghost.Location).ToString());
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
           
            base.Draw(gameTime);
        }

        public override void StateChanged(object sender, EventArgs e)
        {
            base.StateChanged(sender, e);

            //handled change to paused state
            if (GameManager.State == pausedState)
            {
                //just set enabled to false;
                this.Enabled = false;
                pacMan.Enabled = false;
                ghost.Enabled = false;
            }
            else if (GameManager.State != this.Value)
            {
                //change to any other state
                Visible = false;
                Enabled = false;
                pacMan.Visible = false;
                pacMan.Enabled = false;
                ghost.Visible = false;
                ghost.Enabled = false;
                //Call Load or add components
                
            }
            else
            {
                //Change back to this state 
                pacMan.Visible = true; //pac visible
                pacMan.Enabled = true; //pac enabled
                ghost.Visible = true;
                ghost.Enabled = true;
                //Call Unload or remove components
            }
        }

        protected override void LoadContent()
        {
            font = this.Game.Content.Load<SpriteFont>(@"Arial");

            //stop ghost
            this.ghost.Speed = 0;


            base.LoadContent();
        }
    }
}
