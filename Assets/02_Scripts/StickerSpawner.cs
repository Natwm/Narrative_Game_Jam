using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class StickerSpawner : MonoBehaviour
{
    public int numberofspawn;
    public GameObject baseSticker;
    public float circleSize;
    public string[] namestoload;
    public float safeRange;
    public GameObject collider_feuille;
    public GameObject collider_spawnfeuille;
    public GameObject[] collidersDecors;
    GameManager instance;
    SceneParser sceneParser;
    // Start is called before the first frame update
    void Start()
    {
        instance = FindObjectOfType<GameManager>();
        sceneParser = GetComponent<SceneParser>();
    }

    public GameObject GetObjectbyName(string resourcename)
    {
        GameObject tempObj = Resources.Load("Stickers/sticker_"+ resourcename) as GameObject;

        return tempObj;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, circleSize);
        Gizmos.DrawWireSphere(transform.position, safeRange);
    }

    public void SpawnAllStickers(string[] arraytospawn)
    {
        for (int i = 0; i < namestoload.Length; i++)
        {
            GameObject newSticker = GameObject.Instantiate(GetObjectbyName(arraytospawn[i]));
            Vector2 circle = Random.insideUnitCircle*circleSize;
            Vector3 spawnPos = new Vector3(circle.x,1, circle.y);
            newSticker.GetComponent<StickerBehaviours>().stickerName = namestoload[i];
            newSticker.transform.SetPositionAndRotation(spawnPos,newSticker.transform.rotation);
            
        }
    }

    // Get Points
    // Check on point if there is the collider
    // if collider > Go back to Get Points
    // eles > Go to Spawn Sticker
    // Spawn Sticker.

    public Vector3 GetPointInCircle()
    {
        Vector2 newRandInCircle = Random.insideUnitCircle * circleSize;
        return new Vector3(newRandInCircle.x, 1, newRandInCircle.y);
    }

    public bool IsPointValid(Vector3 pointToCheck)
    {
        Vector3 safeV = new Vector3(safeRange, safeRange, safeRange);
        Collider[] ColliderInRange = Physics.OverlapSphere(pointToCheck, safeRange);
        foreach (Collider item in ColliderInRange)
        {
            Debug.Log(item.gameObject.name);
            if (item.tag == "NoSpawn" || item.isTrigger == true)
            {
                return false;
            }
        }

        return true;
    }

    public Vector3 LoopPoints()
    {
        Vector3 testVector = GetPointInCircle();
        if (IsPointValid(testVector))
        {
            return testVector;
        }
        else
        {
            return LoopPoints();
        }
    }


    //TO DO
    //-Add localisation and object mention
    public void SpawnItem(SceneItem[] arrayToPickFrom)
    {
        collider_feuille.SetActive(false);
        collider_spawnfeuille.SetActive(true);
        for (int i = 1; i < arrayToPickFrom.Length; i++)
        {
            GameObject newSticker;
            if (arrayToPickFrom[i].name != "")
            {
                int iteration = Random.Range(1, arrayToPickFrom[i].iterationNumber+1);
                for (int n = 0; n < iteration; n++)
                {
                    newSticker = Instantiate(GetObjectbyName(arrayToPickFrom[i].name));
                    newSticker.GetComponent<StickerBehaviours>().stickerName = arrayToPickFrom[i].name;
                    if (instance.currentLanguage == GameManager.Language.Francais)
                    {
                        newSticker.GetComponent<StickerBehaviours>().RotateLeft = KeyCode.A;
                        newSticker.GetComponent<StickerBehaviours>().RotateRight = KeyCode.E;
                    }
                    else
                    {
                        newSticker.GetComponent<StickerBehaviours>().RotateLeft = KeyCode.Q;
                        newSticker.GetComponent<StickerBehaviours>().RotateRight = KeyCode.E;
                    }
                    newSticker.transform.SetPositionAndRotation(LoopPoints(), newSticker.transform.rotation);

                }
                
            }
            
            
        }
        collider_feuille.SetActive(true);
        collider_spawnfeuille.SetActive(false);
        foreach (GameObject item in collidersDecors)
        {
            item.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SpawnItem(sceneParser.ParseScene(instance.CurrentScene));
        }
    }

    // To Do
    // Index Scene
    // CSV by scene
    // Mandatory Names - Optional by Scene - Max Doubles.
    // Get non scene Related Array
    //DISTRIBUTION?
    // 
    // Parse Csv
    // Get Mandatory Names
    // 
    // Read Array
    // GENERATION
    // FIRST>All OBLIGATORY ITEMS
    // SECOND> SECONDARY ITEMS > Get Max Iteration Number > Rand Iteration
    // LAST> FREE ITEMS
}
