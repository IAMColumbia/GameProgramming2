using MGPacManComponents.Pac;
using Microsoft.Xna.Framework;
using Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGObserver
{
    public class PacManSubject : MonogamePacMan, IPacmanSubject
    {


        protected List<IPacmanObserver> _observers;

        public PacManSubject(Game game) : base(game)
        {
            this._observers = new List<IPacmanObserver>();
        }

        public List<IPacmanObserver> observers
        {
            get
            {
                return _observers;
            }

            set
            {
                _observers = value;
            }
        }

        public void Attach(IObserver o)
        {
            //don't allow IObserver only allow IPacmanObservers 
            //this is by design 
            throw new NotImplementedException();
        }

        public void Attach(IPacmanObserver o)
        {
            this._observers.Add(o);
        }

        public void Detach(IObserver o)
        {
            //don't allow IObserver only allow IPacmanObservers 
            //this is by design
            throw new NotImplementedException();
        }

        public void Detach(IPacmanObserver o)
        {
            this._observers.Remove(o);
        }

        public void Notify()
        {
            foreach(IPacmanObserver o in _observers)
            {
                o.ObserverUpdate(this.PacState);
            }
        }

        protected override void pacStateChanged()
        {
            base.pacStateChanged();
            this.Notify();
        }
    }
}
