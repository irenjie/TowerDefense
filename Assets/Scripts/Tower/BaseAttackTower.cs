using MScene;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTL.Combat {


    public abstract class BaseAttackTower : BaseTower {
        public Vector3 position => transform.position;
        [SerializeField] protected Transform attackPoint;


        /// <summary>
        /// ����ɹ���Ŀ��
        /// </summary>
        /// <returns></returns>
        public Enemy GetClosestEnemyTarget() {
            List<Enemy> allEnemys = LevelScene.instance.EnemyManager.enemyList;
            foreach (Enemy enemy in allEnemys) {
                if (enemy == null || !enemy.isAlive)
                    continue;

                // �ݲ����ֵ���Ϳ��е�λ
                float enemyDistance = Vector3.Distance(position, enemy.position);
                if (enemyDistance > towerConfig.attackRange)
                    continue;
                return enemy;
            }
            return null;
        }

    }
}