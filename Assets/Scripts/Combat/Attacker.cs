// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2018/07/13
// 所有攻击者的基类

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAttackType {
    Tower,
    Skill,
    Enemy
}


namespace MTL.Combat {

    public interface Attacker {
        public int ID { get; }

        public float GetAttackDamage();
    }
}