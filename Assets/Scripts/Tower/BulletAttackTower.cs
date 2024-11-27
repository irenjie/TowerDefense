using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTL.Combat {
    public class BulletAttackTower : BaseAttackTower {
        [SerializeField] GameObject bulletPrefab = null;

        float attackCD = 0;



        private void Update() {
            if (attackCD > 0) {
                attackCD -= Time.deltaTime;
                return;
            }

            Enemy target = GetClosestEnemyTarget();
            if (target == null)
                return;

            Vector3 targetDir = (target.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
            transform.rotation = targetRotation;
            Attack(target);

        }

        void Attack(Enemy target) {
            GameObject bullet = Instantiate<GameObject>(bulletPrefab, transform);
            attackCD = towerConfig.attackSpeed;
            bullet.GetComponent<Bullet>().Init(attackPoint.position, transform.forward, this);
        }

    }
}