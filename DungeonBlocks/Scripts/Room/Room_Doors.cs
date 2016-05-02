using UnityEngine;
using System.Collections.Generic;

class Room_Doors
{
    List<RoomLevel> RoomData;
    Vector3 RoomDimensions;
    int NumberOfDoors;

    //***************************************************************************************************

    public Room_Doors() { }
    public Room_Doors(List<RoomLevel> roomData, Vector3 roomDimensions) { RoomData = roomData; RoomDimensions = roomDimensions; }

    //***************************************************************************************************

    public void Generate()
    {
        DecideNumberOfDoors();
        CreateDoors();
    }

    //***************************************************************************************************

    void DecideNumberOfDoors()
    {
        NumberOfDoors = Random.Range(2, 5);
    }

    //***************************************************************************************************

    void CreateDoors()
    {
        //CreateBaseDoor(RoomData[0].RoomPositions);

        for (int i = 0; i <= NumberOfDoors; i++)
        {
            CreateDoor(RoomData[Random.Range(0, RoomData.Count)].RoomPositions);
        }
    }

    //***************************************************************************************************

    void CreateDoor(List<RoomPosition> RoomPositions)
    {
        RoomPosition Pos = FindPosition(RoomPositions);
        DoorRotation(Pos);
        Pos.Value = 2;
    }

    //***************************************************************************************************

    RoomPosition FindPosition(List<RoomPosition> RoomPositions)
    {
        RoomPosition Pos;

        while(true)
        {
            int r = Random.Range(0, RoomPositions.Count);

            if (RoomPositions[r].Position.x == 0 ||
                RoomPositions[r].Position.x == RoomDimensions.x -1 ||
                RoomPositions[r].Position.z == 0 ||
                RoomPositions[r].Position.z == RoomDimensions.z -1
                )
            {
                    Pos = RoomPositions[r];
                    break;
            }
        }

        return Pos;
    }

    //***************************************************************************************************

    void DoorRotation(RoomPosition Pos)
    {
        if (Pos.Position.x == 0 )
        { Pos.Rotation = 90; }
        if (Pos.Position.x == RoomDimensions.x - 1)
        { Pos.Rotation = 270; }
        if (Pos.Position.z == 0 )
        { Pos.Rotation = 0; }
        if (Pos.Position.z == RoomDimensions.z - 1)
        { Pos.Rotation = 180; }
    }

    //***************************************************************************************************
}

