using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour
{

    public static Player_Controller Instance;

    public bool Active = true;

    public GameObject StartPoint;

    Player_Movement PlayerMovement;
    Player_Shoot PlayerShoot;
    Player_Stamina PlayerStamina;

    //***************************************************************************************************

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        PlayerMovement = gameObject.GetComponent<Player_Movement>();
        PlayerShoot = gameObject.GetComponent<Player_Shoot>();
        PlayerStamina = gameObject.GetComponent<Player_Stamina>();
    }

    //***************************************************************************************************

    void Start ()
    {
        Activate();
    }

    //***************************************************************************************************

    public void Activate()
    {
        PlayerMovement.Active = true;
        Camera_Controller.Instance.Target = this.gameObject;
        Active = true;
    }

    //***************************************************************************************************

    public void Deactivate()
    {
        PlayerMovement.Active = false;
        Active = false;
    }

    //***************************************************************************************************

    public void BecomeExhausted ()
    {
        PlayerMovement.Speed_Current = PlayerMovement.Speed_Exhausted;
        PlayerShoot.ShootState = 1;
        StartCoroutine(WaitForRecovery());
    }

    //***************************************************************************************************

    IEnumerator WaitForRecovery ()
    {
        yield return new WaitForSeconds(PlayerStamina.WaitTimeTillQueStaminaGain);
        RecoverFromExhaustion();
    }

    //***************************************************************************************************

    void RecoverFromExhaustion ()
    {
        PlayerMovement.Speed_Current = PlayerMovement.Speed_Walking;
        PlayerShoot.ShootState = 0;
    }

    //***************************************************************************************************

    public void Die ()
    {
        transform.position = StartPoint.transform.position;
    }

    //***************************************************************************************************
}
