using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTL.Combat {
    /// <summary>
    /// Ñ°Â·£¬¿ØÖÆÒÆ¶¯
    /// </summary>
    public class PathFinder : MonoBehaviour {
        Path path = null;
        Enemy enemy = null;

        private bool isMove = false;
        int curPathNodeIndex;

        public void Stop() {
            isMove = false;
        }

        public void Init(Path path) {
            isMove = true;
            this.path = path;
            enemy = GetComponent<Enemy>();
            curPathNodeIndex = 0;
        }

        private void Update() {
            if (!isMove) {
                return;
            }

            if (curPathNodeIndex >= path.nodeTotalNum) {
                Stop();
                enemy.EnterBase();
                return;
            }

            Vector3 targetPos = path.GetNodePos(curPathNodeIndex);
            float targetDistance = Vector3.Distance(transform.position, targetPos);
            if (targetDistance < 0.1f)
                curPathNodeIndex++;

            Vector3 targetDirection = (targetPos - transform.position).normalized;
            transform.position += targetDirection * enemy.enemyConfig.speed * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, enemy.enemyConfig.rotationSpeed);
        }
    }
}