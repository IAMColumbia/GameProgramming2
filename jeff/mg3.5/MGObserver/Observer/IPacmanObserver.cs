using MGPacManComponents.Pac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Observer
{
    public interface IPacmanObserver : IObserver
    {
        void ObserverUpdate(PacManState p);  //State from MGPacManComponents.Pac.PacMan
    }
}
