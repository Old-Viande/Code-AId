using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomEvent
{
    public string ID = "0";
    public string Name = "New Room Event";
    [TextArea]
    public List<string> Description = new();

    #region 事件触发
    public bool RoomEventTriggerFoldout = true;
    public RoomEventEffectTrigger RoomEventEffectTrigger;
    public EventTriggerPre EventTriggerPre;
    public float EventTriggerPreArg1;
    [Range(0, 100)]
    public float EventTriggerPreArg2;
    public EventValueChangeType EventTriggerPreArg3;
    public float EventTriggerPreArg4;
    public float EventTriggerPreArg5;
    #endregion // 事件触发

    #region 事件执行
    public bool EventEffectFoldout = true;
    public EventTarget EventTarget;
    public EventTargetFilter EventTargetFilter;
    public EventValueType EventTargetCheckValueType;
    public EventValueChangeType EventTargetCheckValueChangeType;
    public float EventTargetFilterArg1;
    public bool EventTargetFilterArg2;
    public EventEffect EventEffect;
    public EventValueType EventEffectChangeValueType;
    public EventValueChangeType EventEffectChangeValueChangeType;
    public float EventEffectChangeValue;
    #endregion // 事件执行

    #region UI Buttons
    public List<UnityEngine.Events.UnityEvent> ButtonEvent = new();
    #endregion
}
