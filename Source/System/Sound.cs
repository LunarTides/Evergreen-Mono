using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Evergreen.System {
    public abstract class Sound {
        private static readonly Dictionary<string, SoundEffect[]> sfx = [];
        private const bool ENABLED = true;

        public static void Load() {
            if (!ENABLED) {
                return;
            }

            ContentManager content = Evergreen.Instance.Content;

            sfx.Add("Dig", [
                content.Load<SoundEffect>("Sounds/Dig_0"),
                content.Load<SoundEffect>("Sounds/Dig_1"),
                content.Load<SoundEffect>("Sounds/Dig_2"),
            ]);

            sfx.Add("Grab", [content.Load<SoundEffect>("Sounds/Grab")]);
        }

        /// <summary>
        /// Plays a sound effect. If there are multiple sounds with the same name, it plays a random one of those.
        /// </summary>
        /// <param name="name">The name of the sound effect to play.</param>
        public static void Play(string name) {
            if (!ENABLED) {
                return;
            }

            SoundEffect[] sounds = sfx[name];

            SoundEffect sound = sounds[Random.Shared.Next(0, sounds.Length - 1)];
            sound.Play();
        }
    }
}
