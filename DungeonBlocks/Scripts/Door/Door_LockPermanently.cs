using UnityEngine;
using System.Collections;

public class Door_LockPermanently : MonoBehaviour {

    //***************************************************************************************************

    public bool PermanentLock = true;
    public bool DestroyRoomOnPermaLock = true;
    public Door door;
    public Room_Generator OldGenerator;

    //***************************************************************************************************

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            door.Close();
            if (PermanentLock == true)
            {
                door.Locked = true;
                if (DestroyRoomOnPermaLock == true) { DestroyRoom(); }
                //print("Locked!");
                Destroy(this.gameObject);
            }
        }
    }

    //***************************************************************************************************

    void DestroyRoom()
    {
        foreach (Transform Child in transform.parent.transform)
        {
            if (Child.GetComponent<Room_Generator>() != null)
            {
                transform.parent.transform.parent = Child.GetComponent<Room_Generator>().FloorClone.transform;
            }
        }
        OldGenerator.DestroyRoom();
    }

    //***************************************************************************************************
}
