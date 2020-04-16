using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.State;
using MonoGameLibrary.Util;
using Microsoft.Xna.Framework.Input;
using MGScreenStrategy.GameStates;

namespace Screenz
{

    public enum GameDialogStatus {  None, Open, Dismissed }

    public interface IDialogState : IGameState { }

    class DialogState : BaseGameState, IDialogState
    {

        private Texture2D pausedTexture;
        private SpriteFont font;

        public int BorderSize;
        public string Text;

        public GameDialogStatus DialogStatus;
        public Color BackGrongColor, TextColor;

        HelpDiaglogState helpDialog;

        public DialogState(Game game, IGameStateManager manager, string Text)
            : base(game, manager)
        {
            game.Services.AddService(typeof(IDialogState), this);
            this.BorderSize = 50;
            this.Text = Text;
            this.DialogStatus = GameDialogStatus.None;
            this.BackGrongColor = Color.BlanchedAlmond;
            this.TextColor = Color.DarkSlateGray;

            this.helpDialog = new HelpDiaglogState(this.Game, manager, "Help for dialog");
        }


        protected override void LoadContent()
        {
            this.pausedTexture = this.Game.Content.Load<Texture2D>(@"Transparent75Percent");
            this.font = this.Game.Content.Load<SpriteFont>("Arial");
            base.LoadContent();

        }

        public override void Update(GameTime gameTime)
        {
            if (Input.WasPressed(0, InputHandler.ButtonType.Back, Keys.Escape))
                GameManager.PopState();

            if (Input.WasPressed(0, InputHandler.ButtonType.A, Keys.H))
                GameManager.PushState(helpDialog);


            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Rectangle window = new Rectangle(0 + BorderSize, 0 + BorderSize,
                this.Game.Window.ClientBounds.Width - (BorderSize * 2), this.Game.Window.ClientBounds.Height - (BorderSize * 2));
            SpriteBatch.Begin();
            this.SpriteBatch.Draw(pausedTexture, window, this.BackGrongColor);
            this.SpriteBatch.DrawString(font, this.Text, new Vector2(100, 150), this.TextColor);
            SpriteBatch.End();
            base.Draw(gameTime);
        }

        public void ResetDialog()
        {
            this.DialogStatus = GameDialogStatus.None;
        }

        public override void StateChanged(object sender, EventArgs e)
        {
            base.StateChanged(sender, e);
            if(GameManager.State == helpDialog)
            {
                this.Visible = true; //Keep onscreen with help dialog
            }

            //handled change to paused state
            if (GameManager.State != this.Value)
            {
                //state navigated to set to open
                this.DialogStatus = GameDialogStatus.Open;
            }
            else
            {
                //leaving this state set to dismissed
                this.DialogStatus = GameDialogStatus.Dismissed;
            }
        }

    }
}
