using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.State;
using MonoGameLibrary.Util;
using Screenz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGScreenStrategy.GameStates
{
    public interface IHelpDiaglogState : IGameState { }

    class HelpDiaglogState : BaseGameState, IHelpDiaglogState
    {
        private Texture2D pausedTexture;
        private SpriteFont font;

        public int BorderSize;
        public string Text;

        public GameDialogStatus DialogStatus;
        public Color BackGrongColor, TextColor;

        HelpDiaglogState helpDialog;

        public HelpDiaglogState(Game game, IGameStateManager manager, string Text)
            : base(game, manager)
        {
            game.Services.AddService(typeof(IHelpDiaglogState), this);
            this.BorderSize = 100;
            this.Text = Text;
            this.DialogStatus = GameDialogStatus.None;
            this.BackGrongColor = Color.DarkRed;
            this.TextColor = Color.BlanchedAlmond;

            helpDialog = new HelpDiaglogState(game, manager, "help");   
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
                GameManager.PushState( helpDialog);


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

