using MScene;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTL.Combat {


    public abstract class BaseAttackTower : BaseTower {
        public Vector3 position => transform.position;
        [SerializeField] protected Transform attackPoint;


        /// <summary>
        /// 最近可攻击目标
        /// </summary>
        /// <returns></returns>
        public Enemy GetClosestEnemyTarget() {
            List<Enemy> allEnemys = LevelScene.instance.EnemyManager.enemyList;
            foreach (Enemy enemy in allEnemys) {
                if (enemy == null || !enemy.isAlive)
                    continue;

                // 暂不区分地面和空中单位
                float enemyDistance = Vector3.Distance(position, enemy.position);
                if (enemyDistance > towerConfig.attackRange)
                    continue;
                return enemy;
            }
            return null;
        }

    }
}