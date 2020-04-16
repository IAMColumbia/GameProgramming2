using UnityEngine;
using System.Collections;

public enum GhostState { Chasing, Evading, Roving, Dead }

public class Ghost
{

	public Vector2 Direction = new Vector2();
	
	protected GhostState state;
	public GhostState State
	{
		get { return state;}
		set { 
				if(this.state != value)		//only set value if state is changed
				{
					this.state = value;
					
					//if state is set to Roving pick random direction
					switch(State)
					{
					case GhostState.Roving :
						RandomDirection();
						break;
					case GhostState.Chasing :
						break;
					case GhostState.Evading :
						break;
					case GhostState.Dead :	
						this.Direction.x = this.Direction.y = 0;
						break;
					}
				}
			}
	}
	
    //Ghost class is dependant on the PacMan class
	public Ghost(PacMan pac)
	{
		this.State = GhostState.Roving;

	}

    public Ghost()
    {
        this.State = GhostState.Roving;
        //pac.Attach(this);
    }

	
	public void RandomDirection()
	{
		//TODO use Random from mono instead of unity
		this.Direction.x = Random.Range(-1,1);
		this.Direction.y = Random.Range(-1,1);
		//Check for Direction if there is none set it randomly
		if(this.Direction.sqrMagnitude == 0)
		{
			this.RandomDirection();
		}
	}

    public virtual void Log(string s)
    {
        //TODO Should be in subclass
        Debug.Log(s);
    }

    //From IObservable messages from PacMan
    public void ObserverUpdate(object sender, object message)
    {
        if (sender is PacMan)
        {
            if (message is string)
            {
                //TODO should be subclassed to avoid using Debug.Log in non unity specific class
                this.Log(string.Format("{0} recieved message from {1} : {2}", this, sender, message));
                switch (message.ToString())
                {
                    case "SuperPacMan" :
                        this.state = GhostState.Evading;
                        break;
                    case "SuperPacMan End" :
                        this.state = GhostState.Roving;
                        break;
                }
            }
            if (message is PacManState)
            {
                switch ((PacManState)message)
                {
                    case PacManState.SuperPacMan :
                        break;
                    case PacManState.Spawning :
                        break;
                    case PacManState.Chomping :
                        break;
                    case PacManState.Still :
                        break;
                }
            }
        }
    }
}


