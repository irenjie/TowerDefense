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
        // ��ǰѡ��Ҫ��������� ID
        public string curDragTowerRealID = string.Empty;
        // Ҫ����������϶�ʱ���ɽ������������ϣ�ʱ����
        GameObject dragTowerRedRim = null;
        // Ҫ����������϶�ʱ�ɽ����������ϣ�ʱ����
        GameObject dragTowerBlueRim = null;
        // ��ǰ��ʾ�˵�����������ѡ�к����������������������
        Buildable curShowMenuTowerBase = null;

        // �������ʱ���߼��㼶
        public LayerMask towerStation;

        //Ray ray;
        //RaycastHit hit;
        //GameObject hitObj;

        private void Update() {
            SelectTowerBaseUpdate();
            DragTowerUpdate();
        }

        /// <summary>
        /// ������������������������������
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
        /// ��������˵��е���������ʼ�϶�
        /// </summary>
        void DragTowerUpdate() {
            if (string.IsNullOrEmpty(curDragTowerRealID))
                return;

            if (dragTowerRedRim == null || dragTowerBlueRim == null) {
                CreateDragRim();
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // ���������������
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
                                EventManager.Get().Fire(this, (int)EventID.TipPanel_ShowTip, new StringEventArgs("û���㹻����"));
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

            // ���ڵ�����
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