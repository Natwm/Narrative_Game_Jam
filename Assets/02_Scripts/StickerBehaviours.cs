using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using FMOD;
using FMODUnity;
using PixelCrushers.DialogueSystem;

public class StickerBehaviours : MonoBehaviour
{

    private GameObject SplashScreen;
    public GameObject SplashSpawn;
    public float spawnSpeed = 1f;
    public Mouse_Movement move;
    Timer timer;
    [SerializeField] private float rotationSpeed = 0.5f;
    [SerializeField] private float splashScaleMax = 0.5f;
    
    private bool isMoving = false;
    public bool isSpawned;
    public float fadetimer;
    public string stickerName;
    public bool hasbeengrabbed;
    bool hasfaded;

   public KeyCode RotateLeft;
   public KeyCode RotateRight;
    //Suce
    public bool IsMoving { get => isMoving; set => isMoving = value; }

   
    // Start is called before the first frame update
    void Start()
    {
        SplashScreen = transform.GetChild(0).gameObject;
        timer = new Timer(fadetimer);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y != 0 && isMoving)
        {
            transform.DORotate((transform.eulerAngles + new Vector3(0, rotationSpeed * 10 * Input.mouseScrollDelta.y, 0)), 0.01f);
        }
        if (tag != "Maurice")
        {
            if (isMoving && Input.GetKey(RotateLeft))
            {
                transform.DORotate((transform.rotation.eulerAngles + new Vector3(0, -rotationSpeed, 0)), 0.01f);
            }

            if (isMoving && Input.GetKey(RotateRight))
            {
                transform.DORotate((transform.rotation.eulerAngles + new Vector3(0, rotationSpeed, 0)), 0.01f);
            }
        }
        if (IsMoving)
        {
            transform.DOScale(1.2f, 0);
        }
        if (timer.IsFinished())
        {
            if (!hasfaded)
            {
            Destroy(transform.GetChild(1).gameObject);
                hasfaded = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Glue") && !isMoving && hasbeengrabbed)
        {
            if (!isSpawned)
            {
                if (tag != "Maurice")
                {
                    GameObject spawn = Instantiate(SplashSpawn, Vector3.zero, SplashSpawn.transform.rotation);
                    spawn.transform.parent = transform;
                    spawn.transform.localPosition = new Vector3(0, 0, 0.1f);
                    spawn.transform.DOScale(Random.Range(0.2f, splashScaleMax), spawnSpeed);
                    spawn.transform.DORotate(new Vector3(-90, 0, Random.Range(0, 360)), 0.001f);
                    spawn.GetComponent<SpriteRenderer>().DOFade(0, fadetimer);
                    timer.ResetPlay();
                    GetComponent<Rigidbody>().isKinematic = true;
                    GetComponent<BoxCollider>().isTrigger = true;
                    isSpawned = true;
                    transform.DOScale(1f, 0.05f);

                    DialogueLua.SetVariable("ObjectToUse", this.stickerName);

                    if (GameManager.instance.waitForPlayerInput)
                    {
                        FindObjectOfType<StandardUIContinueButtonFastForward>().OnFastForward();
                        GameManager.instance.waitForPlayerInput = false;
                    }
                }
                
                
                GetComponent<SpriteRenderer>().sortingOrder = move.layerindex;
                move.layerindex++;
                
                
            }
        }
    }

    

    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Glue") && !isMoving)
        {
            if (!isSpawned)
            {
                GameObject spawn = Instantiate(SplashSpawn, SplashScreen.transform.position, Quaternion.identity);
                spawn.transform.DOScale(.05f, 2f);
            }
        }
    }*/

}
