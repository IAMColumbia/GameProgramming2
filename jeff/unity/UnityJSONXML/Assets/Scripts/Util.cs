using UnityEngine;
using System.Collections;
using Spawner;
using System.Collections.Generic;

public class Util : MonoBehaviour {

	static Vector3 bottomLeft; 
	static Vector3 topRight;
	static Rect cameraRect;

	static bool DebugOn = false;
	
	// Use this for initialization
	void Start () {
		bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
		topRight = Camera.main.ScreenToWorldPoint(new Vector3(
			Camera.main.pixelWidth, Camera.main.pixelHeight));
			
		cameraRect = new Rect(
			bottomLeft.x,
			bottomLeft.y,
			topRight.x - bottomLeft.x,
			topRight.y - bottomLeft.y);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static bool IsOnScreen ( GameObject go)
    {
        bool OnScreen = true;
        float width = 0; //should get this from spriteRenderer
        float height = 0; //should get this from spriteRederer
        if (((go.transform.position.x + (width)) < cameraRect.xMin) 
            || ((go.transform.position.x - (width)) > cameraRect.xMax)
            || ((go.transform.position.y + (height)) > cameraRect.yMax)
            || ((go.transform.position.y - (height)) < cameraRect.yMin) 
         )
         {
            OnScreen = false;
         }
        return OnScreen;
    }
	
	public static Vector3 BounceOffWalls(Vector3 position, float width, float height, ref Vector2 direction)
	{
		
        
        //if(cameraRect.xMin == cameraRect.x) throw new UnityException("No instance of Util in Scene");
		//if(!cameraRect.Contains(position))
		//{
			//keep ghost on screen
			if(position.x + (width) <= cameraRect.xMin)
			{
				if(DebugOn)Debug.Log(string.Format("{0} xMin {1} {2} {3} direction {4}", cameraRect.xMin, position, width, height, direction));
                direction.x *=-1;
			}
			if(position.x - (width) >= cameraRect.xMax)
			{
				if(DebugOn)Debug.Log(string.Format("{0} xMax {1} {2} {3} direction {4}", cameraRect.xMax, position, width, height, direction));
                direction.x *=-1;
			}
			if(position.y + (height)> cameraRect.yMax)
			{
				if(DebugOn)Debug.Log(string.Format("{0} yMax {1} {2} {3} direction {4}", cameraRect.yMax, position, width, height, direction));
                direction.y *=-1;
			}
			if(position.y - (height) < cameraRect.yMin)
			{
				if (DebugOn) Debug.Log(string.Format("{0} yMin {1} {2} {3} direction {4}", cameraRect.yMin, position, width, height, direction));
                direction.y *=-1;
			}
			position = new Vector3(
				Mathf.Clamp(position.x, cameraRect.xMin, cameraRect.xMax),
				Mathf.Clamp(position.y, cameraRect.yMin, cameraRect.yMax), position.z);
		//}
		return position;
	}

	//Logs Error on GetComponent Call the return null
	public static void GetComponentIfNull< T >( MonoBehaviour that, ref T cachedT ) where T : Component
	{
		if( cachedT == null )
		{
			cachedT = (T)that.GetComponent( typeof( T ) );
			if( cachedT == null )
			{
				Debug.LogWarning( "GetComponent of type " + typeof( T ) + " failed on " + that.name, that );
			}
		}
	}
	
	public static int GetRandom(int max)
	{
		return Random.Range(1,max);
	}

    public void ToggleAllSpawners()
    {
        var MonoObjects = GameObject.FindObjectsOfType(typeof(MonoBehaviour));
        List<ISpawner> spawners = new List<ISpawner>();
        foreach(MonoBehaviour m in MonoObjects)
        {
            if(m is  ISpawner)
            {
                spawners.Add((ISpawner)m);
            }
        }
        foreach (ISpawner s in spawners)
        {
            if (s.SpawnerEnabled)
                s.SpawnerEnabled = false;
            else
                s.SpawnerEnabled = true;
        }

    }
	
	
}
