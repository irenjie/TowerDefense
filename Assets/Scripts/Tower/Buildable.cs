using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MTL.Combat {
    /// <summary>
    /// 塔座，炮塔必须建在塔座上
    /// </summary>
    public class Buildable : MonoBehaviour {
        public bool HasTower { get; private set; } = false;
        public bool CanBuild { get; private set; } = false;

        public void BuildTower() {

        }

        /// <summary>
        /// 显示建造菜单
        /// </summary>
        public void ShowBuildMenu() {

        }

        /// <summary>
        /// 隐藏建造菜单
        /// </summary>
        public void HideBuildMenu() {

        }
    }
}