using Microsoft.Xna.Framework;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input.Touch;

namespace MGTouch
{
    
    class TouchInputHandler : InputHandler, ITouchInputHander
    {
        GameConsole console;  //gameconsole depends on InputHandler can't have cyclic redundancy this need to be optional

        TouchCollection touchStateCollection;

        VirtualThumbstick VirtualThumbstickL, VirtualThumbstickR;
        TouchLocation firstTouchLeft, firstTouchRight;

        GestureSample gesture;
        protected GestureSample CurrentGesture
        {
            get { return gesture; }
            set
            {
                if (gesture.Timestamp != value.Timestamp)
                {
                    gesture = value;
                    GestureUpdate.Invoke(gesture);   
                }
                if (gesture.GestureType != value.GestureType)
                {
                    GestureChanged.Invoke(gesture, gesture.GestureType);
                }
            }
        }

        public delegate void TouchInputHandlerGestureChangedHandler(GestureSample gesture,
                        GestureType gestureType);
        public event TouchInputHandlerGestureChangedHandler GestureChanged;

        public delegate void TouchInputHandlerGestureDataUpdateHandler(GestureSample gesture);
        public event TouchInputHandlerGestureDataUpdateHandler GestureUpdate;

        public delegate void TouchInputHandlerTouchUpdateHandler(TouchLocation touch);
        public event TouchInputHandlerTouchUpdateHandler TouchUpdate;


        //objectpool
        //List<TouchLocation> touchLocationPool;

        public TouchInputHandler(Game game) : base (game)
        {
            //touchLocationPool = new List<TouchLocation>();

            TouchPanel.EnabledGestures =  GestureType.DoubleTap |  GestureType.Tap;
            //GestureType.FreeDrag | GestureType.Pinch | GestureType.PinchComplete | GestureType.DragComplete | GestureType.HorizontalDrag | GestureType.VerticalDrag | GestureType.Flick |

            GestureChanged += TouchInputHandler_GestureChanged;
            GestureUpdate += TouchInputHandler_GestureUpdate;

            TouchUpdate += TouchInputHandler_TouchUpdate;
       }

        private void TouchInputHandler_TouchUpdate(TouchLocation touch)
        {
            logString = $"{touch.Id}, {touch.Position} {touch.State}";
            if (touch.TryGetPreviousLocation(out prevLocaton))
            {
                logString += $" : prev {prevLocaton.Id}, {prevLocaton.Position}";
            }
            WriteToConsole(logString);
        }

        private void TouchInputHandler_GestureUpdate(GestureSample gesture)
        {
            //this.WriteToConsole($"TouchInputHandler_GestureUpdate: {gesture.Delta} {gesture.Delta2}");
        }

        private void TouchInputHandler_GestureChanged(GestureSample gesture, GestureType gestureType)
        {
            this.WriteToConsole($"TouchInputHandler_GestureChanged: {gestureType}");
        }

        public void AddGameConsole(GameConsole console)
        {
            this.console = console;

            //add thumbsticks when adding console
            WriteToConsole("Console added to TouchInputHandler");
            VirtualThumbstickL = new VirtualThumbstick(this.Game);
            this.Game.Components.Add(VirtualThumbstickL);
            VirtualThumbstickL.Enabled = false;
            VirtualThumbstickL.Visible = false;

            VirtualThumbstickR = new VirtualThumbstick(this.Game);
            this.Game.Components.Add(VirtualThumbstickR);
            VirtualThumbstickR.Enabled = false;
            VirtualThumbstickR.Visible = false;
        }


        /// <summary>
        /// Wraps console write can depend on it uses game console if it exisits. Writes to output is no gameconsole
        /// </summary>
        /// <param name="s"></param>
        private void WriteToConsole(string s)
        {
            if (console != null)
            {
                this.console.GameConsoleWrite(s);
            }
            else
            {
                Console.WriteLine($"GameConsole missing : {s}");
            }
        }

        /// <summary>
        /// Wraps console write can depend on it uses game console if it exisits. Writes to output is no gameconsole
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void LogToConsole(string key, string value)
        {
            if (console != null)
            {
                this.console.Log(key, value) ;
            }
            else
            {
                Console.WriteLine($"GameConsole missing : {key} : {value}");
            }
        }

        

        TouchLocation prevLocaton;
        string logString;
        public override void Update(GameTime gameTime)
        {
            if(midX == 0) midX = this.Game.GraphicsDevice.Viewport.Width / 2;
            // Update touch panel state
            touchStateCollection = TouchPanel.GetState();

            foreach (TouchLocation t in touchStateCollection)
            {
                TouchUpdate.Invoke(t);
                
                CheckVirtualThumbsticks(t, prevLocaton);
                
            }

            LogToConsole("IsGestureAvailable", TouchPanel.IsGestureAvailable.ToString());
            //gestures
            while (TouchPanel.IsGestureAvailable)
            {
                CurrentGesture = TouchPanel.ReadGesture();
                LogToConsole("GestureType", gesture.GestureType.ToString());
                if (CurrentGesture.GestureType == GestureType.VerticalDrag)
                {
                    if (gesture.Delta.Y < 0) { }
                    //return new RotateLeftCommand(_gameController);
                    if (gesture.Delta.Y > 0) { }
                        //return new RotateRightCommand(_gameController);
                }

                if (CurrentGesture.GestureType == GestureType.HorizontalDrag)
                {
                    if (gesture.Delta.X < 0) { }
                    //return new RotateLeftCommand(_gameController);
                    if (gesture.Delta.X > 0) { }
                        //return new RotateRightCommand(_gameController);
                }
            }
            
            base.Update(gameTime);
            
        }


        int midX;
        private void CheckVirtualThumbsticks(TouchLocation t, TouchLocation prevLocaton)
        {
            //Check for firsttouch
            if (firstTouchLeft.Id == 0)
            {
                if(t.Position.X < midX)
                {
                    firstTouchLeft = t;
                    VirtualThumbstickL.LocationStart = t.Position;
                    
                    return;
                }
                
            }
            else
            {
                UpdateVirtualThumbstickTouch(VirtualThumbstickL, ref firstTouchLeft, t);
            }

            if (firstTouchRight.Id == 0)
            {
                if (t.Position.X > midX)
                {
                    firstTouchRight = t;
                    VirtualThumbstickR.LocationStart = t.Position;
                    return;
                }
            }
            else
            {
                UpdateVirtualThumbstickTouch(VirtualThumbstickR, ref firstTouchRight, t);
            }

            
        }

        void UpdateVirtualThumbstickTouch( VirtualThumbstick v, ref TouchLocation fstouch, TouchLocation currentTouch)
        {
            //First touch update
            if (currentTouch.Id == fstouch.Id)
            {
                switch (currentTouch.State)
                {
                    case TouchLocationState.Released:
                        //Clear data in fstouch and virtual tumb stick
                        v.LocationStart = Vector2.Zero;
                        fstouch = new TouchLocation();
                        break;
                    case TouchLocationState.Moved:
                        v.LocationDelta = currentTouch.Position;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
