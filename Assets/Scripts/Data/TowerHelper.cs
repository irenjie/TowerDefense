

using Helper;
using NUnit.Framework;
using System.Collections.Generic;

namespace Data {
    public class TowerConfig {
        public int id { get; private set; }
        public string realID => $"{id}{curLevel.ToString("D2")}";
        public string name { get; private set; }
        public string description;
        // 等级
        public int curLevel;
        public List<TowerConfig> upgrades;
        public int unlockLevel { get; private set; }
        public int energeCost { get; private set; }
        public int attackPower { get; private set; }
        public float attackSpeed { get; private set; }
        public float attackRange { get; private set; }
        public float birthTime { get; private set; }
        public float aniFrame { get; private set; }
        public float shootAniDelay { get; private set; }
        public string iconAddress => $"Tower/{id}/icon.png";
        public string btnIconAddress => $"Tower/{id}/btnIcon.png";
        public float rotateSpeed { get; private set; }
        public bool canAttackFly { get; private set; }
        // 有限攻击空中单位
        public bool airFirst { get; private set; }

        public TowerConfig(int id, string name, string description, int unlockLevel, int energeCost, int attackPower,
            float attackSpeed, float attackRange, float birthTime, float aniFrame, float shootAniDelay, float rotateSpeed,
            bool canAttackFly, bool airFirst) {
            this.id = id;
            this.name = name;
            this.description = description;
            this.curLevel = 1;
            this.unlockLevel = unlockLevel;
            this.energeCost = energeCost;
            this.attackPower = attackPower;
            this.attackSpeed = attackSpeed;
            this.attackRange = attackRange;
            this.birthTime = birthTime;
            this.aniFrame = aniFrame;
            this.shootAniDelay = shootAniDelay;
            this.rotateSpeed = rotateSpeed;
            this.canAttackFly = canAttackFly;
            this.airFirst = airFirst;
        }
    }

    public static class TowerHelper {
        readonly static List<TowerConfig> towerConfigs = new List<TowerConfig>();
        static bool Initialized = false;

        public static void Initialize() {
            if (Initialized)
                return;
            Initialized = true;

            string[] lines = CsvHelper.ReadLines("Config/Tower.csv");
            int lineCount = lines.Length;
            for (int i = 2; i < lineCount; i++) {
                string[] la = lines[i].Trim().Split(',');
                int id = int.Parse(la[0].Substring(0, 4));
                TowerConfig newTower = new TowerConfig(id, la[1], null, int.Parse(la[2]), int.Parse(la[3]), int.Parse(la[4]),
                        float.Parse(la[5]), float.Parse(la[6]), float.Parse(la[7]), float.Parse(la[8]), float.Parse(la[9]),
                        float.Parse(la[11]), la[12] == "1", la[13] == "1");
                TowerConfig findTower = towerConfigs.Find(config => config.id == id);
                if (findTower == null) {
                    // 新塔
                    newTower.description = la[14];
                    towerConfigs.Add(newTower);
                } else {
                    // 升级塔
                    findTower.upgrades ??= new List<TowerConfig>();
                    newTower.curLevel = findTower.upgrades.Count + 2;
                    findTower.upgrades.Add(newTower);
                }
            }
        }

        public static TowerConfig GetTowerConfig(int id, int level = 1) {
            if (level < 1)
                return null;

            TowerConfig result = towerConfigs.Find(config => config.id == id);
            if (result == null)
                return null;
            if (level == 1) {
                return result;
            }

            return level - 1 > result.upgrades.Count ? null : result.upgrades[level - 2];
        }

        public static TowerConfig GetTowerConfig(string realID) {
            return towerConfigs.Find(config => config.realID == realID);
        }
    }
}
