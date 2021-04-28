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

    public Dictionary<string, Hint> a;

    [Header("Panel")]
    [SerializeField] private GameObject mainPanelGO;
    [SerializeField] private GameObject inventoryPanelGO;
    [SerializeField] private GameObject menuPanelGO;
    [SerializeField] private GameObject choicePanelGO;
    [SerializeField] private GameObject speechPanelGO;
    [SerializeField] private GameObject codex_Panel;

    [Header("Main Window")]
    [SerializeField] private GameObject mainHintImage;

    [Space]
    [SerializeField] private TMP_Text mainHintdescription;

    [Space]
    [Header("Inventory")]
    [SerializeField] private GameObject ui_InventoryButton;

    [Space]
    [Header("Present Window")]
    [SerializeField] private GameObject ui_PresentButton;

    [Space]
    [Header("Dialogue")]
    [SerializeField] private GameObject textPanel;

    [Space]
    [Header("Slider")]
    [SerializeField] private GameObject lifeSlider;
    [SerializeField] private GameObject heatSlider;
    [SerializeField] private GameObject speechSlider;

    [Space]
    [Header("Speech")]
    [SerializeField] private GameObject speechContainer;
    [SerializeField] private TMP_Text speechDialogue;

    [Space]
    [Header("Codex")]
    [SerializeField] private GameObject codex_ButtonContainer;
    [SerializeField] private GameObject codex_ButtonPrefabs;

    [Space]
    [SerializeField] private TMP_Text codex_Title;
    [SerializeField] private TMP_Text codex_Citation;
    [SerializeField] private TMP_Text codex_Info;
    [SerializeField] private TMP_Text codex_Content;
    [SerializeField] private Image codex_Image;

    private Dictionary<string, CodexContent> codex_Dico = new Dictionary<string, CodexContent>();

    private Hints mainItem;

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
        switch (GameManager.instance.GameState)
        {
            case GameManager.GameStatus.Convince:
                SetUpConvinceUI();
                break;

            case GameManager.GameStatus.Inspect:
                SetUpInspec_And_PresentUI();
                break;

            case GameManager.GameStatus.Present:
                SetUpInspec_And_PresentUI();
                break;

            case GameManager.GameStatus.DeEscalate:
                SetUpDe_EscalateUI();
                break;

            case GameManager.GameStatus.Speech:
                SetUpDe_SpeechUI();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            textPanel.GetComponent<Image>().color = Color.red;
            //print(FindObjectOfType<StandardDialogueUI>().gameObject.GetComponent<Image>().color = Color.red);
        }
    }
    #endregion

    public void test(GameObject go)
    {
        textPanel = go;
    }

    #region Set Up UI
    public void SetUpConvinceUI()
    {
        ShowPresentButton();

        codex_Panel.SetActive(false);
        mainPanelGO.SetActive(false);
        choicePanelGO.SetActive(false);
        speechPanelGO.SetActive(false);
        menuPanelGO.SetActive(false);
        heatSlider.SetActive(false);

        lifeSlider.SetActive(true);

        lifeSlider.GetComponent<Slider>().maxValue = FindObjectOfType<Player>().MaxPenality;
        lifeSlider.GetComponent<Slider>().value = FindObjectOfType<Player>().MaxPenality;
    }

    public void SetUpInspec_And_PresentUI()
    {
        ShowPresentButton();

        choicePanelGO.SetActive(true);

        codex_Panel.SetActive(false);
        speechPanelGO.SetActive(false);
        mainPanelGO.SetActive(false);
        menuPanelGO.SetActive(false);

        lifeSlider.SetActive(false);
        heatSlider.SetActive(false);
    }

    public void SetUpDe_EscalateUI()
    {
        ShowPresentButton();

        codex_Panel.SetActive(false);
        mainPanelGO.SetActive(false);
        choicePanelGO.SetActive(false);
        speechPanelGO.SetActive(false);
        menuPanelGO.SetActive(false);
        lifeSlider.SetActive(false);

        heatSlider.SetActive(true);

        heatSlider.GetComponent<Slider>().maxValue = FindObjectOfType<Player>().MaxNervousness;
        heatSlider.GetComponent<Slider>().value = FindObjectOfType<Player>().Nervousness;
    }

    public void SetUpDe_SpeechUI()
    {
        lifeSlider.SetActive(false);
        heatSlider.SetActive(false);

        codex_Panel.SetActive(false);
        mainPanelGO.SetActive(false);
        choicePanelGO.SetActive(false);
        menuPanelGO.SetActive(false);

        speechPanelGO.SetActive(true);

        speechSlider.GetComponent<Slider>().maxValue = FindObjectOfType<Player>().MaxLocution;
        speechSlider.GetComponent<Slider>().value = FindObjectOfType<Player>().MaxLocution;
    }

    public void SetUpCodex(List<CodexContent> content)
    {
        foreach (CodexContent item in content)
        {
            GameObject codexObject = Instantiate(codex_ButtonPrefabs, codex_ButtonContainer.transform);
            Button codexButton = codexObject.GetComponent<Button>();
            CodexElt objectContent = codexObject.GetComponent<CodexElt>();
            string firstName = item.Title.Split(' ')[0];

            objectContent.Content = item;
            objectContent.Name = item.Title;
            item.Button = codexButton.gameObject;
            codex_Dico.Add(firstName, item);

            codexButton.onClick.AddListener(delegate { UpdateCodexContent(firstName); });

            codexButton.interactable = objectContent.Show;

            if (!objectContent.Show)
            {
                codexButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "???";
            }

            print(codex_Dico[item.Title]);
        }
    }

    public void SetUpCodex(CodexContent content)
    {
        //codex_Panel.SetActive(true);
        GameObject codexObject = Instantiate(codex_ButtonPrefabs, codex_ButtonContainer.transform);
        Button codexButton = codexObject.GetComponent<Button>();
        CodexElt objectContent = codexObject.GetComponent<CodexElt>();
        string firstName = content.Title.Split(' ')[0];

        objectContent.Content = content;
        objectContent.Name = content.Title;
        content.Button = codexButton.gameObject;

        codex_Dico.Add(firstName, content);

        codexButton.onClick.AddListener(delegate { UpdateCodexContent(firstName); });

        codexButton.interactable = objectContent.Show;

        if (!objectContent.Show)
        {
            codexButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "???";
        }

        print(firstName);
        print(codex_Dico[firstName].Title);
    }

    void SetUpPanel()
    {
        mainPanelGO.SetActive(false);
        inventoryPanelGO.SetActive(false);
        menuPanelGO.SetActive(false);
        choicePanelGO.SetActive(false);
        speechPanelGO.SetActive(false);
    }

    #endregion

    #region Clikable Text

    public void clickObject(string text)
    {
        if (codex_Dico.ContainsKey(text)){
            Button codexButton = codex_Dico[text].Button.GetComponent<Button>();
            TMP_Text buttonText = codex_Dico[text].Button.transform.GetChild(0).GetComponent<TMP_Text>();

            codexButton.interactable = true;
            buttonText.text = codex_Dico[text].Title;
        }
    }

    public void AddCodex(CodexContent content)
    {
        GameObject codexObject = Instantiate(codex_ButtonPrefabs, codex_ButtonContainer.transform);
        CodexElt objectContent = codexObject.GetComponent<CodexElt>();

        objectContent.Content = content;
        objectContent.Name = content.Title;
        codex_Dico.Add(content.Title, content);
        print(codex_Dico[content.Title]);
    }

    #endregion

    #region Slider

    public void UpdateLifeSliderValue(int value)
    {
        lifeSlider.GetComponent<Slider>().value = value;
    }

    public void UpdateHeatSliderValue(int value)
    {
        heatSlider.GetComponent<Slider>().value = value;
    }

    public void UpdateSpeechSliderValue(int value)
    {
        speechSlider.GetComponent<Slider>().value = value;
    }

    #endregion

    #region Show UI Element
    public void ShowSaveMenu()
    {
        menuPanelGO.SetActive(!menuPanelGO.active);
    }

    public void ShowInventoryPanel(List<Hints> hints)
    {
        mainPanelGO.SetActive(!mainPanelGO.active);

        if (mainPanelGO.active && hints.Count > 0)
        {
            Hints firstHint = hints[0];
            mainItem = hints[0];

            mainHintdescription.text = firstHint.hint_data.name + " \n\n" + firstHint.hint_data.sentence;
            mainHintImage.GetComponent<Image>().sprite = firstHint.hint_data.artwork;
            ShowPresentButton();
        }

    }

    public void ShowInventoryPanel()
    {
        if (!mainPanelGO.active)
        {
            mainPanelGO.SetActive(true);
            ShowPresentButton();
        }

    }

    public void tes()
    {
        Debug.Log("button");
        codex_Panel.SetActive(true);
    }

    private void ShowPresentButton()
    {
        if (GameManager.instance.IsPresent)
            ui_PresentButton.SetActive(true);
        else
            ui_PresentButton.SetActive(false);
    }

    public void ShowChoicePanel()
    {
        choicePanelGO.SetActive(!choicePanelGO.active);
        DialogueManager.StopConversation();
    }

    public void ChangeMainWindow(Hints hint)
    {
        if (!mainPanelGO.active)
        {
            mainPanelGO.SetActive(true);
            ShowPresentButton();
        }

        mainItem = hint;

        mainHintdescription.text = hint.hint_data.name + " \n\n" + hint.hint_data.sentence;
        mainHintImage.GetComponent<Image>().sprite = hint.hint_data.artwork;
    }

    public void CloseWindow()
    {
        mainPanelGO.SetActive(false);
    }

    #endregion

    #region Inventory
    public void AddObjectToInvetory(Hints hint, int inventorySize)
    {
        GameObject uiButton = Instantiate(ui_InventoryButton, inventoryPanelGO.transform);
        uiButton.GetComponent<Image>().sprite = hint.hint_data.artwork;
        uiButton.GetComponent<Button>().onClick.AddListener(delegate { GameManager.instance.Player.ChangeMainHint(inventorySize); });
    }

    public void ClearInventory()
    {
        for (int i = 0; i < inventoryPanelGO.transform.childCount; i++)
        {
            Destroy(inventoryPanelGO.transform.GetChild(i).gameObject);
        }
    }

    public void SetIventoryPanel(List<Hint> hints)
    {
        for (int i = 0; i < hints.Count; i++)
        {
            Hint hint = hints[i];
            inventoryPanelGO.transform.GetChild(i).GetComponent<Image>().sprite = hint.Image;
        }
    }

    #endregion

    #region Speech

    public void ShowSpeechText(int index, string speechPreviousText)
    {
        speechDialogue.text = speechPreviousText;
    }

    #endregion

    #region Codex

    void UpdateCodexContent(string codexELT)
    {
        CodexContent elt = codex_Dico[codexELT];

        codex_Content.text = elt.Content;
        codex_Citation.text = elt.Citation;
        codex_Image.sprite = elt.Image;
        codex_Info.text = elt.Information;
        codex_Title.text = elt.Title;
    }

    #endregion

    #region GETTER & SETTER

    public Hints MainItem { get => mainItem; set => mainItem = value; }

    #endregion

    #region Lua region

    private void OnEnable()
    {
        Lua.RegisterFunction("ShowChoicePanel", this, SymbolExtensions.GetMethodInfo(() => ShowChoicePanel()));
    }

    private void OnDisable()
    {
        Lua.UnregisterFunction("ShowChoicePanel");
    }

    #endregion
}
