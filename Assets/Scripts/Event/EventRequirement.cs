/// <summary>
/// �¼���������
/// </summary>
public enum EventTrigger
{
    [EnumName("����ɫ����")]
    OnCreated = 0,
    OnDied = 1,
}

/// <summary>
/// �����¼���Ч����������
/// </summary>
public enum RoomEventEffectTrigger
{
    [EnumName("��ҽ��뷿��ʱ����")]
    OnEnter = 0,
    [EnumName("����뿪����ʱ����")]
    OnLeave = 1,
    [EnumName("���ͣ�����䣨���غ�ʱ������")]
    OnStay = 2
}

/// <summary>
/// �¼�����ǰ������
/// </summary>
public enum EventTriggerPre
{
    [EnumName("��")]
    None = 0,
    [EnumName("����ʱ����")]
    TimeDuration = 1,
    [EnumName("���������0~100��")]
    Random = 2,
    [EnumName("��������")]
    EnemyCount = 3,
    [EnumName("��ǰ�غ���")]
    RoundSpent = 4
}

/// <summary>
/// �¼�ִ��Ч��
/// </summary>
public enum EventEffect
{
    [EnumName("�ı���ֵ")]
    ChangeValue = 0,
    [EnumName("�ı�Buffer")]
    ChangeBuffer = 1,
    [EnumName("�ƶ�����")]
    MoveObject = 2,
    [EnumName("���ɶ���")]
    CreateObject = 3
}

/// <summary>
/// �¼�ִ�ж���
/// </summary>
public enum EventTarget
{
    [EnumName("��ǰ���")]
    CurrentPlayer = 0,
    [EnumName("�������")]
    AllPlayer = 1,
    [EnumName("���з��������")]
    AllPlayerInRoom = 2,
    [EnumName("���з��������")]
    RandomPlayer = 3,
    [EnumName("���е���")]
    AllEnemy = 4,
    [EnumName("���з����ڵ���")]
    AllEnemyInRoom = 5,
    [EnumName("���з��������")]
    RandomEnemy = 6,
}

/// <summary>
/// �¼�ִ�ж���ɸѡ����
/// </summary>
public enum EventTargetFilter
{
    [EnumName("��")]
    None = 0,
    [EnumName("�ж�������ֵ")]
    CheckValue = 1,
    [EnumName("�ж������Ƿ�Ϊ��ǰ���")]
    IsCurrentPlayer = 2
}

/// <summary>
/// ���û������������
/// </summary>
public enum EventValueType
{
    [EnumName("����ֵ")]
    HealthPoint = 0,
    [EnumName("�������ֵ")]
    MaxHealthPoint = 1,
    [EnumName("������")]
    Attack = 2,
    [EnumName("������")]
    Defense = 3,
    [EnumName("�ٶȣ�Ӱ���ж�˳��")]
    Speed = 4,
    [EnumName("�ж���")]
    ActionPoint = 5,
    [EnumName("����ж���")]
    MaxActionPoint = 6,
    [EnumName("�ƶ��ٶȣ�Ӱ��ÿ���ж�������ƶ����룩")]
    MoveSpeed = 7,
    [EnumName("������ȴ")]
    SkillColdDown = 8
}

/// <summary>
/// ����ɸѡ
/// </summary>
public enum EventValueFilter
{
    [EnumName("����")]
    Single = 0,
    [EnumName("���")]
    Multi = 1,
    [EnumName("����")]
    All = 2
}

/// <summary>
/// �������͸ı䷽ʽ
/// </summary>
public enum EventValueChangeType
{
    [EnumName("ֱ������")]
    Set = 0,
    [EnumName("����")]
    Add = 1,
    [EnumName("���Ӱٷֱȣ���λ��%��")]
    AddPercent = 2,
    [EnumName("����")]
    Minus = 3,
    [EnumName("���ٰٷֱȣ���λ��%��")]
    MinusPercent = 4,
    [EnumName("����")]
    Multiply = 5,
}