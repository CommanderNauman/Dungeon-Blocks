using UnityEngine;
using System.Collections.Generic;

class Room_Visualization
{
    int TileDimension = 10;

    List<RoomLevel> RoomData;
    GameObject Number;
    GameObject Holder;

    //***************************************************************************************************

    public Room_Visualization() { }
    public Room_Visualization(List<RoomLevel> roomData, GameObject number , int tileDimension)
    {
        RoomData = roomData; Number = number; TileDimension = tileDimension;
    }


    //***************************************************************************************************

    public void Visualize()
    {
        CreateHolder();
        CreateNumbers();
    }

    //***************************************************************************************************

    void CreateNumbers()
    {
        foreach(RoomLevel Level in RoomData)
        {
            foreach (RoomPosition pos in Level.RoomPositions)
            {
                GameObject number = GameObject.Instantiate(Number);
                number.transform.parent = Holder.transform;
                number.transform.localPosition = pos.Position * TileDimension;
                SetNumberText(number, pos.Value);
            }
        }
    }

    //***************************************************************************************************

    void CreateHolder()
    {
        Holder = new GameObject("Holder");
        ZeroOut(Holder);
    }

    //***************************************************************************************************

    void ZeroOut(GameObject obj)
    {
        obj.transform.position = Vector3.zero;
        obj.transform.eulerAngles = Vector3.zero;
    }

    //***************************************************************************************************

    void SetNumberText (GameObject number, int value  )
    {
        TextMesh text = number.GetComponent<TextMesh>();
        text.text = value.ToString();
    }

    //***************************************************************************************************
}

