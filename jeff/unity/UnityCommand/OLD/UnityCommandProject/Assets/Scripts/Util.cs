using UnityEngine;
using System.Collections;

public class Util : MonoBehaviour {

	static Vector3 bottomLeft; 
	static Vector3 topRight;
	static Rect cameraRect;
	
	// Use this for initialization
	void Start () {
		bottomLeft = GetComponent<Camera>().ScreenToWorldPoint(Vector3.zero);
		topRight = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(
			GetComponent<Camera>().pixelWidth, GetComponent<Camera>().pixelHeight));
			
		cameraRect = new Rect(
			bottomLeft.x,
			bottomLeft.y,
			topRight.x - bottomLeft.x,
			topRight.y - bottomLeft.y);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public static Vector3 BounceOffWalls(Vector3 position, float width, float height, ref Vector2 direction)
	{
		
        
        //if(cameraRect.xMin == cameraRect.x) throw new UnityException("No instance of Util in Scene");
		//if(!cameraRect.Contains(position))
		//{
			//keep ghost on screen
			if(position.x + (width) <= cameraRect.xMin)
			{
				Debug.Log(string.Format("{0} xMin {1} {2} {3} direction {4}", cameraRect.xMin, position, width, height, direction));
                direction.x *=-1;
			}
			if(position.x - (width) >= cameraRect.xMax)
			{
                Debug.Log(string.Format("{0} xMax {1} {2} {3} direction {4}", cameraRect.xMax, position, width, height, direction));
                direction.x *=-1;
			}
			if(position.y + (height)> cameraRect.yMax)
			{
                Debug.Log(string.Format("{0} yMax {1} {2} {3} direction {4}", cameraRect.yMax, position, width, height, direction));
                direction.y *=-1;
			}
			if(position.y - (height) < cameraRect.yMin)
			{
                Debug.Log(string.Format("{0} yMin {1} {2} {3} direction {4}", cameraRect.yMin, position, width, height, direction));
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
	
	
}
