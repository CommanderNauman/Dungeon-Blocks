using UnityEngine;

public class Door_Hallway : MonoBehaviour {

    public GameObject Hallway;
    public GameObject OldRoom;
    public Vector3 HallwayPos;
    
    //***************************************************************************************************

    public void Use()
    {
        GameObject hall = CreateHallway();
        SetOldRoom(hall);
        Destroy(this);
    }

    //***************************************************************************************************

    GameObject CreateHallway()
    {
        GameObject hall = Instantiate(Hallway);
        hall.transform.parent = gameObject.transform.parent.transform;
        hall.transform.localPosition = HallwayPos;
        hall.transform.localEulerAngles = Vector3.zero;
        hall.transform.parent = null;
        return hall;
    }

    //***************************************************************************************************

    void SetOldRoom(GameObject hall)
    {
        foreach (Transform child in hall.transform)
        {
            if (child.GetComponent<Room_Trigger>() != null)
            {
                child.GetComponent<Room_Trigger>().OldRoom = OldRoom;
            }
        }
    }

    //***************************************************************************************************
}
