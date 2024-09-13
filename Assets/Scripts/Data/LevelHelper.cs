using Extensions;
using Helper;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Data {
    public class LevelConfig {
        public int ID { get; private set; }
        public string name { get; private set; }
        public List<string> stroyDesc;
        public string sceneAddress => $"Level/{ID}/Level{ID}.unity";
        public string iconAddress => $"Level/{ID}/icon.png";
        public int startEnerge { get; private set; }
        public int startHealth { get; private set; }
        // 可以建造的塔
        public List<int> towerEnabled;
        public string fadeIconAddress => $"Level/{ID}/fadeIcon.psd";
        public int energeBallGenerationRate { get; private set; }
        public int easyModeMul { get; private set; }
        public int hardModeMul { get; private set; }
        public float armMul { get; private set; }
        public float electMul { get; private set; }
        public float laserMul { get; private set; }

        public LevelConfig(int iD, string name, List<string> description, int startEnerge, int startHealth, List<int> towerEnabled,
            int energeBallGenerationRate, int easyModeMul, int hardModeMul, float armMul, float electMul, float laserMul) {
            ID = iD;
            this.name = name;
            this.stroyDesc = description;
            this.startEnerge = startEnerge;
            this.startHealth = startHealth;
            this.towerEnabled = towerEnabled;
            this.energeBallGenerationRate = energeBallGenerationRate;
            this.easyModeMul = easyModeMul;
            this.hardModeMul = hardModeMul;
            this.armMul = armMul;
            this.electMul = electMul;
            this.laserMul = laserMul;
        }
    }

    public static class LevelHelper {
        public readonly static List<LevelConfig> levelConfigs = new List<LevelConfig>();
        static bool Initialized = false;

        public static void Init() {
            if (Initialized)
                return;
            Initialized = true;

            string[] lines = CsvHelper.ReadLines("Config/Level.csv");
            int lineCount = lines.Length;
            for (int i = 3; i < lineCount; i++) {
                string[] la = lines[i].Trim().Split(',');
                List<int> towerEnabled = la[6].Split('|').Select(id => int.Parse(id)).ToList();
                List<string> storyDesc = la[8].Split('|').ToList();
                LevelConfig newLevel = new LevelConfig(int.Parse(la[0]), la[2], storyDesc, int.Parse(la[4]), int.Parse(la[5]), towerEnabled,
                    int.Parse(la[9]), int.Parse(la[10]), int.Parse(la[11]), float.Parse(la[12]), float.Parse(la[13]), float.Parse(la[14]));

                levelConfigs.Add(newLevel);
            }
        }

    }
}
