using Helper;
using MTL.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTL.Combat {
    public class Enemy : MonoBehaviour {
        [SerializeField] private int id;
        public EnemyConfig enemyConfig { get; private set; }
        [SerializeField] Health health;
        PathFinder pathFinder;
        public bool isAlive => health.isAlive;


        public void Init(Path path) {
            enemyConfig = EnemyHelper.GetEnemyConfigById(id);
            pathFinder = GetComponent<PathFinder>();
            pathFinder.Init(path);
            health = GetComponent<Health>();

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