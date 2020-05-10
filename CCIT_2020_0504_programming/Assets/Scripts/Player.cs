using UnityEngine;
using UnityEditor;
using System.Collections;

public class Player : MonoBehaviour
{
    //Health of player character
    public float Health = 100f;

    //Attack range
    public float AttackRange = 2f;

    //Amount of damage to inflict on enemy
    public float AttackDamage = 10f;

    //Reference to blood-vision
    public GameObject BloodVision = null;

    //List of enemies in level
    private AI_Enemy[] Enemies = null;

    #region
    public Rigidbody rigid;
    public Vector3 MoveDirection;
    public float MoveSpeed;
    #endregion

    #region 
    public GameObject Bullet;
    public Transform Muzzle;
    public ParticleSystem MuzzleFlash;
    public float Bullet_Speed = 10.0f;
    public float ShootingRadius = 0.1f;
    #endregion


    //--------------------------------------------------
    //Event called on health changed
    public void ChangeHealth(float Amount)
    {
        //Reduce health
        Health += Amount;

        //Show blood vision and then hide
        BloodVision.SetActive(true);
        Invoke("HideBloodVision", 0.5f);

        //Should we die?
        if (Health <= 0)
        {
            //Exit from game, back to editor
            EditorApplication.isPlaying = false;
            return;
        }
    }
    //--------------------------------------------------
    //Hides blood
    void HideBloodVision()
    {
        BloodVision.SetActive(false);
    }
    //--------------------------------------------------
    //Called at Object Create
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        Bullet = Resources.Load("Bullet") as GameObject;
        Muzzle = Camera.main.transform.Find("Gun").Find("Muzzle");
        MuzzleFlash = Muzzle.GetChild(0).GetComponent<ParticleSystem>();
        MoveDirection = new Vector3(0f, 0f, 0f);
    }
    //--------------------------------------------------
    //Called at level start-up
    void Start()
    {
        //Get all enemies in scene
        Enemies = Object.FindObjectsOfType<AI_Enemy>();
    }
    //--------------------------------------------------
    //Called every frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Button Down");
            //Bullet Direction = Camera.main.transform.forward + (Random.insideUnitSphere * ShootingRadius)

            GameObject obj = GameObject.Instantiate(Bullet, Muzzle.position, Quaternion.identity);
            MuzzleFlash.Play();
            obj.GetComponent<Rigidbody>().velocity = (Camera.main.transform.forward + (Random.insideUnitSphere * ShootingRadius)) * Bullet_Speed;
        }
        //Should we attack?
        //if (Input.GetKeyDown(KeyCode.RightControl))
        //{
        //    //Damage enemies in range
        //    foreach (AI_Enemy En in Enemies)
        //    {
        //        //If within attack distance then attack
        //        if (Vector3.Distance(transform.position, En.transform.position) < AttackRange)
        //        {
        //            En.SendMessage("ChangeHealth", AttackDamage, SendMessageOptions.DontRequireReceiver);
        //        }
        //    }
        //}
        MoveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            MoveDirection = transform.forward * MoveSpeed;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            MoveDirection = transform.forward * -MoveSpeed;
        }
        if(Input.GetKey(KeyCode.A))
        {
            MoveDirection += transform.right * -MoveSpeed;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            MoveDirection += transform.right * MoveSpeed;
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            MoveDirection *= 1.5f;
        }
        rigid.velocity = MoveDirection;
    }
    //--------------------------------------------------
}
