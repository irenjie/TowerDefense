using Buff;
using System.Collections.Generic;
using UnityEngine;

namespace MTL.Combat {
    public class DamageInfo {
        public Attacker attacker { get; private set; }
        public GameObject target { get; private set; }
        public float damage { get; private set; }
        public EDamageType type { get; private set; }

        // �˺��Ƕ�
        public Vector3 damageDegree { get; private set; }
        // ������
        public float criticalRate { get; private set; }
        // ����Ƶ��
        public int hitRate { get; private set; }

        // �������� buff
        private readonly List<BuffInfo> addBuffs = new List<BuffInfo>();

        public DamageInfo(Attacker attacker, GameObject target, float damage, EDamageType type, Vector3 damageDegree, float criticalRate, int hitRate, List<BuffInfo> addBuffs) {
            this.attacker = attacker;
            this.target = target;
            this.damage = damage;
            this.type = type;
            this.damageDegree = damageDegree;
            this.criticalRate = criticalRate;
            this.hitRate = hitRate;
            this.addBuffs = addBuffs;
        }

        public DamageInfo(Attacker attacker, GameObject target, float damage, EDamageType type) {
            this.attacker = attacker;
            this.target = target;
            this.damage = damage;
            this.type = type;
        }
    }
}