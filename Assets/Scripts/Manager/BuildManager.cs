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
        // ��ǰѡ��Ҫ��������� ID
        int curDragTowerID = -1;
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
            // �϶���
            if (curDragTowerID != -1) {
                if (dragTowerRedRim == null || dragTowerBlueRim == null)
                    CreateDragRim();
                else {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hit, towerStation)) {
                        GameObject hitObj = hit.collider.gameObject;
                        if (hitObj.TryGetComponent<Buildable>(out Buildable towerBase)) {
                            // ���������������
                            dragTowerRedRim.SetActive(false);
                            dragTowerBlueRim.SetActive(true);
                            dragTowerBlueRim.transform.position = hit.point;
                            if (Input.GetMouseButtonUp(0)) {
                                TowerConfig towerConfig = TowerHelper.GetTowerConfigById(curDragTowerID);
                                if (towerBase.CanBuild) {
                                    if (LevelScene.LevelLifeTimeManager.HasEnoughEnergyToBuildTower(towerConfig.energeCost))
                                        towerBase.BuildTower();
                                    else
                                        EventManager.Get().Fire(this, (int)EventID.TipPanel_ShowTip, new StringEventArgs("û���㹻����"));
                                }
                            }else if (Input.GetMouseButtonUp(1)) {
                                ResetDragData();
                            }
                        } else {
                            // û�ϵ������ϵ�����ȡ����ק
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