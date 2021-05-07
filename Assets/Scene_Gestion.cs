using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;
using DG.Tweening;
using FMODUnity;


public class Scene_Gestion : MonoBehaviour
{
    public Image BlackFade;
    public Color Dotween;
    public Color CDELAMERDE;

    [FMODUnity.EventRef]
     public string MusicAmbiant;

    FMOD.Studio.EventInstance AmbiantEvent;
    MainMenu mainMenuScript;

    public void InitializeScene()
    {
        
        GameManager.instance.CurrentScene ++;
        //FindObjectOfType<StickerSpawner>().JESUISUNSCHLAGUEMAISJESPAWNDESOBJETS();
       
    }

    public void Start()
    {
        mainMenuScript = FindObjectOfType<MainMenu>();
    }

    //LANCER LE FADE TO BLACK
    public void SceneTransition()
    {       
        BlackFade.gameObject.SetActive(true);
        BlackFade.DOColor(CDELAMERDE,2);
        Invoke("LoadNewScene", 2);
   
    }
    
    

    //CHARGEMENT DE LA SCENE SUIVANTE
    public void LoadNewScene()
    {
        
        ChangeScene();
        SceneManager.LoadSceneAsync(GameManager.instance.CurrentScene);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (GameManager.instance.CurrentScene == 1)
        {
            LaunchMusic();
        }
        BlackFade.DOColor(Dotween, 2);
        Invoke("DisableFade",2);   
    }

    void DisableFade()
    {
        BlackFade.gameObject.SetActive(false);
    }

    public void ChangeScene()
    {
        int newScene = GameManager.instance.CurrentScene++;
        SceneManager.LoadScene(newScene);
    }

    public void LaunchMusic()
    {
        GameManager.instance.soundEvent.start();
        AmbiantEvent = FMODUnity.RuntimeManager.CreateInstance(MusicAmbiant);
        AmbiantEvent.start();
    }

    void OnEnable()
    {
        Lua.RegisterFunction("GoToNextScene", this, SymbolExtensions.GetMethodInfo(() => SceneTransition()));

    }

    void OnDisable()
    {
        //Lua.UnregisterFunction("GoToNextScene");
    }
}
