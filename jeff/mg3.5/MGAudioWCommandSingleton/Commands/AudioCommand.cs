using ConsoleCommandWUndo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MGAudioWCommandSingleton.Commands
{

    public enum AudioSoundType {  Song, SoundEffect, Command };

    public enum AudioCommandType { PlayOneShot, PlayLooped, PlaySingle, PlayReplace, Text };

    class AudioCommand : Command, IAudioCommand
    {

        AudioSoundType SoundType;
        AudioCommandType CommandType;
        string fileName;

        AudioCommandProcessor acp;

        public AudioCommand(AudioCommandProcessor gc, AudioSoundType soundType, AudioCommandType commandType, string fileName) : base ()
        {
            this.CommandName = "Audio Command";
            SoundType = soundType;
            CommandType = commandType;
            this.fileName = fileName;
            this.acp = gc;
        }

        public override void Execute(GameComponent gc)
        {
            this.Execute();
            base.Execute(gc);
        }


        public override void Execute()
        {
            switch (SoundType)
            {
                case AudioSoundType.Song:
                    acp.ExecuteSong(CommandType, fileName);
                    break;
                case AudioSoundType.SoundEffect:
                    acp.ExecuteEffect(CommandType, fileName);
                    break;
            }
            base.Execute();
        }

        public void Execute(AudioCommandProcessor acp, string CommandText)
        {
            this.Execute(CommandText);
        }

        public void Execute(string CommandText)
        {
            acp.Execute(CommandText);
        }
    }
}
