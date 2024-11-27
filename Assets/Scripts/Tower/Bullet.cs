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

        BaseTower tower;
        Rigidbody rigidbody;

        private void Awake() {
            rigidbody = GetComponent<Rigidbody>();
        }

        public void Init(Vector3 birthPos, Vector3 attackDir, BaseAttackTower tower) {
            transform.position = birthPos;
            transform.rotation = Quaternion.LookRotation(attackDir);
            this.tower = tower;
        }

        private void Update() {
            aliveDuration -= Time.deltaTime;
            if (aliveDuration <= 0)
                Destroy(gameObject);
        }

        private void FixedUpdate() {
            rigidbody.position += transform.forward * speed * Time.deltaTime;
        }

        private void OnCollisionEnter(Collision collision) {
            //Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            //if (enemy == null || !enemy.isAlive)
            //    return;

            //Destroy(gameObject);

        }

    }

}