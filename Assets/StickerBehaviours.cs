using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StickerBehaviours : MonoBehaviour
{
    private GameObject SplashScreen;
    public GameObject SplashSpawn;
    public float spawnSpeed = 1f;

    private bool isMoving = false;
    private bool isSpawned;

    public bool IsMoving { get => isMoving; set => isMoving = value; }

    // Start is called before the first frame update
    void Start()
    {
        SplashScreen = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving && Input.GetKeyDown(KeyCode.A)){
            transform.DORotate((transform.rotation.eulerAngles + new Vector3(0, 0.2f, 0)), 0.01f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Glue") && !isMoving)
        {
            if (!isSpawned)
            {
                GameObject spawn = Instantiate(SplashSpawn, SplashScreen.transform.position, Quaternion.identity);
                spawn.transform.DOScale(Random.RandomRange(0.023f,.04f), spawnSpeed);
                spawn.transform.DORotate(new Vector3(0, Random.RandomRange(0, 360), 0), 0.001f);
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
