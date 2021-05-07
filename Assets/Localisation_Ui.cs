using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Localisation_Ui : MonoBehaviour
{
    public TMP_Text StartText;
    public TMP_Text SessionText;
    
    // Start is called before the first frame update
    void Start()
    {
         
        if (GameManager.instance.currentLanguage==GameManager.Language.Anglais)
        {
            StartText.text = "Start";
            SessionText.text = "Session " + SceneManager.GetActiveScene().buildIndex;
        }
        else
        {
            StartText.text = "Commencer";
            SessionText.text = "Séance " + SceneManager.GetActiveScene().buildIndex;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
