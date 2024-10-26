using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MTL.Combat {
    public enum BulletType {
        Target,
        Line
    }


    public class Bullet : MonoBehaviour { 
        public BulletType type {  get; private set; }

        [SerializeField] float speed;

        BaseTower tower;


    }

}