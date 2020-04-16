using UnityEngine;
using System.Collections;

public interface IObserver {

    //unity has an object called Object that is in the default namespace we need to specify System.Object
    //Might be better to string type this for our game
    void ObserverUpdate(System.Object sender, System.Object message);
}

