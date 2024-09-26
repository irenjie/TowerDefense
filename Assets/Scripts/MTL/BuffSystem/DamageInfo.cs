using MTL.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buff {
    public class DamageInfo {
        public GameObject creator { get; private set; }
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