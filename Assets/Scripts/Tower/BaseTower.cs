using MTL.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTL.Combat {
    public class BaseTower : Attacker {

        protected TowerConfig towerConfig;
        protected bool canAttack = false;

        public virtual void Init(int towerID) {
            towerConfig = TowerHelper.GetTowerConfigById(towerID);
            canAttack = true;
        }

        public override float GetAttackDamage() {
            return towerConfig.attackPower;
        }

        /// <summary>
        /// 最近可攻击目标
        /// </summary>
        /// <returns></returns>
        public Enemy GetClosestEnemyTarget() {
            Enemy target = null;

            return target;

        }
    }
}