using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Event",menuName ="Event")]
public class Event_SO : ScriptableObject
{
    // Start is called before the first frame update
    public List<Event> events = new List<Event>();
    public Dictionary<string, Event> eventSave = new Dictionary<string, Event>();
#if UNITY_EDITOR
    void OnValidate()
    {
        eventSave.Clear();
        foreach (var property in events)
        {
            if (!eventSave.ContainsKey(property.eventName))

            {
                eventSave.Add(property.eventName, property);

            }
        }
    }
#else
    void Awake()
    {
         eventSave.Clear();
        foreach (var property in events)
        {
            if (!eventSave.ContainsKey(property.eventName))

            {
                eventSave.Add(property.eventName, property);

            }
        }
    }
#endif
}
