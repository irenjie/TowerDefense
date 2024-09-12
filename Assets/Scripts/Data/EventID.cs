

namespace Event {
    public enum EventID {
        /// <summary>
        /// 显示提示
        /// </summary>
        TipsPanelShowTips = 0,
        /// <summary>
        /// 设置面板切换
        /// </summary>
        SwitchOptionsPanel = 1,
        /// <summary>
        /// 切换百科内容显示
        /// </summary>
        SwitchIntroduction = 2,
        /// <summary>
        /// 设置波数
        /// </summary>
        SetWaveNum = 3,
        /// <summary>
        /// 设置总波数
        /// </summary>
        SetTotalWaveNum = 4,
        /// <summary>
        /// 设置生命值
        /// </summary>
        SetHeartNum = 5,
        /// <summary>
        /// 设置能量值s
        /// </summary>
        SetEnergyNum = 6,
        /// <summary>
        /// 初始化战斗信息
        /// </summary>
        InitCombat = 7,
        /// <summary>
        /// 设置关卡名
        /// </summary>
        SetLevelName = 8,
        /// <summary>
        /// 设置关卡故事情节
        /// </summary>
        SetStoryDesc = 9,
        /// <summary>
        /// 暂停并且暂停的UI变化
        /// </summary>
        PauseNUIChange = 10,
        /// <summary>
        /// 取消暂停并且取消暂停的UI变化
        /// </summary>
        PlayNUIChange = 11,
        /// <summary>
        /// 关卡难度变化
        /// </summary>
        DiffcLevelChange = 12,
        /// <summary>
        /// 所有技能CD减少
        /// </summary>
        ReduceAllSkillCD = 13,
        /// <summary>
        /// 技能使用结束
        /// </summary>
        SkillUseEnd = 14,
    }
}