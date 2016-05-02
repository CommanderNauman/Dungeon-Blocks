using UnityEngine;
using System.Collections.Generic;

class Room_Stairs : MonoBehaviour
{
    private List<RoomLevel> RoomData;

    //***************************************************************************************************

    public Room_Stairs(List<RoomLevel> roomData)
    {
        RoomData = roomData;
    }

    //***************************************************************************************************

    public void Generate()
    {
        FindPotentialStairPoints();
    }

    //***************************************************************************************************

    void FindPotentialStairPoints()
    {
        for (int i = RoomData.Count-1; i >= 1; i--) {
            for (int j = 0; j < RoomData[i].RoomPositions.Count; j++) {
                if ((RoomData[i].RoomPositions[j].Value == 1 || 
                    RoomData[i].RoomPositions[j].Value == 2) &&
                    RoomData[i].RoomPositions[j].StairLock == false
                    )
                {
                    
                    AssessPoint(RoomData[i].RoomPositions[j], i);
                }
            }
        }
    }

    //***************************************************************************************************

    void AssessPoint(RoomPosition Pos, int Level)
    {
        List<Vector3> TopPoints = CreateVectors(Pos.Position, Pos.Position.y, 1);
        List<Vector3> BotPoints = CreateVectors(Pos.Position, Pos.Position.y-1, 1);
        List<Vector3> ForwardPoints = CreateVectors(Pos.Position, Pos.Position.y - 1, 2);

        //print(Pos.Position.y);

        for (int i = 0; i < 4; i++)
        {
            //print("Top: " + TopPoints[i] + " :: Bot: " + BotPoints[i]);

            bool TopClear = ClearTheTop(TopPoints[i], Level);
            RoomPosition BotPos = FindThePos(BotPoints[i], Level-1, 1);
            RoomPosition ForPos = FindThePos(ForwardPoints[i], Level - 1, 0);
            if ( TopClear == true && BotPos != null && Pos.StairLock == false)
            {
                //print("here!");
                BotPos.Value = 5;
                BotPos.Rotation = SetRotation(Pos.Position, BotPoints[i]);
                Pos.StairLock = true;
                StairLockThisLevel(Pos, Level);
                if (ForPos != null) { ForPos.Value = 1; }
            }
        }
    }

    //***************************************************************************************************

    void StairLockThisLevel( RoomPosition Pos , int Level)
    {
        List<Vector3> Points = CreateVectors(Pos.Position, Pos.Position.y, 1);
        foreach (Vector3 Point in Points)
        {
            RoomPosition newPos = RoomData[Level].RoomPositions.Find(obj => obj.Position == Point);
            if (newPos != null && ( (newPos.Value == 1) || (newPos.Value) == 2))
            {
                newPos.StairLock = true;
                //StairLockThisLevel(newPos, Level);
                print("locking");
            }
        }
    }

    //***************************************************************************************************

    int SetRotation (Vector3 Top , Vector3 Bot)
    {
        if (Top.x < Bot.x) {
            //print(1 + " :: T: " + Top + " :: B:" + Bot);
            return 90;
        }
        else if (Top.x > Bot.x) {
            //print(2 + " :: T: " + Top + " :: B:" + Bot);
            return -90;
        }
        else if (Top.z < Bot.z) {
            //print(3 + " :: T: " + Top + " :: B:" + Bot);
            return 0;
        }
        else //if (Top.z > Bot.z) 
        {
            //print(4 + " :: T: " + Top + " :: B:" + Bot);
            return 180;
        }
        //print("no!");
    }

    //***************************************************************************************************

    bool ClearTheTop (Vector3 Position, int Level)
    {
        RoomPosition Pos = RoomData[Level].RoomPositions.Find(obj => obj.Position == Position);
        if (Pos != null && Pos.Value == 0 )
        {
            //print("true");
            return true;
        }
        else { //print("false");
            return false; }
    }

    //***************************************************************************************************

    RoomPosition FindThePos(Vector3 Position , int Level , int value)
    {
        RoomPosition Pos = RoomData[Level].RoomPositions.Find(obj => obj.Position == Position);
        if (Pos != null && Pos.Value == value) { //print("RoomPos is: " + Pos.Position);
            return Pos; }
        else { //print("nope");
            return null; }
    }

    //***************************************************************************************************

    List<Vector3> CreateVectors (Vector3 Pos, float Y , int Places)
    {
        List<Vector3> Points = new List<Vector3>();

        Points.Add(new Vector3(Pos.x + Places, Y, Pos.z));
        Points.Add(new Vector3(Pos.x - Places, Y, Pos.z));
        Points.Add(new Vector3(Pos.x, Y, Pos.z + Places));
        Points.Add(new Vector3(Pos.x , Y, Pos.z - Places));

        /*
        foreach(Vector3 point in Points)
        {
            print(point);
        }
        */
        return Points;
    }

    //***************************************************************************************************
}

