using UnityEngine;
using System.Collections.Generic;

class Room_Floors
{
    private List<RoomLevel> RoomData;
    private int FloorNumber = 1;
    private int FloorState;
    private bool ForceFloor;

    //***************************************************************************************************

    public Room_Floors(List<RoomLevel> roomData, int floorState)
    {
        RoomData = roomData; FloorState = floorState;
    }

    //***************************************************************************************************

    public void Generate()
    {
        DetermineFloorState();
        CreateFloor();
    }

    //***************************************************************************************************

    void DetermineFloorState ()
    {
        switch (FloorState)
        {
            case 0:
                ForceFloor = ReturnRandomBool();
                break;
            case 1:
                ForceFloor = true;
                break;
            case 3:
                ForceFloor = false;
                break;
        }
    }

    //***************************************************************************************************

    bool ReturnRandomBool ()
    {
        float RandomNumber = Random.Range(0f, 1f);
        if (RandomNumber < 0.5f) { return false; }
        else { return true; }
    }

    //***************************************************************************************************

    void CreateFloor ()
    {
        if (ForceFloor == true) {
            foreach (RoomPosition Pos in RoomData[0].RoomPositions) {
                if (Pos.Value != 2) { Pos.Value = 1; }
            }
        }
    }

    //***************************************************************************************************
}

