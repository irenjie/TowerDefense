using Helper;
using MScene;
using MTL.Data;
using MTL.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering;

namespace MTL.Combat {

    public class BuildManager : MonoBehaviour {
        // 当前选中要建造的炮塔 ID
        public string curDragTowerRealID = string.Empty;
        // 要建造的炮塔拖动时不可建（不在塔座上）时对象
        GameObject dragTowerRedRim = null;
        // 要建造的炮塔拖动时可建（在塔座上）时对象
        GameObject dragTowerBlueRim = null;
        // 当前显示菜单的塔座对象（选中后可升级、销毁其上炮塔）
        Buildable curShowMenuTowerBase = null;

        // 点击炮塔时射线检测层级
        public LayerMask towerStation;

        //Ray ray;
        //RaycastHit hit;
        //GameObject hitObj;

        private void Update() {
            SelectTowerBaseUpdate();
            DragTowerUpdate();
        }

        /// <summary>
        /// 点击塔座，弹出建造炮塔或升级面板
        /// </summary>
        void SelectTowerBaseUpdate() {
            if (Input.GetMouseButtonUp(0)) {
                if (curShowMenuTowerBase != null) {
                    curShowMenuTowerBase.HideBuildMenu();
                }

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit)) {
                    GameObject hitObj = hit.collider.gameObject;
                    if (hitObj.TryGetComponent<Buildable>(out Buildable towerBase) && towerBase.HasTower) {
                        towerBase.ShowBuildMenu();
                        curShowMenuTowerBase = towerBase;
                    }

                    if (hitObj.TryGetComponent<EnergyBall>(out EnergyBall energyBall)) {
                        energyBall.OnClick();
                    }
                }
            }
        }

        /// <summary>
        /// 点击炮塔菜单中的炮塔，开始拖动
        /// </summary>
        void DragTowerUpdate() {
            if (string.IsNullOrEmpty(curDragTowerRealID))
                return;

            if (dragTowerRedRim == null || dragTowerBlueRim == null) {
                CreateDragRim();
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // 鼠标射线在塔座上
            if (Physics.Raycast(ray, out RaycastHit hit, towerStation)) {
                GameObject hitObj = hit.collider.gameObject;
                if (hitObj.TryGetComponent<Buildable>(out Buildable towerBase)) {
                    dragTowerRedRim.SetActive(false);
                    dragTowerBlueRim.SetActive(true);
                    dragTowerBlueRim.transform.position = hit.point;
                    if (Input.GetMouseButtonUp(0)) {
                        TowerConfig towerConfig = TowerHelper.GetTowerConfigRealId(curDragTowerRealID);
                        if (towerBase.CanBuild) {
                            if (LevelScene.instance.LevelLifeTimeManager.HasEnoughEnergyToBuildTower(towerConfig.energeCost))
                                towerBase.BuildTower(towerConfig);
                            else
                                EventManager.Get().Fire(this, (int)EventID.TipPanel_ShowTip, new StringEventArgs("没有足够能量"));
                            ResetDragData();
                        }
                    } else if (Input.GetMouseButtonUp(1)) {
                        ResetDragData();
                    }
                    return;
                }
            } else {
                return;
            }

            // 射在地面上
            dragTowerRedRim.SetActive(true);
            dragTowerBlueRim.SetActive(false);
            dragTowerRedRim.transform.position = hit.point + new Vector3(0, 2, 0);
            if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0)) {
                ResetDragData();
            }
        }

        void CreateDragRim() {
            dragTowerRedRim = LoaderHelper.Get().InstantiatePrefab($"Tower/Rim/{curDragTowerRealID}R.prefab", transform);
            dragTowerBlueRim = LoaderHelper.Get().InstantiatePrefab($"Tower/Rim/{curDragTowerRealID}B.prefab", transform);
        }

        void ResetDragData() {
            Addressables.Release(dragTowerRedRim);
            Addressables.Release(dragTowerBlueRim);
            Destroy(dragTowerRedRim);
            Destroy(dragTowerBlueRim);
            dragTowerRedRim = dragTowerBlueRim = null;
            curDragTowerRealID = string.Empty;
        }
    }
}