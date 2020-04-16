using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSONOjbectMap;

public class TestTextReaders : MonoBehaviour
{

    public GhostManager Manager;

    public GameObject PacMan;
    
    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {


        Manager = new GhostManager(PacMan);

    }

    // Update is called once per frame
    void Update()
    {
        Manager.Update();
        if(Input.GetKeyUp(KeyCode.A))
        {
            Manager.State = GhostManager.GhostManagerState.StartAuto;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            Manager.State = GhostManager.GhostManagerState.Save;
        }
    }

    
}
