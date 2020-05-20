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

    #region Move
    public Rigidbody rigid;
    public Vector3 MoveDirection;
    public float MoveSpeed;
    #endregion

    #region Gun
    //public GameObject Bullet;
    public Transform Muzzle;
    public ParticleSystem MuzzleFlash;
    public Object BulletHitHole;
    public LayerMask BulletMask;
    //public float Bullet_Speed = 10.0f;
    public float ShootingRadius = 0.1f;
    public int maxAmmoCount = 180;
    public int curAmmo = 30;
    public bool isReload = false;
    public float ReloadTime = 2.0f;
    public bool isShot = false;
    public float ShotTerm = 0.2f;
    #endregion

    private UIManager uiManager;

    //--------------------------------------------------
    //Event called on health changed
    public void ChangeHealth(float Amount)
    {
        //Reduce health
        Health = Mathf.Clamp(Health + Amount, 0f, 100.0f);

        //Show blood vision and then hide
        if (Amount < 0)
        {
            BloodVision.SetActive(true);
            Invoke("HideBloodVision", 0.5f);
        }
        uiManager.ChangeHP(Health);

        //Should we die?
        if (Health <= 0)
        {
            //Exit from game, back to editor
            EditorApplication.isPlaying = false;
            return;
        }
    }
    public void AddAmmo(int _ammoCount)
    {
        maxAmmoCount += _ammoCount;
        uiManager.ChangeBulletCount(curAmmo, maxAmmoCount);
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
        //Bullet = Resources.Load("Bullet") as GameObject;
        Muzzle = Camera.main.transform.Find("Gun").Find("Muzzle");
        MuzzleFlash = Muzzle.GetChild(0).GetComponent<ParticleSystem>();
        BulletHitHole = Resources.Load("BulletHitHole");
        MoveDirection = new Vector3(0f, 0f, 0f);
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
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
        if (!isReload)
        {
            if (curAmmo > 0)
            {
                if (!isShot && Input.GetMouseButton(0))
                {
                    Debug.Log("Mouse Button Down");
                    isShot = true;
                    Invoke("FinishShot", ShotTerm);
                    curAmmo--;
                    uiManager.ChangeBulletCount(curAmmo, maxAmmoCount);
                    MuzzleFlash.Play();
                    RaycastHit hit;
                    //Vector3 camera = Camera.main.transform.localEulerAngles;
                    //Camera.main.transform.localEulerAngles = new Vector3(camera.x - 10.0f, camera.y, 0);
                    if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward + (Random.insideUnitSphere * ShootingRadius), out hit, 100.0f, BulletMask))
                    {
                        if (hit.collider.CompareTag("Enemy"))
                        {
                            hit.transform.SendMessage("ChangeHealth", -AttackDamage, SendMessageOptions.DontRequireReceiver);
                            //hit.collider.GetComponent<AI_Enemy>().ChangeHealth(-AttackDamage);
                            Debug.Log("hit Enemy");
                        }
                        else
                        {
                            GameObject hitHole = Instantiate(BulletHitHole, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal)) as GameObject;
                            Destroy(hitHole, 1.0f);
                        }
                    }
                }
            }
            else
            {
                //reload
                if (maxAmmoCount > 0)
                {
                    Reload();
                }
            }

            if(Input.GetKey(KeyCode.R))
            {
                Reload();
            }
        }

        MoveDirection = Vector3.zero;

        //Move
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

        //Run
        if(Input.GetKey(KeyCode.LeftShift))
        {
            MoveDirection *= 1.5f;
        }

        rigid.velocity = MoveDirection;
    }
    //--------------------------------------------------

    void FinishReload()
    {
        isReload = false;
    }

    void Reload()
    {
        isReload = true;
        Invoke("FinishReload", ReloadTime);
        maxAmmoCount -= 30 - curAmmo;
        curAmmo = 30;
        uiManager.Reload(ReloadTime, maxAmmoCount);
    }

    void FinishShot()
    {
        isShot = false;
    }
}
