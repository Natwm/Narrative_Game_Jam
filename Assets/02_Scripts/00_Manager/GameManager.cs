using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;
using FMODUnity;

public class GameManager : MonoBehaviour
{

    #region Enum
    public enum GameStatus
    {

    }

    public enum Language
    {
        Francais,Anglais
    }
    #endregion
    public Language currentLanguage;
    #region PARAM
    public static GameManager instance;
    public int CurrentScene;

    public bool isEnd = false;

    private GameObject maurice;

    [FMODUnity.EventRef]
    public string musique;

    public FMOD.Studio.EventInstance soundEvent;

    [SerializeField] private List<string> DrawingNeeded = new List<string>();

    public bool waitForPlayerInput = false;
    public int amountOfSticker = 0;
    public int maxAmountOfSticker = 0;

    public bool waitASecond = false;

    public GameObject Maurice { get => maurice; set => maurice = value; }

    #endregion

    #region AWAKE | START | UPDATE
    void Awake()
    {
        //print(DialogueManager.GetLocalizedText());
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        soundEvent = FMODUnity.RuntimeManager.CreateInstance(musique);
        //soundEvent.start();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            print(IsDrawValid());//AddItemToDraw("pomme");

    }
    #endregion

    #region Call UI


    #endregion

    public void SpawnAObject(string Object)
    {
        if (amountOfSticker >= maxAmountOfSticker)
        {
            FindObjectOfType<StickerSpawner>().SpawnAObject(Object);
        }
    }

    public void ItsEnd()
    {
        GameManager.instance.isEnd = true;

        Maurice.GetComponent<StickerBehaviours>().ItsEnd();
    }

    public void AddItemNeeded(string itemName) // ajouter le script de l'objet dragable
    {
        DrawingNeeded.Add(itemName);
    }

    public void ResetItemNeeded()
    {
        DrawingNeeded = new List<string>();
    }
    public void AddItemToDraw(string itemName) // ajouter le script de l'objet dragable
    {
        string varName = "Objet";
        for (int i = 0; i < DrawingNeeded.Count; i++)
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

    private bool IsDrawValid()
    {
        int isValid = 0;
        string varName = "Objet";
        foreach (string item in DrawingNeeded)
        {
            for (int x = 0; x < DrawingNeeded.Count; x++)
            {
                string lookAtThisVar = varName + x.ToString();
                if (IsValidInput(lookAtThisVar, item))
                {
                    isValid ++;
                }
            }
        }
        return isValid == DrawingNeeded.Count;
    }

    public bool IsValidInput(string draw, string wait)
    {
        return DialogueLua.GetVariable(draw).AsString == wait;
    }

    public void test(string message)
    {
        print(message);
    }

    public void DadMusique()
    {
        soundEvent.setParameterByName("MUSIC", 0.0f);
    }

    public void MotherMusique()
    {
        soundEvent.setParameterByName("MUSIC", 0.25f);
    }

    public void HinaMusique()
    {
        soundEvent.setParameterByName("MUSIC", .45f);
    }

    public void EthanMusique()
    {
        soundEvent.setParameterByName("MUSIC", .65f);
    }

    public void NothingMusique()
    {
        soundEvent.setParameterByName("MUSIC", 1f);
    }

    #region ChangeEnum

    #endregion

    #region Button Event
    #endregion

    #region Set Language

    #endregion

    #region GETTER & SETTER
    public void ChangeWaitForInput()
    {
        waitForPlayerInput = true;
    }

    public void WaitASecond()
    {
        waitASecond = true;
    }

    #endregion

    #region Lua region
    void OnEnable()
    {
        Lua.RegisterFunction("Dad", this, SymbolExtensions.GetMethodInfo(() => EthanMusique()));
        Lua.RegisterFunction("Mom", this, SymbolExtensions.GetMethodInfo(() => EthanMusique()));
        Lua.RegisterFunction("Hina", this, SymbolExtensions.GetMethodInfo(() => EthanMusique()));
        Lua.RegisterFunction("Ethan", this, SymbolExtensions.GetMethodInfo(() => EthanMusique()));
        Lua.RegisterFunction("Nothing", this, SymbolExtensions.GetMethodInfo(() => EthanMusique()));

        Lua.RegisterFunction("AddItem", this, SymbolExtensions.GetMethodInfo(() => AddItemNeeded(string.Empty)));
        Lua.RegisterFunction("ResetItem", this, SymbolExtensions.GetMethodInfo(() => ResetItemNeeded()));
        Lua.RegisterFunction("waitForPlayerInput", this, SymbolExtensions.GetMethodInfo(() => ChangeWaitForInput()));
        Lua.RegisterFunction("WaitForMaurice", this, SymbolExtensions.GetMethodInfo(() => ItsEnd()));
        Lua.RegisterFunction("SpawnAObject", this, SymbolExtensions.GetMethodInfo(() => SpawnAObject(string.Empty)));
        Lua.RegisterFunction("WaitASecond", this, SymbolExtensions.GetMethodInfo(() => WaitASecond()));
        //Lua.RegisterFunction("ShowInputPanel", this, SymbolExtensions.GetMethodInfo(() => CanvasManager.instance.ShowInputPanel()));
    }

    void OnDisable()
    {
        /*Debug.Log("blabla");
        Lua.UnregisterFunction("Dad");
        Lua.UnregisterFunction("Mom");
        Lua.UnregisterFunction("Hina");
        Lua.UnregisterFunction("Ethan");
        Lua.UnregisterFunction("Nothing");

        Lua.UnregisterFunction("AddItem");
        Lua.UnregisterFunction("ResetItem");
        Lua.UnregisterFunction("waitForPlayerInput");
        Lua.UnregisterFunction("WaitForMaurice");*/
        //Lua.UnregisterFunction("ShowInputPanel");
    }

    #endregion
}
