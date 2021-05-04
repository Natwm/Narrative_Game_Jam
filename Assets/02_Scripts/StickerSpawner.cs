﻿using System.Collections;
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
    
    // Start is called before the first frame update
    void Start()
    {
        instance = FindObjectOfType<GameManager>();
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

    public void SpawnItem(string[] arrayToPickFrom)
    {
        collider_feuille.SetActive(false);
        collider_spawnfeuille.SetActive(true);
        for (int i = 0; i < arrayToPickFrom.Length; i++)
        {
            GameObject newSticker = Instantiate(GetObjectbyName(arrayToPickFrom[i]));
            newSticker.GetComponent<StickerBehaviours>().stickerName = namestoload[i];
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
            SpawnItem(namestoload);
        }
    }
}
