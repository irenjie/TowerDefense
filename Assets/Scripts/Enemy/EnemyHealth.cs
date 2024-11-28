using Extensions;
using UnityEngine.UI;

namespace MTL.Combat {
    public class EnemyHealth : Health {
        Enemy enemy;
        Image healthBar;

        private void Awake() {
            enemy = GetComponent<Enemy>();
            healthBar = transform.Find<Image>("HealthBarPivot/Canvas/ImageFill");
        }

        private void Start() {
            Init(enemy.enemyConfig.maxHealth);
        }


        private void Update() {
            healthBar.fillAmount = curHealth / maxHealth;
        }

        public override void TakeDamage(DamageInfo dinfo) {
            float damageResult = dinfo.damage;

            switch (dinfo.type) {
                case EDamageType.Armour:
                    damageResult *= enemy.enemyConfig.antiArm;
                    break;
                case EDamageType.Electricity:
                    damageResult *= enemy.enemyConfig.antiElct;
                    break;
                case EDamageType.Laser:
                    damageResult *= enemy.enemyConfig.antiLas;
                    break;
                default:
                    break;
            }
            base.TakeDamage(damageResult);
        }


        protected override void HandleDeath() {
            if (curHealth > 0)
                return;

            enemy.Die();

            base.HandleDeath();

        }
    }
}