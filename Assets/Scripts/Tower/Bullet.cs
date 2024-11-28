using Helper;
using MScene;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MTL.Combat {
    public enum BulletType {
        Target,
        Line
    }


    public class Bullet : MonoBehaviour {
        public BulletType type { get; private set; }
        [SerializeField] float speed;
        [SerializeField] float aliveDuration = 0;

        BulletType bulletType;
        Transform attackTarget;
        BulletAttackTower tower;
        new Rigidbody rigidbody;

        private void Awake() {
            rigidbody = GetComponent<Rigidbody>();
        }

        public void Init(Vector3 birthPos, Transform target, BulletAttackTower tower) {
            bulletType = BulletType.Target;
            attackTarget = target;
            Init(birthPos, tower);
        }

        public void Init(Vector3 birthPos, Vector3 attackDir, BulletAttackTower tower) {
            bulletType = BulletType.Line;
            Init(birthPos, tower);
            rigidbody.rotation = Quaternion.LookRotation(attackDir);
        }

        private void Init(Vector3 birthPos, BulletAttackTower tower) {
            transform.position = birthPos;
            this.tower = tower;
        }

        private void Update() {
            aliveDuration -= Time.deltaTime;
            if (aliveDuration <= 0)
                Destroy(gameObject);
        }

        private void FixedUpdate() {
            switch (bulletType) {
                case BulletType.Line:
                    LineTypeFixedUpdate();
                    break;
                case BulletType.Target:
                    TargetTypeFixedUpdate();
                    break;
            }

        }

        private void LineTypeFixedUpdate() {
            rigidbody.position += transform.forward * speed * Time.deltaTime;
        }

        private void TargetTypeFixedUpdate() {
            if (attackTarget == null) {
                bulletType = BulletType.Line;
                return;
            }
            rigidbody.rotation = Quaternion.LookRotation(attackTarget.position - rigidbody.position);
            LineTypeFixedUpdate();
        }

        private void OnTriggerEnter(Collider other) {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy == null || !enemy.isAlive)
                return;

            DamageInfo damageInfo = new DamageInfo(tower, enemy.gameObject, tower.GetAttackDamage(), EDamageType.Armour);
            enemy.TakeDamage(damageInfo);

            Destroy(gameObject);

        }

    }

}