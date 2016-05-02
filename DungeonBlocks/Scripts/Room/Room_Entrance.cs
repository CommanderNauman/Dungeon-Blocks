using UnityEngine;
using System.Collections.Generic;


class Room_Entrance 
{
    List<RoomLevel> RoomData;

    List<RoomPosition> Doors = new List<RoomPosition>();

    //List<Vector3> Points = new List<Vector3>();

    //***************************************************************************************************

    public Room_Entrance() { }
    public Room_Entrance(List<RoomLevel> roomData)
    {
        RoomData = roomData;
    }

    //***************************************************************************************************

    public void Generate()
    {
        FindAllDoors();
        ChooseDoor();
    }

    //***************************************************************************************************

    void ChooseDoor()
    {
        int R = Random.Range(0, Doors.Count-1);
        Doors[R].Value = 3;
    }

    //***************************************************************************************************

    void FindAllDoors()
    {
        foreach (RoomLevel level in RoomData)
        {
            foreach (RoomPosition pos in level.RoomPositions)
            {
                if (pos.Value == 2) { Doors.Add(pos); }
            }
        }
    }

    //***************************************************************************************************
}
