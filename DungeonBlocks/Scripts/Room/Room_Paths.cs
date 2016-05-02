using UnityEngine;
using System.Collections.Generic;

class Room_Paths
{
    List<RoomLevel> RoomData;

    List<RoomPosition> Doors = new List<RoomPosition>();

    //List<Vector3> Points = new List<Vector3>();

    //***************************************************************************************************

    public Room_Paths() { }
    public Room_Paths(List<RoomLevel> roomData)
    {
        RoomData = roomData;
    }

    //***************************************************************************************************

    public void Generate ()
    {
        FindAllDoors();
        SetPaths();
    }

    //***************************************************************************************************

    void SetRoomValues(List<Vector3> Points)
    {
        foreach(Vector3 Point in Points)
        {
            FindRoomDataPoint(Point);
        }
    }

    //***************************************************************************************************

    void FindRoomDataPoint (Vector3 Point)
    {
        foreach (RoomLevel level in RoomData)
        {
            foreach (RoomPosition Pos in level.RoomPositions)
            {
                if (Point == Pos.Position) { Pos.Value = 1; }
            }
        }
    }

    //***************************************************************************************************

    void SetPaths()
    {
        for (int i = 1; i < Doors.Count; i++)
        {
            List<Vector3> Points = FindPath(Doors[i-1].Position, Doors[i].Position);
            SetRoomValues(Points);
        }
    }

    //***************************************************************************************************

    List<Vector3> FindPath(Vector3 DoorOne , Vector3 DoorTwo)
    {
        List<Vector3> points = new List<Vector3>();

        Vector3 Start = DoorOne;

        while (Start != DoorTwo )
        {
            Start = MoveTowards(Start, DoorTwo);
            if (Start.x == DoorTwo.x || Start.z == DoorTwo.z) { Start = new Vector3(Start.x, DoorTwo.y, Start.z); }
            if (Start != DoorTwo) { points.Add(Start); }
        }
        /*
        foreach( Vector3 point in points )
        {
            MonoBehaviour.print(point);
        }
        MonoBehaviour.print("here2");
        */
        return points;
    }

    //***************************************************************************************************
    /*
    Vector3 MidPoint (Vector3 Start, Vector3 End)
    {


        return new Vector3();
    }
    */
    //***************************************************************************************************

    Vector3 MoveTowards (Vector3 Start , Vector3 End)
    {
        int x = 1;
        if (Start.x > End.x) { x = -1; }
        int z = 1;
        if (Start.z > End.z) { z = -1; }

        //int dx = Mathf.RoundToInt(Mathf.Abs(Start.x - End.x));
        //int dz = Mathf.RoundToInt(Mathf.Abs(Start.z - End.z));

        if (Start.x != End.x)
        //if (dx > dz)
        {
            Start = Start + new Vector3(x, 0, 0);
        }
        else
        {
            Start = Start + new Vector3(0, 0, z);
        }

        return Start;
    }

    //***************************************************************************************************

    void FindAllDoors()
    {
        foreach(RoomLevel level in RoomData) {
            foreach (RoomPosition pos in level.RoomPositions) {
                if (pos.Value == 2){ Doors.Add(pos); /*MonoBehaviour.print("here3");*/ }
                //MonoBehaviour.print("here");
            }
        }
    }

    //***************************************************************************************************
}

