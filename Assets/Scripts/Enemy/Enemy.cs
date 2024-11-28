using Helper;
using MScene;
using MTL.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTL.Combat {
    public class Enemy : MonoBehaviour {
        [SerializeField] private int id;
        public EnemyConfig enemyConfig { get; private set; }
        EnemyHealth health;
        PathFinder pathFinder;
        public Vector3 position=>transform.position;
        public bool isAlive => health.isAlive;


        public void Init(Path path) {
            enemyConfig = EnemyHelper.GetEnemyConfigById(id);
            pathFinder = GetComponent<PathFinder>();
            pathFinder.Init(path);
            health = GetComponent<EnemyHealth>();

        }

        /// <summary>
        /// µÐÈË½ø·ÀÓùËþ
        /// </summary>
        public void EnterBase() {
            LevelScene.instance.EnemyManager.EnemyEnterBase(this);
            Destroy(gameObject);
        }

        public void Die() {
            LevelScene.instance.EnemyManager.EnemyDie(this);
            Destroy(gameObject);
        }

        public void TakeDamage(DamageInfo damageInfo) {
            health.TakeDamage(damageInfo);
        }


    }
}