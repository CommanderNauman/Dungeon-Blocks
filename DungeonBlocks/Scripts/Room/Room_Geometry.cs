using UnityEngine;
using System.Collections.Generic;

public class Room_Geometry : MonoBehaviour
{
    RoomModels Models;

    bool StartRoom;
    int TileDimension = 10;
    Vector3 RoomDimensions;
    List<RoomLevel> RoomData;
    
    GameObject EntrancePoint;
    GameObject StartHallway; 
    GameObject Holder;
    GameObject Pivot;

    //***************************************************************************************************

    public Room_Geometry (
        List<RoomLevel> roomData, 
        RoomModels models, 
        int tileDimension, 
        Vector3 roomDimension, 
        bool startRoom,
        GameObject entrancePoint,
        GameObject startHallway
        )
    {
        RoomData = roomData;
        TileDimension = tileDimension;
        StartRoom = startRoom;
        RoomDimensions = roomDimension;
        Models = models;
        EntrancePoint = entrancePoint;
        StartHallway = startHallway;
    }

    //***************************************************************************************************

    public void Generate ()
    {
        CreateRoomHolder();
        PlaceAllGeometry();
        CreatePivot();
        PlacePivot();
        ParentToPivot();
        PlacePivotAtEntrancePoint();
        ParentStartHallway();
        SetOldRoom();
    }

    //***************************************************************************************************

    void ParentStartHallway()
    {
        if (StartRoom == false)
        {
            StartHallway.transform.parent = Pivot.transform;
        }
    }

    //***************************************************************************************************

    void SetOldRoom()
    {
        Door_Hallway[] Hallways = FindObjectsOfType<Door_Hallway>();
        foreach (Door_Hallway hallway in Hallways)
        {
            hallway.OldRoom = Pivot;
        }
    }

    //***************************************************************************************************

    void CreateRoomHolder ()
    {
        Holder = new GameObject("Room");
        Holder.transform.position = Vector3.zero;
        Holder.transform.eulerAngles = Vector3.zero;
    }

    //***************************************************************************************************

    void CreatePivot()
    {
        Pivot = new GameObject("RoomPivot");
        Pivot.transform.parent = Holder.transform.parent;
        Pivot.transform.localPosition = Vector3.zero;
        Pivot.transform.localEulerAngles = Vector3.zero;
    }

    //***************************************************************************************************

    void PlacePivot()
    {
        if (StartRoom == false)
        {
            RoomPosition Pos = FindDoor();
            Pivot.transform.localPosition = Pos.Position * TileDimension;
            Pivot.transform.eulerAngles = new Vector3(0, Pos.Rotation, 0);
        }
    }

    //***************************************************************************************************

    void ParentToPivot()
    {
        Pivot.transform.parent = null;
        Holder.transform.parent = Pivot.transform;
    }

    //***************************************************************************************************

    void PlacePivotAtEntrancePoint()
    {
        if (StartRoom == false)
        {
            Pivot.transform.position = EntrancePoint.transform.position;
            Pivot.transform.rotation = EntrancePoint.transform.rotation;
        }
        else
        {
            Pivot.transform.position = Vector3.zero;
            float x = -1 * ( (RoomDimensions.x / 2 * TileDimension) - (TileDimension/2) );
            float z = -1 * ( (RoomDimensions.z / 2 * TileDimension) - (TileDimension/2) );
            Holder.transform.localPosition = new Vector3(x, Holder.transform.localPosition.y, z);
        }
    }

    //***************************************************************************************************

    RoomPosition FindDoor()
    {
        RoomPosition Pos = new RoomPosition();

        foreach (RoomLevel level in RoomData)
        {
            foreach (RoomPosition pos in level.RoomPositions)
            {
                if (pos.Value == 3) { Pos = pos; }
            }
        }

        return Pos;

    }

    //***************************************************************************************************

    void PlaceAllGeometry()
    {
        PlaceGeometry(Models.Floors, 1);
        PlaceGeometry(Models.Doors, 2);
        PlaceGeometry(Models.Floors, 3);
        PlaceGeometry(Models.Stairs, 5);
        //print("Models Placed");
    }


    //***************************************************************************************************
    /*
    void CreateWalls()
    {

    }
    */
    //***************************************************************************************************

    void PlaceGeometry (List<GameObject> Objects, int Value)
    {
        

        foreach (RoomLevel level in RoomData) {
            foreach (RoomPosition Pos in level.RoomPositions) {
                if (Pos.Value == Value) {
                    int R = Random.Range(0, Objects.Count);
                    GameObject obj = Instantiate(Objects[R]) as GameObject;
                    obj.transform.parent = Holder.transform;
                    obj.transform.localPosition = Pos.Position * TileDimension;
                    obj.transform.eulerAngles = new Vector3(0, Pos.Rotation, 0);
                }
            }
        }

    }

    //***************************************************************************************************
}

