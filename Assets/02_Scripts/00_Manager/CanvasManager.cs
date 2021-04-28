using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PixelCrushers.DialogueSystem;

public class CanvasManager : MonoBehaviour
{
    #region Param
    public static CanvasManager instance;

    #endregion

    #region AWAKE | START | UPDATE
    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : CanvasManager");
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
    #endregion


    #region GETTER & SETTER

    #endregion

    #region Lua regio

    #endregion
}
