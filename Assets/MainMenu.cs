using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    public Image CreditImage;
    public Sprite[] soundsprite;
    bool soundState;
    //Launch Scene
    public void LaunchGame()
    {
        SceneManager.LoadScene(1);
    }

    //ShowCredit
    public void ShowCredits()
    {
        Debug.Log("Ok Boomer");
        
            CreditImage.enabled = true;
            CreditImage.rectTransform.DOScale(1.2f,0.5f);
            CreditImage.DOFade(1, 0.5f);
        
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SetLanguageSettings(string language)
    {
        if (language == "francais")
        {

        }
        else
        {

        }
    }

    public void ToggleSound(Image soundbutton)
    {
        if (soundState == true)
        {
            soundbutton.sprite = soundsprite[1];
            soundState = false;
        }
        else
        {
            soundbutton.sprite = soundsprite[0];
            soundState = true;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
