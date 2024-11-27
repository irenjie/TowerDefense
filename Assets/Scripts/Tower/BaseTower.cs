using MTL.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTL.Combat {
    public class BaseTower : MonoBehaviour {

        protected TowerConfig towerConfig;
        protected bool canAttack = false;
        [SerializeField] protected int towerID;
        protected int towerLevel = 1;

        public virtual void Init() {
            towerConfig = TowerHelper.GetTowerConfigById(towerID);
            canAttack = true;
        }

    }
}