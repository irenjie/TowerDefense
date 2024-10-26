using Buff;
using System.Collections.Generic;
using UnityEngine;

namespace MTL.Combat {
    public class DamageInfo {
        public Attacker attacker { get; private set; }
        public GameObject target { get; private set; }
        public float damage { get; private set; }
        public EDamageType type { get; private set; }

        // ÉËº¦½Ç¶È
        public Vector3 damageDegree { get; private set; }
        // ±©»÷ÂÊ
        public float criticalRate { get; private set; }
        // ¹¥»÷ÆµÂÊ
        public int hitRate { get; private set; }

        // ¹¥»÷¸½¼Ó buff
        private readonly List<BuffInfo> addBuffs = new List<BuffInfo>();
    }
}