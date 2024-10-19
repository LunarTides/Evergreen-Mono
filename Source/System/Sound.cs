using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Evergreen.System
{
    public abstract class Sound
    {
        private static Dictionary<string, SoundEffect[]> sfx = [];

        public static void Load()
        {
            ContentManager content = Evergreen.Instance.Content;

            sfx.Add("Dig", [
                content.Load<SoundEffect>("Sounds/Dig_0"),
                content.Load<SoundEffect>("Sounds/Dig_1"),
                content.Load<SoundEffect>("Sounds/Dig_2"),
            ]);

            sfx.Add("Grab", [content.Load<SoundEffect>("Sounds/Grab")]);
        }

        public static void Play(string name)
        {
            SoundEffect[] sounds = sfx[name];

            SoundEffect sound = sounds[Random.Shared.Next(0, sounds.Length - 1)];
            sound.Play();
        }
    }
}
