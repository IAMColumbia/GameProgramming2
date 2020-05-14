using UnityEngine;
using System.Collections.Generic;


public enum PacManState { Spawning, Still, Chomping, SuperPacMan }

public class PacMan : ISubject {

    //From ISubject
    List<IObserver> Ghosts;
    
    protected PacManState _state;
    public PacManState State { 
        get { return _state; } 
        set {
                if (_state != value)
                {
                    this.Log (string.Format("{0} was: {1} now {2}", this.ToString(), _state, value));
                    _state = value;
                    this.Notify();
                    
                }
            } 
    }

    public PacMan()
    {
        //Create List of Observers
        this.Ghosts = new List<IObserver>();
        //Set default state will call notify so make sure this.Ghosts is intitialized first
        this.State = PacManState.Still;
        
    }

    public virtual void Log(string s)
    {
        //nothing
    }

    //From ISubect add a Ghost that is interested in receiving messages from PacMan
    public void Attach(IObserver o)
    {
        this.Ghosts.Add(o);
    }

    //From ISubject Removes a Ghost from the list of Ghosts odserving PacMan
    public void Detach(IObserver o)
    {
        this.Ghosts.Remove(o);
        
        //Shouldn't need this????
        int indexOfFirstDeadGHost = 0;
        foreach (Ghost g in Ghosts){
            if(g.State == GhostState.Dead)
            {
                indexOfFirstDeadGHost = Ghosts.IndexOf(g);
                this.Ghosts.RemoveAt(indexOfFirstDeadGHost);
            }
        }
        
    }
    

    public void Notify()
    {
        foreach (IObserver o in Ghosts)
        {
            o.ObserverUpdate(this, this.State);
        }
    }
}

