using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MTL.Combat {
    /// <summary>
    /// �������������뽨��������
    /// </summary>
    public class Buildable : MonoBehaviour {
        public bool HasTower { get; private set; } = false;
        public bool CanBuild { get; private set; } = false;

        public void BuildTower() {

        }

        /// <summary>
        /// ��ʾ����˵�
        /// </summary>
        public void ShowBuildMenu() {

        }

        /// <summary>
        /// ���ؽ���˵�
        /// </summary>
        public void HideBuildMenu() {

        }
    }
}