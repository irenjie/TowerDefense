using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTL.Combat {
    public class TowerManager : MonoBehaviour {
        readonly List<BaseTower> towerList = new List<BaseTower>();

        public void AddTower(BaseTower tower) {
            towerList.Add(tower);
        }

        public void RemoveTower(BaseTower tower) {
            towerList.Remove(tower);
        }

        public void ClearTowerList() {
            towerList.Clear();
        }

    }
}