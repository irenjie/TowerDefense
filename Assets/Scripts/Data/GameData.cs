using Extensions;
using Helper;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Data {

    [Serializable]
    public class GameData {
        #region 游戏数据
        public int level;
        // 完成的关卡及最高分
        public SerializedDictionary<int, int> completedLevels;
        // 选中的上阵炮塔
        public List<int> selectedTowers;
        // 选中的上阵技能
        public List<int> selectedSkills;

        [NonSerialized] public LevelConfig selectedLevel;

        #endregion

        #region 保存与读取
        private static string saveDataPath => Path.Combine(Application.persistentDataPath, "savadata/savadata.json");
        private static GameData instance;

        public static GameData Get() {
            instance ??= LoadFromStorage();
            return instance;
        }

        public static void Save() {
            if (instance == null)
                return;

            try {
                FileHelper.SaveText(saveDataPath, instance.ToString());
            } catch (Exception e) {
                UnityDebugHelper.LogException(e);
            }
        }

        private static GameData LoadFromStorage() {
            string data = FileHelper.ReadText(saveDataPath);
            return LoadFromString(data);
        }

        private static GameData LoadFromString(string content) {
            GameData gameData = null;

            try {
                gameData = JsonUtility.FromJson<GameData>(content);
                gameData?.InvokeAfterLoad();
            } catch (Exception e) {
                UnityDebugHelper.LogException(e);
            }

            gameData ??= JsonUtility.FromJson<GameData>("{}");
            return gameData;
        }

        private void InvokeAfterLoad() {
        }

        public override string ToString() {
            return JsonUtility.ToJson(this);
        }
        #endregion

    }
}