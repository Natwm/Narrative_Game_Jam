using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerSpawner : MonoBehaviour
{
    public int numberofspawn;
    public GameObject baseSticker;
    public float circleSize;
    // Start is called before the first frame update
    void Start()
    {
        SpawnAllStickers();
    }

    public void SpawnAllStickers()
    {
        for (int i = 0; i < numberofspawn; i++)
        {
            GameObject newSticker = GameObject.Instantiate(baseSticker);
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
