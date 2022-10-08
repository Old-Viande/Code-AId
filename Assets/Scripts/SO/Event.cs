using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Event 
{
    public string eventName;
    public string eventID;
    public eventType currentEventType;
    public enum eventType
    {
        RandomEnemyCreate,
        EnemyCreate,
        SpecialEnemyCreate,

        ColdDownSkillAdd,
        CountSkillAdd,

        Enhance,       
    } 
    [TextArea]
    public string[]eventText;

    public List<EventOption> options = new List<EventOption>();
}
