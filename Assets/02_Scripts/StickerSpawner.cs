using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerSpawner : MonoBehaviour
{
    public int numberofspawn;
    public GameObject baseSticker;
    public float circleSize;
    public string[] namestoload;
    // Start is called before the first frame update
    void Start()
    {
        SpawnAllStickers();
    }

    public GameObject GetObjectbyName(string resourcename)
    {
        GameObject tempObj = Resources.Load("Stickers/sticker_"+ resourcename) as GameObject;

        return tempObj;
    }

    public void SpawnAllStickers()
    {
        for (int i = 0; i < namestoload.Length; i++)
        {
            GameObject newSticker = GameObject.Instantiate(GetObjectbyName(namestoload[i]));
            Vector2 circle = Random.insideUnitCircle*circleSize;
            Vector3 spawnPos = new Vector3(circle.x,1, circle.y);
            newSticker.transform.SetPositionAndRotation(spawnPos,newSticker.transform.rotation);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
