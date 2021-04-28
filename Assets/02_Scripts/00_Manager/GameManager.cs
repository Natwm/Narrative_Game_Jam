using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;

public class GameManager : MonoBehaviour
{

    #region Enum
    public enum GameStatus
    {

    }
    #endregion

    #region PARAM
    public static GameManager instance;

    #endregion

    #region AWAKE | START | UPDATE
    void Awake()
    {
        //print(DialogueManager.GetLocalizedText());
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : GameManager");
        instance = this;
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }
    #endregion

    #region Call UI


    #endregion

    #region ChangeEnum

    #endregion

    #region Button Event
    #endregion

    #region Set Language

    #endregion

    #region GETTER & SETTER
    
    #endregion

    #region Lua region

    #endregion
}
