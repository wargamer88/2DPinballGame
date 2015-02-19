using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public enum SoundEffect
    {
        CRYSTAL,

        EARTH,
        FIRE,
        LIGHTNING,
        WATER,
        WIND,

        EXPLOSION,
        ICE,
        ICE_BREAK,
        LAVA,
        MAGNET,
        SAND,
        SCORCH,
        STEAM,
        STORM,
        SWIFT,
        WOOD,

    }

    public enum Music
    {
        MENU,
        INGAME,

    }

    public static class SoundManager
    {
        private static SoundChannel _soundChannelMusic;
        private static int SoundChannelID = 1;
        private static bool _fadeMusic;
        private static bool _isplaying = false;

        public delegate void StatusEventListener(string action);
        public static event StatusEventListener onAfterFadeMusic;

        private static Dictionary<SoundEffect, Sound> _dictionarySounds = new Dictionary<SoundEffect, Sound>()
        {
            { SoundEffect.CRYSTAL,      new Sound(@"Assets\sounds\crystal pickup.wav")  },

            { SoundEffect.EARTH,        new Sound(@"Assets\sounds\effects\Earth.wav")  },
            { SoundEffect.FIRE,         new Sound(@"Assets\sounds\effects\Fire.wav")  },
            { SoundEffect.LIGHTNING,    new Sound(@"Assets\sounds\effects\Lightning.wav")  },
            { SoundEffect.WATER,        new Sound(@"Assets\sounds\effects\Water.wav")  },
            { SoundEffect.WIND,         new Sound(@"Assets\sounds\effects\Wind.wav")  },
            
            { SoundEffect.EXPLOSION,    new Sound(@"Assets\sounds\effects\explosion.wav")  },
            { SoundEffect.ICE,          new Sound(@"Assets\sounds\effects\ice.wav")  },
            { SoundEffect.ICE_BREAK,    new Sound(@"Assets\sounds\effects\ice break.wav")  },
            { SoundEffect.LAVA,         new Sound(@"Assets\sounds\effects\lava.wav")  },
            { SoundEffect.MAGNET,       new Sound(@"Assets\sounds\effects\magnet.wav")  },
            { SoundEffect.SAND,         new Sound(@"Assets\sounds\effects\sand.wav")  },
            { SoundEffect.SCORCH,       new Sound(@"Assets\sounds\effects\scorch.wav")  },
            { SoundEffect.STEAM,        new Sound(@"Assets\sounds\effects\steam.wav")  },
            { SoundEffect.STORM,        new Sound(@"Assets\sounds\effects\storm.wav")  },
            { SoundEffect.SWIFT,        new Sound(@"Assets\sounds\effects\swift.wav")  },
            { SoundEffect.WOOD,         new Sound(@"Assets\sounds\effects\wood.wav")  },
        };

        private static Dictionary<Music, Sound> _dictionaryMusic = new Dictionary<Music, Sound>()
        {
            { Music.INGAME,        new Sound(@"Assets\sounds\music\Ingame.wav",true, true)  },
            { Music.MENU,         new Sound(@"Assets\sounds\music\Menu.wav",true, true)  },
        };

        public static void PlaySound(SoundEffect effect, float volume = 1.0f, float pan = 0.0f)
        {
            Sound sound = _dictionarySounds[effect];
            SoundChannel soundChannel = new SoundChannel(SoundChannelID);
            
            soundChannel = sound.Play();
            soundChannel.Volume = volume;
            soundChannel.Pan = pan;
            SoundChannelID++;
            if (SoundChannelID == 32)
            {
                SoundChannelID = 1;
            }
        }

        public static void PlayMusic(Music musicKey)
        {
            if (!_isplaying)
            {
                _isplaying = true;
                Sound music = _dictionaryMusic[musicKey];
                
                _soundChannelMusic = music.Play(false, 0);
                _soundChannelMusic.Volume = 0.7f;
            }
        }

        public static void StopMusic(bool? fadeMusic = false)
        {
            if (_isplaying)
            {
                _fadeMusic = fadeMusic ?? fadeMusic == false;

                if (!_fadeMusic)
                {
                    _soundChannelMusic.Stop();
                    _isplaying = false;
                }
                else
                {
                    Game.main.OnAfterStep += FadeMusic;
                }
            }
        }

        private static void FadeMusic()
        {
            _soundChannelMusic.Volume = _soundChannelMusic.Volume - 0.05f;
            if (_soundChannelMusic.Volume <= 0)
            {
                StopMusic();
                onAfterFadeMusic("musicFaded");
                Game.main.OnAfterStep -= FadeMusic;
            }
        }

    }
}
