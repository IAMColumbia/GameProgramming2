using UnityEngine;
using System.Collections;

using System.Collections.Generic;


public class Util : MonoBehaviour
{

    static Vector3 bottomLeft;
    static Vector3 topRight;
    static Rect cameraRect;

    static bool showDebug = true;  //turns on and off console logging

    // Use this for initialization
    void Start()
    {
        bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        topRight =Camera.main.ScreenToWorldPoint(new Vector3(
            Camera.main.pixelWidth,Camera.main.pixelHeight));

        cameraRect = new Rect(
            bottomLeft.x,
            bottomLeft.y,
            topRight.x - bottomLeft.x,
            topRight.y - bottomLeft.y);
    }

    // Update is called once per frame
    void Update()
    {

        Profile.StartProfile("Util");
        //my code
        Profile.EndProfile("Util");
    }

    void OnApplicationQuit()
    {

        Profile.PrintResults();
    }

    public static Vector3 BounceOffWalls(Vector3 position, float width, float height, ref Vector2 direction)
    {
        //if(cameraRect.xMin == cameraRect.x) throw new UnityException("No instance of Util in Scene");
        if(!cameraRect.Contains(position))
        {
            //keep  on screen
            if (position.x  <= cameraRect.xMin  )
            {
                if (showDebug) Debug.Log(string.Format("left xMin {0} pos {1} w {2} h {3} direction {4}", cameraRect.xMin, position, width, height, direction));
                direction.x *= -1;
            }
            if (position.x >= cameraRect.xMax )
            {
                if (showDebug) Debug.Log(string.Format("right xMin {0} pos {1} w {2} h {3} direction {4}", cameraRect.xMin, position, width, height, direction));
                direction.x *= -1;
            }
            if (position.y  > cameraRect.yMax )
            {
                if (showDebug) Debug.Log(string.Format("top xMin {0} pos {1} w {2} h {3} direction {4}", cameraRect.xMin, position, width, height, direction));
                direction.y *= -1;
            }
            if (position.y  < cameraRect.yMin )
            {

                if (showDebug) Debug.Log(string.Format("bottom xMin {0} pos {1} w {2} h {3} direction {4}", cameraRect.xMin, position, width, height, direction));
                direction.y *= -1;
            }
            position.x = Mathf.Clamp(position.x, cameraRect.xMin, cameraRect.xMax);
            position.y = Mathf.Clamp(position.y, cameraRect.yMin, cameraRect.yMax);
            if (showDebug) Debug.Log(string.Format("corected position {0} direction {1}", position, direction));
        }
        return position;
    }

    //Logs Error on GetComponent Call the return null
    public static void GetComponentIfNull<T>(MonoBehaviour that, ref T cachedT) where T : Component
    {
        if (cachedT == null)
        {
            cachedT = (T)that.GetComponent(typeof(T));
            if (cachedT == null)
            {
                Debug.LogWarning("GetComponent of type " + typeof(T) + " failed on " + that.name, that);
            }
        }
    }

    public static int GetRandom(int max)
    {
        return Random.Range(1, max);
    }

    public class Profile
    {
        public struct ProfilePoint
        {
            public System.DateTime lastRecorded;
            public System.TimeSpan totalTime;
            public int totalCalls;
        }

        private static Dictionary<string, ProfilePoint> profiles = new Dictionary<string, ProfilePoint>();
        private static System.DateTime startTime = System.DateTime.UtcNow;

        private Profile()
        {
        }

        public static void StartProfile(string tag)
        {
#if UNITY_EDITOR
            ProfilePoint point;

            profiles.TryGetValue(tag, out point);
            point.lastRecorded = System.DateTime.UtcNow;
            profiles[tag] = point;
#endif
        }

        public static void EndProfile(string tag)
        {
#if UNITY_EDITOR
            if (!profiles.ContainsKey(tag))
            {
                Debug.LogError("Can only end profiling for a tag which has already been started (tag was " + tag + ")");
                return;
            }
            ProfilePoint point = profiles[tag];
            point.totalTime += System.DateTime.UtcNow - point.lastRecorded;
            ++point.totalCalls;
            profiles[tag] = point;
#endif
        }

