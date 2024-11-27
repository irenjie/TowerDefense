using Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTL.Combat {
    public class EnemyManager : MonoBehaviour {
        public List<Enemy> enemyList { get; private set; }
        public int ExistEnemyNum => enemyList.Count;

        private void Awake() {
            enemyList = new List<Enemy>();
        }

        public void LevelStart(int level) {
            enemyList.Clear();
        }

        public void AddEnemy(Enemy enemy) {
            enemyList.Add(enemy);
        }

        public void EnemyDie(Enemy enemy) {
            enemyList.Remove(enemy);
        }

        public void EnemyEnterBase(Enemy enemy) {
            // ПлбЊ
            // ...
            enemyList.Remove(enemy);
        }
    }
}