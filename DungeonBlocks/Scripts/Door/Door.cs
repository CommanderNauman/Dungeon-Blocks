using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public bool Toggle = false;
    public bool Locked = false;

    public float Speed = 5f;

    public Vector3 OpenPosition;
    public Vector3 ClosePosition;

    //***************************************************************************************************

    public void Use()
    {
        ToggleDoor();
        //print("here");
    }

    //***************************************************************************************************

    public void ToggleDoor ()
    {
        Toggle = !Toggle;
        if (Toggle == true && Locked == false) { Open(); }
        else if (Toggle == false) { Close(); }
    }

    //***************************************************************************************************

    public void Open ()
    {
        StopAllCoroutines();
        Toggle = true;
        StartCoroutine(Move(OpenPosition));
    }

    //***************************************************************************************************

    public void Close()
    {
        StopAllCoroutines();
        Toggle = false;
        StartCoroutine(Move(ClosePosition));
    }

    //***************************************************************************************************

    IEnumerator Move( Vector3 S )
    {
        //print("here2");
        while (Vector3.Distance(transform.localPosition, S) >= Mathf.Epsilon) // Object.transform.position != ScreenPos.transform.position)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, S, Speed * Time.deltaTime);
            yield return null;
        }
    }

    //***************************************************************************************************
}
