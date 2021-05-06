using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using PixelCrushers.DialogueSystem;

public class MainMenu : MonoBehaviour
{
    public Image CreditImage;
    public Sprite[] soundsprite;
    bool soundState;
    


    //Launch Scene
    public void LaunchGame()
    {
        FindObjectOfType<Scene_Gestion>().SceneTransition();
    }

    //ShowCredit
    public void ShowCredits()
    {
        Debug.Log("Show Credits");

        CreditImage.gameObject.SetActive(true);
            CreditImage.DOFade(255, 300);
        CreditImage.transform.DOScale(2.2f, 0.5f);
        
    }

    public void HideCredit()
    {

        CreditImage.CrossFadeAlpha(0, 0.2f,true);
        CreditImage.transform.DOScale(1.6f, 0.5f);
        CreditImage.gameObject.SetActive(false);
    }

    public void MuteSound()
    {
      
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void SetLanguageSettings(string language)
    {
        Debug.Log("Change Language");
        if (language == "francais")
        {
            DialogueManager.instance.SetLanguage("FR");
        }
        else
        {
            DialogueManager.instance.SetLanguage("UK");
        }
    }

    public void ToggleSound(Image soundbutton)
    {
        
        if (soundState == true)
        {
            Debug.Log("Toggle Sound On");
            soundbutton.sprite = soundsprite[1];
            soundState = false;
            Master.setPaused(false);
        }
        else
        {
            Debug.Log("Toggle Sound Off");
            soundbutton.sprite = soundsprite[0];
            soundState = true;
            Master.setPaused(true);
        }
    }

    public FMOD.Studio.Bus Master;
    

    // Start is called before the first frame update
    void Start()
    {
        Master = FMODUnity.RuntimeManager.GetBus("bus:/"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
