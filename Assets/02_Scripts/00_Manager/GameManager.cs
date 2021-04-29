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
        if (Input.GetKeyDown(KeyCode.E))
            AddItemToDraw("pomme");

    }
    #endregion

    #region Call UI


    #endregion

    public void AddItemToDraw(string itemName) // ajouter le script de l'objet dragable
    {
        string varName = "Objet";
        for (int i = 0; i < 3; i++)
        {
            string lookAtThisVar = varName + i.ToString();
            if (ChangeVariable(lookAtThisVar, itemName))
                break;
        }
    }

    private bool ChangeVariable(string varName, string itemName)
    {
        if (DialogueLua.GetVariable(varName).AsString == "")
        {
            DialogueLua.SetVariable(varName, itemName);
            return true;
        }
        return false;
    }

    public void test(string message)
    {
        print(message);
    }

    #region ChangeEnum

    #endregion

    #region Button Event
    #endregion

    #region Set Language

    #endregion

    #region GETTER & SETTER

    #endregion

    #region Lua region
    void OnEnable()
    {
        Lua.RegisterFunction("test", this, SymbolExtensions.GetMethodInfo(() => test(string.Empty)));
    }

    void OnDisable()
    {
        Lua.UnregisterFunction("test"); 
    }

    #endregion
}
