using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using PixelCrushers.DialogueSystem;
using FMODUnity;

public class MainMenu : MonoBehaviour
{
    public Image CreditImage;
    public Image ParentImage;
    public Image TitleImage;

    public Button[] fadeUi;

    public Transform startPoint;
    public Transform endPoint;

    public Sprite[] soundsprite;
    bool soundState;
    public Text startText;
    public Text creditText;
    public Text exitText;

    public bool eventStop;

    [FMODUnity.EventRef]
    public string MusicMenu;

   public FMOD.Studio.EventInstance EventMenu;

    public Text PressStart;

    public void PetiteTransitionPasPiquéeDesHannetons()
    {
        if (!eventStop)
        {
        EventMenu = FMODUnity.RuntimeManager.CreateInstance(MusicMenu); 
        ParentImage.rectTransform.DOMove(endPoint.position,5f);
        ParentImage.DOFade(255,3000);
        PressStart.DOFade(0, 1f);
        foreach (Button item in fadeUi)
        {
            item.image.DOFade(255,3000);
            Text tempText;
            item.transform.GetChild(0).TryGetComponent<Text>(out tempText);
            if (tempText != null)
            {
               tempText.DOFade(255, 3000);
            }
        }
            eventStop = true;
        Invoke("LaunchTitle", 1);

        }
    }

    public void TerminateSound()
    {
        Debug.Log("OkBoomer");
        EventMenu.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void LaunchTitle()
    {
        EventMenu.start();
        TitleImage.DOFade(255, 2000);
    }

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
            GameManager.instance.currentLanguage = GameManager.Language.Francais;
            startText.text = "Jouer";
            creditText.text = "Crédits";
            exitText.text = "Quitter";
        }
        else
        {
            DialogueManager.instance.SetLanguage("UK");
            GameManager.instance.currentLanguage = GameManager.Language.Anglais;
            startText.text = "Play";
            creditText.text = "Credits";
            exitText.text = "Exit";
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

    public float basevolume;
    // Start is called before the first frame update
    void Start()
    {
        Master = FMODUnity.RuntimeManager.GetBus("bus:/");
        ParentImage.rectTransform.SetPositionAndRotation(startPoint.transform.position, ParentImage.rectTransform.rotation);
        Master.getVolume(out basevolume);
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            PetiteTransitionPasPiquéeDesHannetons();
        }
        
    }
}
