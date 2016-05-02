using UnityEngine;
using System.Collections;

public class Player_Raycast : MonoBehaviour {

    //***************************************************************************************************

    // Update is called once per frame
    void Update ()
    {
        UseRaycast();
    }

    //***************************************************************************************************

    public void UseRaycast()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit = new RaycastHit();

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, forward * 4, Color.red);

        if (Physics.Raycast(transform.position, forward, out hit, 4f))
        {
            //print("Hitting! " + hit.ToString());


            if (hit.collider.gameObject.tag == "Npc" ||
                hit.collider.gameObject.tag == "Interactive" ||
                hit.collider.gameObject.tag == "Door" ||
                hit.collider.gameObject.tag == "Resource" ||
                hit.collider.gameObject.tag == "Item" ||
                hit.collider.gameObject.tag == "Player"
                )
            {
                //print("Interactive!");

                if (//Input.GetButtonDown("Button_X") ||
                    Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.SendMessage("Use");
                    //print("Sending use message!");
                }

            }

        }
    }

    //***************************************************************************************************
}
