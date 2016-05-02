using UnityEngine;
using System.Collections.Generic;

class Room_Ladders
{
    private List<RoomLevel> RoomData;
    private List<GameObject> Ladders;
    private int TileDimension;

    //***************************************************************************************************

    public Room_Ladders (List<RoomLevel> roomData, List<GameObject> ladders, int tileDimension )
    {
        RoomData = roomData; Ladders = ladders; TileDimension = tileDimension;
    }

    //***************************************************************************************************

    public void Generate()
    {

    }

    //***************************************************************************************************
}

