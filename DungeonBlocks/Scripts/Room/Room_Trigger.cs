using UnityEngine;

public class Room_Trigger : MonoBehaviour {

    public GameObject room;
    public GameObject EntrancePoint;
    public GameObject OldRoom;

    //***************************************************************************************************

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject newRoom = Instantiate(room);
            Room roomscript = newRoom.GetComponent<Room>();
            roomscript.EntrancePoint = EntrancePoint;
            roomscript.StartHallway = transform.parent.gameObject;
            Destroy(OldRoom);
            Destroy(gameObject);
        }
    }

    //***************************************************************************************************
}
