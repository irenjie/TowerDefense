using Extensions;
using Helper;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MUI {

    public class UIManager : MonoBehaviour {
        #region ui�㼶
        // panel ����㼶
        const ushort panelPaddingLayer = 100;
        // group ����㼶
        const ushort groupPaddingLayer = 3000;
        // ��ǰ group��manager��order,group��ʼ�㼶Ϊ order x groupPaddingLayer
        ushort order;
        #endregion

        // ����չʾ��ҳ��
        readonly List<BasePanel> panels = new List<BasePanel>();

        #region ��ȡ UIManager
        static GameObject root;
        static readonly Dictionary<int, UIManager> managers = new Dictionary<int, UIManager>();
        // ս�������ĳ�פ����
        public static UIManager Combat => Get(1, "Combat");
        // ǰ�Ž���
        public static UIManager Front => Get(2, "Front");
        private static UIManager Get(ushort order, string name) {
            if (!managers.ContainsKey(order)) {
                if (root == null) {
                    root = new GameObject("UI Managers");
                    DontDestroyOnLoad(root.gameObject);
                }

                var g = new GameObject(name);
                g.transform.parent = root.transform;
                UIManager manager = g.AddComponent<UIManager>();
                manager.order = order;
                managers.Add(order, manager);
            }
            return managers[order];
        }

        #endregion

        #region ҳ�濪��
        /*
         * ҳ�濪��ʱ�������£�
         * ҳ��㼶
         * ҳ�����ֲ㼶��͸����
         * ����������ʾ
         */

        /// <summary>
        /// ��תҳ��
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="address"></param>
        /// <returns></returns>
        public T Navigation<T>(string address) where T : BasePanel {
            Clear();
            T panel = LoaderHelper.Get().InstantiatePrefab(address, transform).GetComponent<T>();
            panels.Add(panel);
            panel.PlayShowEffect();
            panel.sortingOrder = order * groupPaddingLayer;
            UpdateMask();
            return panel;
        }

        /// <summary>
        /// ����ҳ��
        /// </summary>
        /// <returns></returns>
        public T PopUp<T>(string address) where T : BasePanel {
            var lastTopPanel = TopPanel;
            T panel = LoaderHelper.Get().InstantiatePrefab(address, transform).GetComponent<T>();
            panels.Add(panel);
            panel.PlayShowEffect();
            panel.sortingOrder = lastTopPanel == null ? order * groupPaddingLayer : lastTopPanel.sortingOrder + panelPaddingLayer;
            if (panel.IsFullScreen) {
                lastTopPanel.SetGraphic(false);
            }

            UpdateMask();
            return panel;
        }

        public void Close(BasePanel panel) {
            bool closingTop = panel == TopPanel;
            int panelCount = PanelCount;
            BasePanel newTopPanel = panels.TryGetAt<BasePanel>(panelCount - 2);

            if (closingTop) {
                if (panel.IsFullScreen && newTopPanel != null) {
                    newTopPanel.SetGraphic(true);
                }
            }

            panel.PlayCloseEffect(() => {
                Addressables.ReleaseInstance(panel.gameObject);
            });
            panels.RemoveAt(panelCount - 1);

            if (closingTop) {
                UpdateMask();
            }
        }

        public void Clear() {
            foreach (var panel in panels) {
                panel.PlayCloseEffect(() => {
                    Addressables.ReleaseInstance(panel.gameObject);
                });
            }
            UpdateMask();
            panels.Clear();
        }

        public static void ClearAll() {
            foreach (var manager in managers) {
                manager.Value.Clear();
            }
        }


        /// <summary>
        /// ���²㼶������
        /// �����ϲ�ҳ���ȫ��ʱ��ʾ
        /// </summary>
        private void UpdateMask() {
            if (this != Front)
                return;

            int panelCount = PanelCount;

            if (panelCount > 0) {

                // ��������
                var lastPanel = panels.Last();
                if (!lastPanel.IsFullScreen) {
                    var panelLayer = lastPanel.sortingOrder;
                    UIMaskHelper.Get().UpdateLayer(panelLayer - 1);
                }
            } else {
                UIMaskHelper.Get().UpdateLayer(-1, 0f, false);
            }

        }
        #endregion

        #region ҳ���ȡ
        public ushort PanelCount => (ushort)panels.Count;
        public BasePanel TopPanel => panels.LastOrDefault();
        #endregion

    }
}