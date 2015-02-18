using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class LevelReader
    {
        public LevelReader()
        {
            
        }

        public string readLevel()
        {
            StreamReader sr = new StreamReader("Assets/Waves/Waves.txt");
            return sr.ReadToEnd();
        }

        public List<string[]> SplitWaves()
        {
            string level = readLevel();
            level = level.Replace("\r", "");
            level = level.Replace("\t", "");
            level = level.ToLower();


            string[] stringSeperator = new string[] { "</wave>" };
            string[] _waves = level.Split(stringSeperator, StringSplitOptions.RemoveEmptyEntries);
            List<string[]> _detailedWaves = new List<string[]>();
            stringSeperator = new string[] {"\n"};

            foreach (string wave in _waves)
            {
                _detailedWaves.Add(wave.Split(stringSeperator, StringSplitOptions.RemoveEmptyEntries));
            }

            _detailedWaves.RemoveAt(0);

            return _detailedWaves;
        }
    }
}
