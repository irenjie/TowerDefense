
namespace Buff {

    /// <summary>
    /// Buff ʱ����·�ʽ
    /// </summary>
    public enum EBuffTimeUpdate {
        Add,
        Replace,
        Keep
    }

    /// <summary>
    /// Buff �������·�ʽ
    /// </summary>
    public enum EBuffRemoveStackUpdate {
        Clear,
        Reduce
    }

    public enum EDamageType {
        Physical,
        Laser,
        Fire,
        Lightning,
        Frost
    }
}