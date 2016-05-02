using UnityEngine;
using System.Collections;

public class Door_LockTrigger : MonoBehaviour {

    public bool PermanentLock = true;
    public Door door;

    //***************************************************************************************************

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            door.Close();
            if (PermanentLock == true)
            {
                door.Locked = true;
                Destroy(gameObject);
            }
        }
    }

    //***************************************************************************************************
}
