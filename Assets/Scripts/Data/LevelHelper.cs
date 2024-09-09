using Extensions;
using Helper;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Data {
    public class LevelConfig {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ImgAddress { get; private set; }

        public LevelConfig(int iD, string name, string description, string imgAddress) {
            ID = iD;
            Name = name;
            Description = description;
            ImgAddress = imgAddress;
        }
    }

    public static class LevelHelper {
        readonly static List<LevelConfig> levelConfigs = new List<LevelConfig>();
        static bool Initialized = false;

        public static void Init() {
            if (Initialized)
                return;
            Initialized = true;

            string[] lines = CsvHelper.ReadLines("Config/LevelConfig.csv");
            int levelCount = lines.Length;
            for (int i = 1; i < levelCount; i++) {
                string[] lineArray = lines[i].Split(',');
                levelConfigs.Add(new LevelConfig(int.Parse(lineArray[0]), lineArray[1], lineArray[3], lineArray[2]));
            }
        }

        public static List<LevelConfig> GetLevelConfigs() {
            return new List<LevelConfig>(levelConfigs);
        }

    }
}
