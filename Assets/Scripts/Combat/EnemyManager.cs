using Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTL.Combat {
    public class EnemyManager : SingletonClass<EnemyManager> {
        List<Enemy> enemyList = new List<Enemy>();
        public int ExistEnemyNum => enemyList.Count;

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