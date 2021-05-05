using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneParser : MonoBehaviour
{
    GameManager instance;
    
    [SerializeField]
    public SceneItem[] allScenesItems;
    // Start is called before the first frame update
    void Start()
    {
        instance = FindObjectOfType<GameManager>();
    
    }

    public SceneItem[] ParseScene(int scene)
    {

        TextAsset sceneData = Resources.Load<TextAsset>("SceneData/scene_" + scene) as TextAsset;
        //Line Split
        string[] Lines = sceneData.text.Split(new char[] { '\n' });
        SceneItem[] returnItem = new SceneItem[Lines.Length];
        for (int i = 1; i < Lines.Length; i++)
        {
            string[] Row = Lines[i].Split(new char[] { ',' });
            if (Row[0] != "")
            {
                SceneItem newItem = new SceneItem(Row[0], Row[1], Row[2]);
                returnItem[i] = newItem;
            }
            
        }
        allScenesItems = returnItem;
        return returnItem;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.M))
        {
            allScenesItems = ParseScene(instance.CurrentScene);
        }
    }
}
