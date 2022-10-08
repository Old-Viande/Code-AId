/// <summary>
/// 事件触发条件
/// </summary>
public enum EventTrigger
{
    [EnumName("当角色生成")]
    OnCreated = 0,
    OnDied = 1,
}

/// <summary>
/// 房间事件的效果触发类型
/// </summary>
public enum RoomEventEffectTrigger
{
    [EnumName("玩家进入房间时触发")]
    OnEnter = 0,
    [EnumName("玩家离开房间时触发")]
    OnLeave = 1,
    [EnumName("玩家停留房间（几回合时）触发")]
    OnStay = 2
}

/// <summary>
/// 事件触发前置条件
/// </summary>
public enum EventTriggerPre
{
    [EnumName("无")]
    None = 0,
    [EnumName("触发时间间隔")]
    TimeDuration = 1,
    [EnumName("随机触发（0~100）")]
    Random = 2,
    [EnumName("敌人数量")]
    EnemyCount = 3,
    [EnumName("当前回合数")]
    RoundSpent = 4
}

/// <summary>
/// 事件执行效果
/// </summary>
public enum EventEffect
{
    [EnumName("改变数值")]
    ChangeValue = 0,
    [EnumName("改变Buffer")]
    ChangeBuffer = 1,
    [EnumName("移动对象")]
    MoveObject = 2,
    [EnumName("生成对象")]
    CreateObject = 3
}

/// <summary>
/// 事件执行对象
/// </summary>
public enum EventTarget
{
    [EnumName("当前玩家")]
    CurrentPlayer = 0,
    [EnumName("所有玩家")]
    AllPlayer = 1,
    [EnumName("所有房间内玩家")]
    AllPlayerInRoom = 2,
    [EnumName("所有房间内玩家")]
    RandomPlayer = 3,
    [EnumName("所有敌人")]
    AllEnemy = 4,
    [EnumName("所有房间内敌人")]
    AllEnemyInRoom = 5,
    [EnumName("所有房间内玩家")]
    RandomEnemy = 6,
}

/// <summary>
/// 事件执行对象筛选条件
/// </summary>
public enum EventTargetFilter
{
    [EnumName("无")]
    None = 0,
    [EnumName("判定对象数值")]
    CheckValue = 1,
    [EnumName("判定对象是否为当前玩家")]
    IsCurrentPlayer = 2
}

/// <summary>
/// 设置或检查的数据类型
/// </summary>
public enum EventValueType
{
    [EnumName("生命值")]
    HealthPoint = 0,
    [EnumName("最大生命值")]
    MaxHealthPoint = 1,
    [EnumName("攻击力")]
    Attack = 2,
    [EnumName("防御力")]
    Defense = 3,
    [EnumName("速度（影响行动顺序）")]
    Speed = 4,
    [EnumName("行动点")]
    ActionPoint = 5,
    [EnumName("最大行动点")]
    MaxActionPoint = 6,
    [EnumName("移动速度（影响每个行动点最大移动距离）")]
    MoveSpeed = 7,
    [EnumName("技能冷却")]
    SkillColdDown = 8
}

/// <summary>
/// 数据筛选
/// </summary>
public enum EventValueFilter
{
    [EnumName("单个")]
    Single = 0,
    [EnumName("多个")]
    Multi = 1,
    [EnumName("所有")]
    All = 2
}

/// <summary>
/// 数据类型改变方式
/// </summary>
public enum EventValueChangeType
{
    [EnumName("直接设置")]
    Set = 0,
    [EnumName("增加")]
    Add = 1,
    [EnumName("增加百分比（单位：%）")]
    AddPercent = 2,
    [EnumName("减少")]
    Minus = 3,
    [EnumName("减少百分比（单位：%）")]
    MinusPercent = 4,
    [EnumName("倍数")]
    Multiply = 5,
}