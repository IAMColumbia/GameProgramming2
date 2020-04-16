using ConsoleCommandWUndo;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGAudioWCommandSingleton.Commands;

namespace MGAudioWCommandSingleton
{
    interface IAudioCommandProcessor { };

    public class AudioCommandProcessor : GameComponent, IAudioCommandProcessor
    {
        InputHandler input;     //From monogamelibrary
        GameConsole console;    //From monogamelibrary


        Dictionary<string, Song> Songs;
        Dictionary<string, SoundEffect> SoundEffects;
        List<SoundEffectInstance> SoundEffectInstances;
        Dictionary<string, SoundEffectInstance> SoundEffectSingleInstances;

        Song currentSong;
        

        //private string outText;

        public AudioCommandProcessor(Game game) : base(game)
        {
            input = (InputHandler)this.Game.Services.GetService<IInputHandler>();
            if (input == null)
            {
                input = new InputHandler(this.Game);
                this.Game.Components.Add(input);

            }
            console = (GameConsole)this.Game.Services.GetService<IGameConsole>();
            if (console == null)
            {
                console = new GameConsole(this.Game);
                this.Game.Components.Add(console);
            }

            Songs = new Dictionary<string, Song>();
            SoundEffects = new Dictionary<string, SoundEffect>();
            SoundEffectInstances = new List<SoundEffectInstance>();
            SoundEffectSingleInstances = new Dictionary<string, SoundEffectInstance>(); 

            this.Game.Services.AddService(typeof(IAudioCommandProcessor), this);
        }

        public override void Update(GameTime gameTime)
        {
            foreach(var sei in SoundEffectInstances)
            {
                //Deal with too many instances
                
            }
            
            base.Update(gameTime);
        }

        public Song GetSong(string SongName)
        {
            Song song;
            if(Songs.TryGetValue(SongName, out song))
            {
                //song found nothing to do
            }
            else
            {
                song = this.Game.Content.Load<Song>(SongName);
                Songs.Add(SongName, song);
            }
            return song;
        }

        public SoundEffect GetSoundEffext(string EffectName)
        {
            SoundEffect effect = SoundEffects.FirstOrDefault(s => s.Key == EffectName).Value;
            if (effect == null)
            {
                effect = this.Game.Content.Load<SoundEffect>(EffectName);
                SoundEffects.Add(EffectName, effect);
            }
            return effect;
        }

        

        /// <summary>
        /// Logs Media State Change events to the Monogamelibrary game console
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediaPlayer_MediaStateChanged(object sender, EventArgs e)
        {
            console.GameConsoleWrite(string.Format("MedaiPlayerState:{0}", MediaPlayer.State.ToString()));
            console.GameConsoleWrite(string.Format("MedaiPlayerVolume{0}", MediaPlayer.Volume.ToString()));
        }

        public override void Initialize()
        {

            //Media Player is static 
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged; //hook up eventHandler
        }

        public void Execute(string CommandText)
        {
            switch(CommandText)
            {
                case "Song Volume Up":
                    MediaPlayer.Volume += .1f; //Song Volume UP
                    break;
                case "Song Volume Down":
                    MediaPlayer.Volume -= .1f; //Song Volume Down
                    break;
                case "Song Play":
                    if(currentSong != null)
                    {
                        MediaPlayer.Play(currentSong); //Song Play
                    }
                    break;
                case "Song Stop":
                    MediaPlayer.Stop(); //Song Stop
                    break;
                case "Song Pause":
                    //Song Pause and un Pause
                    if (MediaPlayer.State == MediaState.Paused)
                        MediaPlayer.Resume();
                    else
                        MediaPlayer.Pause();
                    break;
            }
        }

        internal void ExecuteEffect(AudioCommandType commandType, string fileName)
        {
            SoundEffectInstance instance;
            //SoundEffectInstances.Add(fileName, instance);
            switch (commandType)
            {
                case AudioCommandType.PlayOneShot:
                        instance = GetSoundEffectInstance(GetSoundEffext(fileName), fileName);
                        instance.Play();
                    break;
                case AudioCommandType.PlaySingle:
                    instance = GetSoundEffectInstance(GetSoundEffext(fileName), fileName, true);
                    instance.Play();
                    break;
                case AudioCommandType.PlayReplace:
                    instance = GetSoundEffectInstance(GetSoundEffext(fileName), fileName, true, true);
                    instance.Play();
                    break;
                case AudioCommandType.PlayLooped:
                        instance = GetSoundEffectInstance(GetSoundEffext(fileName), fileName);
                        instance.Play();
                        instance.IsLooped = true;
                    break;
            }
        }

        public SoundEffectInstance GetSoundEffectInstance(SoundEffect Effect, string Name, bool single = false, bool replace = false)
        {
            SoundEffectInstance instance;
            if (single || replace) 
            {
                instance = SoundEffectSingleInstances.FirstOrDefault(s => s.Key == Name).Value;
                if (instance == null || replace)
                {
                    instance = Effect.CreateInstance();
                    if (replace)
                    {
                        //If there is a current intance stop it
                        if(SoundEffectSingleInstances[Name] != null)
                            SoundEffectSingleInstances[Name].Stop();
                        SoundEffectSingleInstances[Name] = instance; //replace with new instance
                    }
                    else
                        SoundEffectSingleInstances.Add(Name, instance); //ass new instance
                }
            }
            else
            {
                instance = SoundEffectInstances.FirstOrDefault(i => i.State == SoundState.Stopped
                    || i.State == SoundState.Paused);
                if (instance == null)
                {
                    instance = Effect.CreateInstance();
                    SoundEffectInstances.Add(instance);
                }
            }
            return instance;
        }

        internal void ExecuteSong(AudioCommandType commandType, string fileName)
        {
            currentSong = GetSong(fileName);
            switch(commandType)
            {
                case AudioCommandType.PlayOneShot:
                    MediaPlayer.Play(currentSong);     //Start the song playing
                    break;
                case AudioCommandType.PlayLooped:
                    MediaPlayer.IsRepeating = true;
                    if(MediaPlayer.State != MediaState.Playing)
                    {
                        MediaPlayer.Play(currentSong);
                    }
                    break;
            }
        }
    }
}
