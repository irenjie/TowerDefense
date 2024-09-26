// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2018/07/13
// 所有攻击者的基类

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTL.Combat {

    public class Attacker : MonoBehaviour {
        public int id => transform.GetInstanceID();

    }
}