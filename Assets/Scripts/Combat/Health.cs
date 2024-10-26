
using Buff;
using MTL.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MTL.Combat {
    /// <summary>
    /// ��λ����ֵ������������ʾ
    /// </summary>
    public class Health : MonoBehaviour {
        public float maxHealth { get; private set; } = 10f;
        public float curHealth { get; private set; }
        public bool isAlive { get; private set; } = true;


        #region �ص���
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

        public virtual void TakeDamage(DamageInfo dinfo) {
            TakeDamage(dinfo.damage);
        }

        public void TakeDamage(float damage) {

            curHealth -= damage;
            curHealth = Mathf.Clamp(curHealth, 0, maxHealth);
            OnDamaged?.Invoke(this, GameEventArgs.Empty);

            HandleDeath();
        }

        public virtual void TakePercentDamage(DamageInfo dinfo) {

        }

        private void PopDamageText(float damage) {

        }

        /// <summary>
        /// �жϲ���������
        /// </summary>
        protected virtual void HandleDeath() {
            if (curHealth <= 0f) {
                isAlive = false;
                OnDie?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Ѫ����ʾ
        /// </summary>
        private void UpdateHealthBar() {

        }
    }
}