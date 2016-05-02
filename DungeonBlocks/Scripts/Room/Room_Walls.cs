using UnityEngine;
using System.Collections.Generic;
    
class Room_Walls
{
    List<RoomLevel> RoomData;
    Vector3 RoomDimensions;
    GameObject Wall;
    GameObject RoomHolder;
    int TileDimension;

    public Room_Walls() { }
    public Room_Walls(List<RoomLevel> roomData, Vector3 roomDimensions, GameObject roomHolder, GameObject wall, int tileDimension)
    {
        RoomData = roomData;
        RoomDimensions = roomDimensions;
        RoomHolder = roomHolder;
        Wall = wall;
        TileDimension = tileDimension;
    }

    //***************************************************************************************************

    public void Generate()
    {
        
    }

    //***************************************************************************************************



    //***************************************************************************************************
}

