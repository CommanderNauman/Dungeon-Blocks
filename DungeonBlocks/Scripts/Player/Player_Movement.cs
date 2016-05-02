using UnityEngine;
using System.Collections;

public class Player_Movement : Movement_Base
{
    //***************************************************************************************************

    public static Player_Movement Instance;

    GameObject Cam;

    //***************************************************************************************************

    void Awake()
    {
        Instance = this;
    }

    //***************************************************************************************************

    public void AdditionalPlayerInitiations()
    {
        Cam = GameObject.Find("CamController");
        LookHolder.transform.parent = Cam.transform;
    }

    //***************************************************************************************************
    /*
    public override void UpdateLookHolder()
    {
        LookHolder.transform.position = Cam.transform.position;
    }
    */
    //***************************************************************************************************

    public void HolderRotationFunction()
    {
        //This is Perfect. No need to touch.

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float Angle = Mathf.Atan2(h, v) * Mathf.Rad2Deg; //converts point system in axis to degrees

        if ((h != 0 || v != 0))
        {
            LookHolder.transform.localRotation = Quaternion.Euler(0, Angle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, LookHolder.transform.rotation, 1);
        }
    }

    //***************************************************************************************************

    public override float InputSpeed()
    {
        //float v = Input.GetAxis("Vertical");
        //float h = Input.GetAxis("Horizontal");

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 AVP = new Vector2(h, v); //the point on the surface of circle via the axis
        Vector2 AVPClamp = Vector2.ClampMagnitude(AVP, 1.0f); // clamps to 1 so that its not greater than 1
        float VDis = Vector2.Distance(Vector2.zero, AVPClamp); //distance between origin and axis on controller
        return VDis;
    }

    //***************************************************************************************************

    void Start()
    {
        StandardInitiation();
        AdditionalPlayerInitiations();
    }

    //***************************************************************************************************

    // Update is called once per frame
    void Update()
    {
        if (Active == true)
        {
            StateSwitch(State);
            HolderRotationFunction();
        }
    }

    //***************************************************************************************************

    void LateUpdate()
    {
        UpdateLookHolder();
        UpdateModelPosition();
    }

    //***************************************************************************************************
}
