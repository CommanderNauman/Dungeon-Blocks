using UnityEngine;
using System.Collections.Generic;

class Room_Data
{
    List<RoomLevel> RoomData;

    Vector3 MinDimensions;
    Vector3 MaxDimensions;
    Vector3 RoomDimensions;

    //***************************************************************************************************

    public Room_Data() { }
    public Room_Data(List<RoomLevel> roomData , Vector3 Min , Vector3 Max)
    {
        RoomData = roomData; MinDimensions = Min; MaxDimensions = Max;
    }

    //***************************************************************************************************

    public Vector3 Generate()
    {
        SetRoomDimensions();
        SetRoomData();
        return RoomDimensions;
    }

    //***************************************************************************************************

    void SetRoomDimensions()
    {
        RoomDimensions.x = GetEvenNumber((int)MinDimensions.x, (int)MaxDimensions.x);
        RoomDimensions.z = GetEvenNumber((int)MinDimensions.z, (int)MaxDimensions.z);
        RoomDimensions.y = GetEvenNumber((int)MinDimensions.y, (int)MaxDimensions.y);
    }

    //***************************************************************************************************

    int GetEvenNumber(int Min, int Max)
    {
        int EvenNumber = Random.Range(Min, Max + 1);
        if (EvenNumber == 0 || EvenNumber == 1) { return EvenNumber = 1; }
        else { while (EvenNumber % 2 != 0) { EvenNumber = Random.Range(Min, Max + 1); } }
        return EvenNumber;
    }

    //***************************************************************************************************

    void SetRoomData()
    {
        SetLevels();
        SetNumberPositions();
    }

    //***************************************************************************************************

    void SetLevels()
    {
        List<RoomPosition> roomPositions = new List<RoomPosition>();

        for (int i = 0; i < RoomDimensions.y; i++)
        {
            RoomData.Add(new RoomLevel(roomPositions));
        }
    }

    //***************************************************************************************************

    void SetNumberPositions()
    {
        for (int y = 0; y < RoomData.Count; y++)
        {
            RoomData[y].RoomPositions = AddNewRoomPositions(y);
        }
    }

    //***************************************************************************************************

    List<RoomPosition> AddNewRoomPositions(int y)
    {
        List<RoomPosition> positions = new List<RoomPosition>();

        for (int z = 0; z < RoomDimensions.z; z++)
        {
            for (int x = 0; x < RoomDimensions.x; x++)
            {
                positions.Add(new RoomPosition(x, y, z));
            }
        }
        return positions;
    }

    //***************************************************************************************************
}