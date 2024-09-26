using Helper;
using MTL.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTL.Combat {
    public class Enemy : MonoBehaviour {
        [SerializeField] private int id;
        private EnemyConfig enemyConfig;
        [SerializeField] Health health;
        Path Path;
        PathFinder pathFinder;


        public void Init(Path path) {
            enemyConfig = EnemyHelper.GetEnemyConfigById(id);
            this.Path = path;
            pathFinder = GetComponent<PathFinder>();
        }

        /// <summary>
        /// µÐÈË½ø·ÀÓùËþ
        /// </summary>
        public void EnterBase() {
            EnemyManager.Get().EnemyEnterBase(this);
            Destroy(gameObject);
        }

        public void Die() {
            EnemyManager.Get().EnemyDie(this);
            Destroy(gameObject);
        }


    }
}