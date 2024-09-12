

namespace Event {
    public enum EventID {
        /// <summary>
        /// ��ʾ��ʾ
        /// </summary>
        TipsPanelShowTips = 0,
        /// <summary>
        /// ��������л�
        /// </summary>
        SwitchOptionsPanel = 1,
        /// <summary>
        /// �л��ٿ�������ʾ
        /// </summary>
        SwitchIntroduction = 2,
        /// <summary>
        /// ���ò���
        /// </summary>
        SetWaveNum = 3,
        /// <summary>
        /// �����ܲ���
        /// </summary>
        SetTotalWaveNum = 4,
        /// <summary>
        /// ��������ֵ
        /// </summary>
        SetHeartNum = 5,
        /// <summary>
        /// ��������ֵs
        /// </summary>
        SetEnergyNum = 6,
        /// <summary>
        /// ��ʼ��ս����Ϣ
        /// </summary>
        InitCombat = 7,
        /// <summary>
        /// ���ùؿ���
        /// </summary>
        SetLevelName = 8,
        /// <summary>
        /// ���ùؿ��������
        /// </summary>
        SetStoryDesc = 9,
        /// <summary>
        /// ��ͣ������ͣ��UI�仯
        /// </summary>
        PauseNUIChange = 10,
        /// <summary>
        /// ȡ����ͣ����ȡ����ͣ��UI�仯
        /// </summary>
        PlayNUIChange = 11,
        /// <summary>
        /// �ؿ��Ѷȱ仯
        /// </summary>
        DiffcLevelChange = 12,
        /// <summary>
        /// ���м���CD����
        /// </summary>
        ReduceAllSkillCD = 13,
        /// <summary>
        /// ����ʹ�ý���
        /// </summary>
        SkillUseEnd = 14,
    }
}