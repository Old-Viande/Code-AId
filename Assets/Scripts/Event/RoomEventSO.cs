using System;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
[Serializable]
public class RoomEventSO : ScriptableObject
{
    public List<RoomEvent> RoomEventList = new();

    public int GetListCount()
    {
        return RoomEventList.Count;
    }

    public RoomEvent GetRoomEvent(int index)
    {
        if (index < 0 || index >= RoomEventList.Count)
            return null;

        return RoomEventList[index];
    }

    public void SetRoomEvent(int index, RoomEvent roomEvent)
    {
        if (index < 0 || index >= RoomEventList.Count)
            return;

        RoomEventList[index] = roomEvent;
    }

    private bool AnyHasID(string ID)
    {
        for (int i = 0; i < RoomEventList.Count; i++)
        {
            if (RoomEventList[i].ID == ID)
                return true;
        }
        return false;
    }

    public void AddNewRoomEvent()
    {
        var RoomEvent = new RoomEvent();
        int id = 0;
        while (AnyHasID(RoomEvent.ID))
        {
            id = int.Parse(RoomEvent.ID);
            id += 1;
            RoomEvent.ID = id.ToString();
        }
        RoomEvent.Name += string.Format($" {id}");
        RoomEventList.Add(RoomEvent);
    }

    public void RemoveRoomEvent( int index)
    {
        if (index < 0 || index >= RoomEventList.Count)
            return;

        RoomEventList.RemoveAt(index);
    }
}
