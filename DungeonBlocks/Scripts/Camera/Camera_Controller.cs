using UnityEngine;
using System.Collections;

public class Camera_Controller : MonoBehaviour {

    //***************************************************************************************************

    public static Camera_Controller Instance;

    public float FreeCamSpeed = 0.8f;

    public float RotationSpeed_Default = 3.0f;
    public float WheelSpeed = 15.0f;

    public float MinDistance = -8f;
    public float MaxDistance = -20f;

    public float X;
    public float Z;

    float Xmin;
    float Xmax;
    float Zmin;
    float Zmax;

    public int State;
    public bool Follow_Target;
    public GameObject Target;

    public enum CamStates
    {
        Default = 0,
        FreeCam = 1
        
    }

    public CamStates CamState;

    GameObject Cam;
    GameObject Rotator;

    //***************************************************************************************************

    void Awake ()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
	}

    //***************************************************************************************************

    void Start ()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        Cam = GameObject.FindGameObjectWithTag("MainCamera");
        Rotator = gameObject.transform.Find("CamRotator").gameObject;
        SetCamDimensions(X, Z);
    }

    //***************************************************************************************************

    void SetCamDimensions(float X, float Z)
    {
        Xmin = 0;
        Xmax = X;
        Zmin = 0;
        Zmax = Z;
    }

    //***************************************************************************************************

    public void GetWorldDimensions (out float xmin, out float xmax, out float zmin, out float zmax)
    {
        xmin = Xmin; xmax = Xmax; zmin = Zmin; zmax = Zmax;
    }

    //***************************************************************************************************

    // Update is called once per frame
    void LateUpdate ()
    {
        StateSwitch();
    }

    //***************************************************************************************************
    /// <summary>
    /// Switches the Cams Mode
    /// </summary>
    /// <param name="newState"> 0 = Default. 1 = Free Cam. </param>


    public void SwitchCamState (int newState)
    {
        CamState = (CamStates)newState;
    }

    //***************************************************************************************************

    void StateSwitch ()
    {
        switch ((int)CamState)
        {
            case 0:
                MouseRotate();
                //JoystickRotate();
                FollowTarget();
                Zoom();
                break;
            case 1:
                MouseRotate();
                VerticalOrbit();
                //JoystickRotate();
                Move();
                Zoom();
                //BoundaryCheck();
                break;
            default:
                break;
        }
    }

    //***************************************************************************************************

    void FollowTarget ()
    {
        gameObject.transform.position = Target.transform.position;
    }

    //***************************************************************************************************

    public void ChangeTarget (GameObject newTarget)
    {
        Target = newTarget;
    }

    //***************************************************************************************************

    void MouseRotate ()
    {
        if (Input.GetButton("Fire2"))
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * RotationSpeed_Default, 0);   
        }
    }

    //***************************************************************************************************
    /*
    void JoystickRotate()
    {
        transform.Rotate(0, Input.GetAxis("RightstickHorizontal") * RotationSpeed_Default, 0);
    }
    */

    //***************************************************************************************************

    void VerticalOrbit()
    {
        if (Input.GetButton("Fire2"))
        {
            Rotator.transform.Rotate(Input.GetAxis("Mouse Y") * RotationSpeed_Default, 0, 0);
        }
    }

    //***************************************************************************************************

    void Zoom()
    {
        var z = Input.GetAxis("Mouse ScrollWheel");

        ZoomIn(z);
        ZoomOut(z);
    }

    //***************************************************************************************************

    void ZoomIn(float z)
    {
        Vector3 Vec = new Vector3(0, 0, z);

        if (z > 0 && Cam.transform.localPosition.z < MinDistance)
        {
            Cam.transform.Translate(Vec * WheelSpeed);

            if (Cam.transform.localPosition.z >= MinDistance)
            {
                Cam.transform.localPosition = new Vector3(Cam.transform.localPosition.x, Cam.transform.localPosition.y, MinDistance);
            }
        }
    }

    //***************************************************************************************************

    void ZoomOut(float z)
    {
        Vector3 Vec = new Vector3(0, 0, z);

        if (z < 0 && Cam.transform.localPosition.z > MaxDistance)
        {
            Cam.transform.Translate(Vec * WheelSpeed);

            if (Cam.transform.localPosition.z <= MaxDistance)
            {
                Cam.transform.localPosition = new Vector3(Cam.transform.localPosition.x, Cam.transform.localPosition.y, MaxDistance);
            }
        }
    }

    //***************************************************************************************************

    void Move()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("CamVert");
        var z = Input.GetAxis("Vertical");

        Vector3 Vec = new Vector3(x, y, z);

        transform.Translate(Vec * FreeCamSpeed);
    }

    //***************************************************************************************************

    void BoundaryCheck()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        if (transform.position.z > Zmax && z > 0)
        {
            transform.position = new Vector3(transform.position.x,
                                                transform.position.y,
                                                Zmax);
        }
        if (transform.position.z < Zmin && z < 0)
        {
            transform.position = new Vector3(transform.position.x,
                                                transform.position.y,
                                                Zmin);
        }
        if (transform.position.x > Xmax && x > 0)
        {
            transform.position = new Vector3(Xmax,
                                                transform.position.y,
                                                transform.position.z);
        }
        if (transform.position.x < Xmin && x < 0)
        {
            transform.position = new Vector3(Xmin,
                                                transform.position.y,
                                                transform.position.z);
        }

    }

    //***************************************************************************************************
}
