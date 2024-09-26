
using Buff;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MTL.Combat {
    /// <summary>
    /// 单位生命值管理，增减、显示
    /// </summary>
    public class Health : MonoBehaviour {
        public float maxHealth { get; private set; } = 10f;
        public float curHealth { get; private set; }

        #region 回调点
        public event EventHandler OnDie;
        public event EventHandler OnChange;
        public event EventHandler OnDamaged;
        public event EventHandler OnHeal;
        #endregion

        public void Init(float maxHealth) {
            curHealth = this.maxHealth = maxHealth;
        }

        private void Update() {
            UpdateHealthBar();
        }

        public void TakeDamage(DamageInfo dinfo, Attacker attacker) {

        }

        private void PopDamageText(float damage) {

        }

        /// <summary>
        /// 判断并处理死亡
        /// </summary>
        private void HandleDeath() {
            if (curHealth <= 0f) {
                OnDie?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 血量显示
        /// </summary>
        private void UpdateHealthBar() {

        }
    }
}