        public static void Reset()
        {
            profiles.Clear();
            startTime = System.DateTime.UtcNow;
        }

        public static void PrintResults()
        {
#if UNITY_EDITOR
            System.TimeSpan endTime = System.DateTime.UtcNow - startTime;
            System.Text.StringBuilder output = new System.Text.StringBuilder();
            output.Append("\nProfile results:");
            foreach (KeyValuePair<string, ProfilePoint> pair in profiles)
            {
                double totalTime = pair.Value.totalTime.TotalSeconds;
                int totalCalls = pair.Value.totalCalls;
                if (totalCalls < 1) continue;
                output.Append("\nProfile ");
                output.Append(pair.Key);
                output.Append(" took ");
                output.Append(totalTime.ToString("F5"));
                output.Append(" seconds to complete over ");
                output.Append(totalCalls);
                output.Append(" iteration");
                if (totalCalls != 1) output.Append("s");
                output.Append(", averaging ");
                output.Append((totalTime / totalCalls).ToString("F5"));
                output.Append(" seconds per call");
            }
            output.Append("\nTotal runtime: ");
            output.Append(endTime.TotalSeconds.ToString("F3"));
            output.Append(" seconds");
            Debug.Log(output.ToString());
#endif
        }
    }

    public class HRProfile
    {
        public struct ProfilePoint
        {
            public System.DateTime lastRecorded;
            public System.TimeSpan totalTime;
            public int totalCalls;
        }

        private static Dictionary<string, ProfilePoint> profiles = new Dictionary<string, ProfilePoint>();
        private static System.DateTime startTime = System.DateTime.UtcNow;
        private static System.DateTime _startTime;
        private static System.Diagnostics.Stopwatch _stopWatch = null;
        private static System.TimeSpan _maxIdle = System.TimeSpan.FromSeconds(10);

        private HRProfile()
        {
        }

        public static System.DateTime UtcNow
        {
            get
            {
                if (_stopWatch == null || startTime.Add(_maxIdle) < System.DateTime.UtcNow)
                {
                    _startTime = System.DateTime.UtcNow;
                    _stopWatch = System.Diagnostics.Stopwatch.StartNew();
                }
                return _startTime.AddTicks(_stopWatch.Elapsed.Ticks);
            }
        }

        public static void StartProfile(string tag)
        {
            ProfilePoint point;

            profiles.TryGetValue(tag, out point);
            point.lastRecorded = UtcNow;
            profiles[tag] = point;
        }

        public static void EndProfile(string tag)
        {
            if (!profiles.ContainsKey(tag))
            {
                UnityEngine.Debug.LogError("Can only end profiling for a tag which has already been started (tag was " + tag + ")");
                return;
            }
            ProfilePoint point = profiles[tag];
            point.totalTime += UtcNow - point.lastRecorded;
            ++point.totalCalls;
            profiles[tag] = point;
        }

        public static void Reset()
        {
            profiles.Clear();
            startTime = System.DateTime.UtcNow;
        }

        public static void PrintResults()
        {
            System.TimeSpan endTime = System.DateTime.UtcNow - startTime;
            System.Text.StringBuilder output = new System.Text.StringBuilder();
            output.Append("\nProfile results:");
            foreach (KeyValuePair<string, ProfilePoint> pair in profiles)
            {
                double totalTime = pair.Value.totalTime.TotalSeconds;
                int totalCalls = pair.Value.totalCalls;
                if (totalCalls < 1) continue;
                output.Append("\nProfile ");
                output.Append(pair.Key);
                output.Append(" took ");
                output.Append(totalTime.ToString("F9"));
                output.Append(" seconds to complete over ");
                output.Append(totalCalls);
                output.Append(" iteration");
                if (totalCalls != 1) output.Append("s");
                output.Append(", averaging ");
                output.Append((totalTime / totalCalls).ToString("F9"));
                output.Append(" seconds per call");
            }
            output.Append("\nTotal runtime: ");
            output.Append(endTime.TotalSeconds.ToString("F3"));
            output.Append(" seconds");
            UnityEngine.Debug.Log(output.ToString());
        }
    }

}

