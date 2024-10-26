// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2018/07/13
// ���й����ߵĻ���

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAttackType {
    Tower,
    Skill,
    Enemy
}


namespace MTL.Combat {

    public abstract class Attacker : MonoBehaviour {
        public int id => transform.GetInstanceID();
        EAttackType attackType;
        EDamageType damageType;

        public abstract float GetAttackDamage();
    }
}