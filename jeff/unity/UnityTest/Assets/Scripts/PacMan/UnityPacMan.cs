using UnityEngine;
using System.Collections;

public class UnityPacMan : PacMan {

    private GameObject _gameObject;

    public UnityPacMan(GameObject g) : base()
    {
        _gameObject = g;
    }

    public override void Log(string s)
    {
        Debug.Log(s);
    }
	
}
