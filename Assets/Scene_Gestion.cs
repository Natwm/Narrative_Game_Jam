using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PixelCrushers;
using UnityEngine.UI;
using DG.Tweening;

public class Scene_Gestion : MonoBehaviour
{
    public Image BlackFade;
    public Color Dotween;
    public Color CDELAMERDE;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void InitializeScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int sceneNumber = int.Parse(currentScene.name);
        GameManager.instance.CurrentScene = sceneNumber;
        FindObjectOfType<PixelCrushers.DialogueSystem.ConversationTrigger>().conversation = "Scène " + currentScene.name;
        FindObjectOfType<StickerSpawner>().JESUISUNSCHLAGUEMAISJESPAWNDESOBJETS();
    }

    public void SceneTransition()
    {       
        BlackFade.gameObject.SetActive(true);
        BlackFade.DOColor(CDELAMERDE,2);
        Invoke("", 2);
        SceneManager.LoadSceneAsync(GameManager.instance.CurrentScene);
    }
    

    private void OnLevelWasLoaded(int level)
    {
        Debug.Log(SceneManager.GetActiveScene());
    }

    public void ChangeScene()
    {
        int newScene = GameManager.instance.CurrentScene++;
        SceneManager.LoadScene(newScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
