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

    public Sprite[] mauriceSprites;
    

    public bool isMaurice;


    //Suce
    public bool IsMoving { get => isMoving; set => isMoving = value; }

    [FMODUnity.EventRef]
    public string dropMauriceSound;

    [FMODUnity.EventRef]
    public string dropSound;

    FMOD.Studio.EventInstance dropEvent;
    // Start is called before the first frame update

    
    void Start()
    {
        SplashScreen = transform.GetChild(0).gameObject;
        timer = new Timer(fadetimer);
        if (tag == "Maurice")
        {
            GameManager.instance.Maurice = this.gameObject;
            dropEvent = FMODUnity.RuntimeManager.CreateInstance(dropMauriceSound);
        }
        else
        {
            dropEvent = FMODUnity.RuntimeManager.CreateInstance(dropSound);
        }
    }

    public void ItsEnd()
    {
        if(tag == "Maurice")
            SwapSprite(mauriceSprites[1]);
    }

    public void SwapSprite(Sprite spriteToChange)
    {
        GetComponent<SpriteRenderer>().sprite = spriteToChange;
        transform.DOScale(0.7f, 0f);
        transform.DOScale(0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y != 0 && isMoving && tag != "Maurice")
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
            if (tag=="Maurice")
            {
                transform.DOScale(0.6f, 0);
            }
            else
            {
                transform.DOScale(1.2f, 0);
            }
            
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

                    GameManager.instance.amountOfSticker++;

                }             
                GetComponent<SpriteRenderer>().sortingOrder = move.layerindex;
                move.layerindex++;                
            }
        }
        else if (other.gameObject.CompareTag("End") && tag == "Maurice" && GameManager.instance.isEnd && !isMoving && hasbeengrabbed)
        {
            print("end");
            // lancer le niveau suivant
        }
    }

    public void PlayDrop()
    {
        dropEvent.start();
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
