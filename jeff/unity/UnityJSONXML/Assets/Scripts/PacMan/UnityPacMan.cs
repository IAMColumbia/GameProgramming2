using UnityEngine;
using System.Collections;

public class UnityPacMan : PacMan {

    private GameObject _gameObject;

    public UnityPacMan(GameObject g) : base()
    {
        _gameObject = g;
#if DEBUG
        this.Log("GameObject" + g.ToString());
#endif
    }

    public override void Log(string s)
    {
        Debug.Log(s);
    }
	
}
