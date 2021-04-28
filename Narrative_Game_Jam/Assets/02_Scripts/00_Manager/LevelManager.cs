using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private int index;

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : GameManager");
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckSpeech(Drag_and_Drop elt)
    {
        if (elt.Index == index)
        {
            Debug.Log("good");
            elt.Status = true;
            elt.gameObject.GetComponent<Image>().color = Color.green;
            index++;
            GameManager.instance.ShowText(index, elt.SpeechText.text);
        }
        else
        {
            Debug.Log("not good");
            elt.gameObject.GetComponent<Image>().color = Color.red;
            Player.instance.LooseLocution();

            GameManager.instance.UpdateLocution(Player.instance.Locution);
        }
    }
}
