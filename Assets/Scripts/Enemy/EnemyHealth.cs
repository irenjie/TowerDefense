

namespace MTL.Combat {
    public class EnemyHealth : Health {
        Enemy enemy;

        private void Start() {
            enemy = GetComponent<Enemy>();
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
            enemy.Die();

            base.HandleDeath();

        }
    }
}