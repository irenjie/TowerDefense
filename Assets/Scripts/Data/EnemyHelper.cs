using Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTL.Data {

    public class EnemyConfig {
        public EnemyConfig() {
        }

        public int id { get; private set; }
        public string name { get; private set; }
        public int maxHealth { get; private set; }
        public float speed { get; private set; }
        public float antiArm { get; private set; }
        public float antiElct { get; private set; }
        public float antiLas { get; private set; }
        public int energeValue { get; private set; }
        public float rotationSpeed { get; private set; }
        public int energyBallSpawnChance { get; private set; }
        public bool isAirUnit { get; private set; }

        public EnemyConfig(int id, string name, int maxHealth, float speed, float antiArm, float antiElct, float antiLas,
            int energeValue, float rotationSpeed, int energyBallSpawnChance, bool isAirUnit) {
            this.id = id;
            this.name = name;
            this.maxHealth = maxHealth;
            this.speed = speed;
            this.antiArm = antiArm;
            this.antiElct = antiElct;
            this.antiLas = antiLas;
            this.energeValue = energeValue;
            this.rotationSpeed = rotationSpeed;
            this.energyBallSpawnChance = energyBallSpawnChance;
            this.isAirUnit = isAirUnit;
        }
    }

    public static class EnemyHelper {
        readonly static List<EnemyConfig> enemyConfigs = new List<EnemyConfig>();
        static bool Initialized = false;

        public static void Initialize() {
            if (Initialized)
                return;
            Initialized = true;

            string[] lines = CsvHelper.ReadLines("Config/Enemy.csv");
            int lineCount = lines.Length;
            for (int i = 2; i < lineCount; i++) {
                string[] la = lines[i].Split(',');
                EnemyConfig enemyConfig = new EnemyConfig(int.Parse(la[0]), la[1], int.Parse(la[2]), float.Parse(la[3]), float.Parse(la[4]),
                    float.Parse(la[5]), float.Parse(la[6]), int.Parse(la[7]), float.Parse(la[8]), int.Parse(la[9]), la[10] == "1");
                enemyConfigs.Add(enemyConfig);
            }
        }

        public static EnemyConfig GetEnemyConfigById(int id) {
            return enemyConfigs.Find(e => e.id == id);
        }
    }
}