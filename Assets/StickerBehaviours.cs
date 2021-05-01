using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    public bool hasbeengrabbed;
    bool hasfaded;
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
        if(isMoving && Input.GetKey(KeyCode.A)){
            transform.DORotate((transform.rotation.eulerAngles + new Vector3(0, -rotationSpeed, 0)), 0.01f);
        }

        if (isMoving && Input.GetKey(KeyCode.E))
        {
            transform.DORotate((transform.rotation.eulerAngles + new Vector3(0, rotationSpeed, 0)), 0.01f);
        }

        if (IsMoving)
        {
            transform.DOScale(0.4f, 0);
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
                GameObject spawn = Instantiate(SplashSpawn, Vector3.zero, SplashSpawn.transform.rotation);
                spawn.transform.parent = transform;
                spawn.transform.localPosition = new Vector3(0,0,0.1f);
                spawn.transform.DOScale(Random.Range(0.2f, splashScaleMax), spawnSpeed);
                spawn.transform.DORotate(new Vector3(-90, 0, Random.Range(0, 360)), 0.001f);
                spawn.GetComponent<SpriteRenderer>().DOFade(0, fadetimer);
                timer.ResetPlay();
                GetComponent<Rigidbody>().isKinematic = true;
                GetComponent<BoxCollider>().isTrigger = true;
                transform.DOScale(0.2f, 0.05f);
                GetComponent<SpriteRenderer>().sortingOrder = move.layerindex;
                move.layerindex++;
                isSpawned = true;
                
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
