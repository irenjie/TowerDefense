
using Buff;
using Helper;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Data {
    public enum SkillType {
        Posional = 0,
        BaseBuff = 1,
        CoolDown = 2
    }

    public class SkillConfig {
        public int id { get; private set; }
        public string name { get; private set; }
        public string description { get; private set; }
        // ½âËøÐÇÐÇÊýÁ¿
        public int unlockStar { get; private set; }
        public string btnIconAddress => $"Skill/{id}/icon.png";
        public float CD { get; private set; }
        public SkillType type { get; private set; }
        public float duration { get; private set; }
        public int energeCost { get; private set; }
        public string SpawnedObjectAddress => $"Skill/{id}/spawn.prefab";
        public EDamageType damageType { get; private set; }

        public SkillConfig(int id, string name, string description, int unlockStar, float cD, SkillType type,
            float duration, int energeCost, EDamageType damageType) {
            this.id = id;
            this.name = name;
            this.description = description;
            this.unlockStar = unlockStar;
            CD = cD;
            this.type = type;
            this.duration = duration;
            this.energeCost = energeCost;
            this.damageType = damageType;
        }
    }



    public static class SkillHelper {
        readonly static List<SkillConfig> skillConfigs = new();
        static bool Initialized = false;

        public static void Init() {
            if (Initialized)
                return;
            Initialized = true;

            string[] lines = CsvHelper.ReadLines("Config/Skill.csv");
            int lineCount = lines.Length;
            for (int i = 2; i < lineCount; i++) {
                string[] la = lines[i].Trim().Split(',');
                SkillConfig config = new SkillConfig(int.Parse(la[0]), la[1], la[10], int.Parse(la[2]), float.Parse(la[4]),
                    (SkillType)int.Parse(la[5]), float.Parse(la[6]), int.Parse(la[7]), (EDamageType)int.Parse(la[9]));
                skillConfigs.Add(config);
            }
        }

        public static SkillConfig GetSkillConfig(int id) {
            return skillConfigs.Find(skill => skill.id == id);
        }

    }
}
