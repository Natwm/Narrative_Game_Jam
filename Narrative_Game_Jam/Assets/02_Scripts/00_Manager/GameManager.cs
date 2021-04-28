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
        Inspect,
        Present,
        Convince,
        DeEscalate,
        Speech,
        None
    }
    #endregion

    #region PARAM
    public static GameManager instance;

    private Player m_Player;

    [Header ("Game Status")]
    [SerializeField] private GameStatus m_GameState;
    [SerializeField] private bool isPresent;

    [Space]
    [Header("Response Length")]
    [SerializeField] private int responseLength = 100;

    [Space]
    [Header("De_Escalate Inventory")]
    [SerializeField] private List<Hint_SO> playerInventory;

    public List<CodexContent> a = new List<CodexContent>();

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
        m_Player = FindObjectOfType<Player>();
        DontDestroyOnLoad(this.gameObject);

        if (GameState == GameStatus.DeEscalate)
            CreatePlayerInventoryForDeEscalate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            NewCodexEntry(a[0]);

    }
    #endregion

    #region Call UI
    public void ShowHintInventory(List<Hints> hints)
    {
        CanvasManager.instance.ShowInventoryPanel(hints);
    }
    public void ShowSaveMenu()
    {
        CanvasManager.instance.ShowSaveMenu();
    }

    public void ChangeMainWindow(Hints hint)
    {
        CanvasManager.instance.ChangeMainWindow(hint);
    }

    public void UpdateLife(int value)
    {
        CanvasManager.instance.UpdateLifeSliderValue(value);
    }

    public void UpdateCrowd(int value)
    {
        CanvasManager.instance.UpdateHeatSliderValue(value);
    }

    public void UpdateLocution(int value)
    {
        CanvasManager.instance.UpdateSpeechSliderValue(value);
    }

    #endregion

    public void FindObject(Hints hint, int inventorySize)
    {
        CanvasManager.instance.AddObjectToInvetory(hint, inventorySize);
    }

    #region CONVINCE
    public void PlayerLooseConvince()
    {
        //DialogueManager.StopConversation();
        //DialogueManager.StartConversation("defeat");
    }

    public void PlayerWinConvince()
    {
        //DialogueManager.StopConversation();
        //DialogueManager.StartConversation("defeat");
    }
    #endregion

    #region De-Escalate
    public void PlayerLooseDe_Escalate()
    {
        //DialogueManager.StopConversation();
        //DialogueManager.StartConversation("defeat");
    }

    public void PlayerWinDe_Escalate()
    {
        //DialogueManager.StopConversation();
        //DialogueManager.StartConversation("defeat");
    }

    private void CreatePlayerInventoryForDeEscalate()
    {
        m_Player.PlayerHints.Clear();
        m_Player.CreateInventory(playerInventory);
    }
    #endregion

    #region Speech

    public void ShowText(int index, string speechPreviousText)
    {
        CanvasManager.instance.ShowSpeechText(index, speechPreviousText);
    }

    public void NewCodexEntry(CodexContent newCodexEntry)
    {
        CanvasManager.instance.SetUpCodex(newCodexEntry);
    }

    #endregion

    void LogicFastForward()
    {
        print(!(DialogueLua.GetVariable("Justification").asBool && DialogueLua.GetVariable("Objection").AsBool));
        if( !(DialogueLua.GetVariable("Justification").asBool && DialogueLua.GetVariable("Objection").AsBool))
        {
            m_Player.FastForwardLogics.OnFastForward();
        }
            
    }

    #region ChangeEnum
    public void ChangeToPresent()
    {
        IsPresent = true;
    }
    public void ChangeToInspect()
    {
        GameState = GameStatus.Inspect;
        m_Player.SetUpInspect();
        IsPresent = false;
    }
    public void ChangeToConvince()
    {
        GameState = GameStatus.Convince;
        IsPresent = false;
    }

    public void ChangeToDe_Escalate()
    {
        GameState = GameStatus.DeEscalate;
    }

    public void ChangeToSpeech()
    {
        GameState = GameStatus.Speech;
    }

    #endregion

    #region Button Event

    public void MoveScene(string sceneName)
    {
        Debug.Log(sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void SpeakToA_NPC(int index)
    {
        LevelManager.instance.GetComponent<DialogueSystemTrigger>().OnUse();
    }
    #endregion

    #region Set Language

    public void ChangeLanguage(string language)
    {
        DialogueManager.SetLanguage(language.ToUpper());
    }


    #endregion

    #region GETTER & SETTER
    public Player Player { get => m_Player; set => m_Player = value; }
    public GameStatus GameState { get => m_GameState; set => m_GameState = value; }
    public bool IsPresent { get => isPresent; set => isPresent = value; }

    #endregion

    #region Lua region

    private void OnEnable()
    {
        Lua.RegisterFunction("Present", this, SymbolExtensions.GetMethodInfo(() => ChangeToPresent()));
        Lua.RegisterFunction("Inspect", this, SymbolExtensions.GetMethodInfo(() => ChangeToInspect()));
        Lua.RegisterFunction("Convince", this, SymbolExtensions.GetMethodInfo(() => ChangeToConvince()));
        Lua.RegisterFunction("De-Esclate", this, SymbolExtensions.GetMethodInfo(() => ChangeToDe_Escalate()));
        Lua.RegisterFunction("Speech", this, SymbolExtensions.GetMethodInfo(() => ChangeToSpeech()));
        Lua.RegisterFunction("LogicFastForward", this, SymbolExtensions.GetMethodInfo(() => LogicFastForward()));
    }
    
    private void OnDisable()
    {
        Lua.UnregisterFunction("Present");
        Lua.UnregisterFunction("Inspect");
        Lua.UnregisterFunction("Convince");
        Lua.UnregisterFunction("De-Esclate");
        Lua.UnregisterFunction("Speech");
        Lua.UnregisterFunction("LogicFastForward");
    }

    #endregion
}
