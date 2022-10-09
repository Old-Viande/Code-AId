using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class RoomEventEditor : EditorWindow
{
    private static RoomEventEditor m_Window = null;
    private RoomEventSO m_RoomEventSO;
    private int m_CurrentIndex = 0;

    private Vector2 m_ScrollPosition;
    private ReorderableList m_DescriptionRL;
    private ReorderableList m_ButtonEventRL;

    [MenuItem("Room Event/Editor")]
    static void OpenWindow()
    {
        m_Window = GetWindow<RoomEventEditor>();
        m_Window.titleContent.text = "Room Event Editor";
        m_Window.minSize = new Vector2(820, 360);
        m_Window.Show();
    }

    private void OnEnable()
    {
        m_RoomEventSO = AssetDatabase.LoadAssetAtPath<RoomEventSO>("Assets/Scripts/Event/RoomEventList.asset");
        if (m_RoomEventSO != null)
        {
            m_RoomEventSO.hideFlags = HideFlags.NotEditable | HideFlags.DontSave;

            RefreshReorderableLists(0);
        }
        else
        {
            Debug.LogError("未找到文件或者文件出错！Assets/Scripts/Event/RoomEventList.asset");
            if (m_Window != null)
                m_Window.Close();
        }
    }

    private void OnGUI()
    {
        // Menu bar
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("新 建", GUILayout.Width(100)))
        {
            m_RoomEventSO.AddNewRoomEvent();
            m_CurrentIndex = m_RoomEventSO.GetListCount() - 1;
            RefreshReorderableLists(m_CurrentIndex);
        }
        if (GUILayout.Button("删 除", GUILayout.Width(100)))
        {
            m_RoomEventSO.RemoveRoomEvent(m_CurrentIndex);
            m_CurrentIndex = m_CurrentIndex == 0 ? 0 : (m_CurrentIndex - 1);
            RefreshReorderableLists(m_CurrentIndex);
        }
        if (GUILayout.Button("保 存", GUILayout.Width(100)))
        {

        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            // Evnet List
            EditorGUILayout.BeginVertical(GUILayout.Width(210), GUILayout.ExpandHeight(true));
            m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);
            for (int i = 0; i < m_RoomEventSO.GetListCount(); i++)
            {
                GUIStyle style = GUI.skin.button;
                var originColor = style.normal.textColor;
                if (m_CurrentIndex == i)
                    style.normal.textColor = Color.blue;
                if (GUILayout.Button(m_RoomEventSO.GetRoomEvent(i).Name, style, GUILayout.Width(190)))
                {
                    m_CurrentIndex = i;
                    RefreshReorderableLists(m_CurrentIndex);
                }
                style.normal.textColor = originColor;
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();

            Handles.DrawLine(new Vector3(210, 30), new Vector3(210, 360));

            // Event Inspector
            EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            var currentRoomEvent = m_RoomEventSO.GetRoomEvent(m_CurrentIndex);
            if (currentRoomEvent != null)
            {
                currentRoomEvent.Name = EditorGUILayout.TextField("名称", currentRoomEvent.Name);
                currentRoomEvent.ID = EditorGUILayout.TextField("ID", currentRoomEvent.ID);
                if (m_DescriptionRL != null)
                {
                    m_DescriptionRL.DoLayoutList();
                }

                var originColor = GUI.backgroundColor;
                GUI.backgroundColor = Color.cyan;
                currentRoomEvent.RoomEventTriggerFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(currentRoomEvent.RoomEventTriggerFoldout, "事件触发");
                GUI.backgroundColor = originColor;
                if (currentRoomEvent.RoomEventTriggerFoldout)
                {
                    EditorGUI.indentLevel++;
                    currentRoomEvent.RoomEventEffectTrigger = (RoomEventEffectTrigger)EditorGUILayout.Popup("触发类型", (int)currentRoomEvent.RoomEventEffectTrigger, GetNames(typeof(RoomEventEffectTrigger)));
                    currentRoomEvent.EventTriggerPre = (EventTriggerPre)EditorGUILayout.Popup("前置条件", (int)currentRoomEvent.EventTriggerPre, GetNames(typeof(EventTriggerPre)));
                    EditorGUI.indentLevel++;
                    switch (currentRoomEvent.EventTriggerPre)
                    {
                        case EventTriggerPre.TimeDuration:
                            currentRoomEvent.EventTriggerPreArg1 = EditorGUILayout.FloatField("时间间隔", currentRoomEvent.EventTriggerPreArg1);
                            break;
                        case EventTriggerPre.Random:
                            currentRoomEvent.EventTriggerPreArg2 = EditorGUILayout.Slider("小于等于", currentRoomEvent.EventTriggerPreArg2, 0.0f, 100.0f);
                            break;
                        case EventTriggerPre.EnemyCount:
                            DrawValueChange(ref currentRoomEvent.EventTriggerPreArg3, ref currentRoomEvent.EventTriggerPreArg4);
                            break;
                        case EventTriggerPre.RoundSpent:
                            currentRoomEvent.EventTriggerPreArg5 = EditorGUILayout.FloatField("回合数", currentRoomEvent.EventTriggerPreArg5);
                            break;
                        case EventTriggerPre.None:
                        default:
                            break;
                    }
                    EditorGUI.indentLevel -= 2;
                }
                EditorGUILayout.EndFoldoutHeaderGroup();

                GUI.backgroundColor = new Color32(255, 192, 203, 255);
                currentRoomEvent.EventEffectFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(currentRoomEvent.EventEffectFoldout, "事件执行效果");
                GUI.backgroundColor = originColor;
                if (currentRoomEvent.EventEffectFoldout)
                {
                    EditorGUI.indentLevel++;
                    currentRoomEvent.EventTarget = (EventTarget)EditorGUILayout.Popup("执行效果对象", (int)currentRoomEvent.EventTarget, GetNames(typeof(EventTarget)));
                    currentRoomEvent.EventTargetFilter = (EventTargetFilter)EditorGUILayout.Popup("对象筛选条件", (int)currentRoomEvent.EventTargetFilter, GetNames(typeof(EventTargetFilter)));
                    EditorGUI.indentLevel++;
                    switch (currentRoomEvent.EventTargetFilter)
                    {
                        case EventTargetFilter.CheckValue:
                            currentRoomEvent.EventTargetCheckValueType = (EventValueType)EditorGUILayout.Popup("检查的数据", (int)currentRoomEvent.EventTargetCheckValueType, GetNames(typeof(EventValueType)));
                            EditorGUI.indentLevel++;
                            switch (currentRoomEvent.EventTargetCheckValueType)
                            {
                                case EventValueType.HealthPoint:
                                case EventValueType.MaxHealthPoint:
                                case EventValueType.Attack:
                                case EventValueType.Defense:
                                case EventValueType.Speed:
                                case EventValueType.ActionPoint:
                                case EventValueType.MaxActionPoint:
                                case EventValueType.MoveSpeed:
                                    DrawValueChange(ref currentRoomEvent.EventTargetCheckValueChangeType, ref currentRoomEvent.EventTargetFilterArg1);
                                    break;
                                case EventValueType.SkillColdDown:
                                    EditorGUILayout.HelpBox("技能关联目前未实现！", MessageType.Warning);
                                    break;
                                default:
                                    break;
                            }
                            EditorGUI.indentLevel--;
                            break;
                        case EventTargetFilter.IsCurrentPlayer:
                            currentRoomEvent.EventTargetFilterArg2 = EditorGUILayout.Toggle("是当前玩家", currentRoomEvent.EventTargetFilterArg2);
                            break;
                        case EventTargetFilter.None:
                        default:
                            break;
                    }
                    EditorGUI.indentLevel--;
                    currentRoomEvent.EventEffect = (EventEffect)EditorGUILayout.Popup("执行的效果", (int)currentRoomEvent.EventEffect, GetNames(typeof(EventEffect)));
                    switch (currentRoomEvent.EventEffect)
                    {
                        case EventEffect.ChangeValue:
                            currentRoomEvent.EventEffectChangeValueType = (EventValueType)EditorGUILayout.Popup("修改的数据", (int)currentRoomEvent.EventEffectChangeValueType, GetNames(typeof(EventValueType)));
                            EditorGUI.indentLevel++;
                            switch (currentRoomEvent.EventEffectChangeValueType)
                            {
                                case EventValueType.HealthPoint:
                                case EventValueType.MaxHealthPoint:
                                case EventValueType.Attack:
                                case EventValueType.Defense:
                                case EventValueType.Speed:
                                case EventValueType.ActionPoint:
                                case EventValueType.MaxActionPoint:
                                case EventValueType.MoveSpeed:
                                    DrawValueChange(ref currentRoomEvent.EventEffectChangeValueChangeType, ref currentRoomEvent.EventEffectChangeValue);
                                    break;
                                case EventValueType.SkillColdDown:
                                    EditorGUILayout.HelpBox("技能关联目前未实现！", MessageType.Warning);
                                    break;
                                default:
                                    break;
                            }
                            EditorGUI.indentLevel--;
                            break;
                        case EventEffect.ChangeBuffer:
                            EditorGUILayout.HelpBox("改变Buffer目前未实现！", MessageType.Warning);
                            break;
                        case EventEffect.MoveObject:
                            EditorGUILayout.HelpBox("移动对象目前未实现！", MessageType.Warning);
                            break;
                        case EventEffect.CreateObject:
                            EditorGUILayout.HelpBox("生成对象目前未实现！", MessageType.Warning);
                            break;
                        default:
                            break;
                    }
                    EditorGUI.indentLevel--;
                }

                
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
    }

    private string[] GetNames(Type type)
    {
        Array values = Enum.GetValues(type);
        List<string> names = Enum.GetNames(type).ToList();

        for (int i = 0; i < names.Count; i++)
        {
            var enumVal = (Enum)values.GetValue(i);
            MemberInfo[] member = enumVal.GetType().GetMember(enumVal.ToString());
            object[] customAttributes = member[0].GetCustomAttributes(typeof(EnumNameAttribute), false);
            EnumNameAttribute enumName = (customAttributes.Length != 0) ? ((EnumNameAttribute)customAttributes[0]) : default;
            if (enumName != null && !string.IsNullOrEmpty(enumName.Name))
                names[i] = enumName.Name;
            else
                names[i] = ObjectNames.NicifyVariableName(names[i]);
        }

        return names.ToArray();
    }

    private void DrawValueChange(ref EventValueChangeType changeType, ref float value)
    {
        changeType = (EventValueChangeType)EditorGUILayout.Popup("改变方式", (int)changeType, GetNames(typeof(EventValueChangeType)));
        EditorGUI.indentLevel++;
        switch (changeType)
        {
            case EventValueChangeType.Set:
                value = EditorGUILayout.FloatField("设置为", value);
                break;
            case EventValueChangeType.Add:
                value = EditorGUILayout.FloatField("增加", value);
                break;
            case EventValueChangeType.AddPercent:
                value = EditorGUILayout.FloatField("增加的百分比", value);
                break;
            case EventValueChangeType.Minus:
                value = EditorGUILayout.FloatField("减少", value);
                break;
            case EventValueChangeType.MinusPercent:
                value = EditorGUILayout.FloatField("减少的百分比", value);
                break;
            case EventValueChangeType.Multiply:
                value = EditorGUILayout.FloatField("倍数", value);
                break;
            default:
                break;
        }
        EditorGUI.indentLevel--;
    }

    private void RefreshReorderableLists(int index)
    {
        RoomEvent roomEvent = m_RoomEventSO.GetRoomEvent(index);
        if (roomEvent == null)
        {
            m_DescriptionRL = null;
            m_ButtonEventRL = null;
        }
        else
        {
            m_DescriptionRL = new ReorderableList(roomEvent.Description, typeof(string), false, true, true, true)
            {
                drawHeaderCallback = (rect) =>
                {
                    EditorGUI.LabelField(rect, "Description");
                },
                drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    roomEvent.Description[index] = EditorGUI.TextArea(rect, roomEvent.Description[index]);
                }
            };
            m_ButtonEventRL = new ReorderableList(roomEvent.ButtonEvent, typeof(UnityEngine.Events.UnityEvent), true, true, true, true)
            {
                drawHeaderCallback = (rect) =>
                {
                    EditorGUI.LabelField(rect, "Button Events");
                },
                drawElementCallback = (rect, index, isActive, isFocused) =>
                {

                }
            };
        }
    }
}