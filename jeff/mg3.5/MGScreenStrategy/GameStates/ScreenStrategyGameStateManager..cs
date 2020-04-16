using Microsoft.Xna.Framework;
using MonoGameLibrary.State;
using Screenz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGScreenStrategy.GameStates
{
    class ScreenStrategyGameStateManager : GameStateManager
    {

        public ITitleIntroState TitleIntroState; //First State
        public IStartMenuState StartMenuState;
        public IPlayingState PlayingState;
        public IPausedState PausedState;
        //Into Screen


        public ScreenStrategyGameStateManager(Game game) : base (game)
        {
            TitleIntroState = new TitleIntroState(game,this);
            StartMenuState = new StartMenuState(game, this);
            PausedState = new PausedState(game, this);
            PlayingState = new PlayingState(game, this, PausedState);
            

            this.ChangeState(TitleIntroState.Value);
        }
    }
}
