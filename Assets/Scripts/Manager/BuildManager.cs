using Helper;
using MScene;
using MTL.Data;
using MTL.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MTL.Combat {

    public class BuildManager : MonoBehaviour {
        // 当前选中要建造的炮塔 ID
        int curDragTowerID = -1;
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

        void DragTowerUpdate() {
            // 拖动中
            if (curDragTowerID != -1) {
                if (dragTowerRedRim == null || dragTowerBlueRim == null)
                    CreateDragRim();
                else {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hit, towerStation)) {
                        GameObject hitObj = hit.collider.gameObject;
                        if (hitObj.TryGetComponent<Buildable>(out Buildable towerBase)) {
                            // 鼠标射线在塔座上
                            dragTowerRedRim.SetActive(false);
                            dragTowerBlueRim.SetActive(true);
                            dragTowerBlueRim.transform.position = hit.point;
                            if (Input.GetMouseButtonUp(0)) {
                                TowerConfig towerConfig = TowerHelper.GetTowerConfigById(curDragTowerID);
                                if (towerBase.CanBuild) {
                                    if (LevelScene.LevelLifeTimeManager.HasEnoughEnergyToBuildTower(towerConfig.energeCost))
                                        towerBase.BuildTower();
                                    else
                                        EventManager.Get().Fire(this, (int)EventID.TipPanel_ShowTip, new StringEventArgs("没有足够能量"));
                                }
                            }else if (Input.GetMouseButtonUp(1)) {
                                ResetDragData();
                            }
                        } else {
                            // 没拖到塔座上点击鼠标取消拖拽
                            dragTowerRedRim.SetActive(true);
                            dragTowerBlueRim.SetActive(false);
                            dragTowerRedRim.transform.position = hit.point;
                            if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0)) {
                                ResetDragData();
                            }
                        }
                    }
                }

            }
        }

        void CreateDragRim() {
            dragTowerRedRim = LoaderHelper.Get().InstantiatePrefab($"{curDragTowerID}R");
            dragTowerBlueRim = LoaderHelper.Get().InstantiatePrefab($"{curDragTowerID}B");
        }

        void ResetDragData() {
            Addressables.Release(dragTowerRedRim);
            Addressables.Release(dragTowerBlueRim);
            Destroy(dragTowerRedRim);
            Destroy(dragTowerBlueRim);
            dragTowerRedRim = dragTowerBlueRim = null;
            curDragTowerID = -1;
        }
    }
}