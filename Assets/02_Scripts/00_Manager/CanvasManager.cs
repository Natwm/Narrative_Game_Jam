using System.IO;
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

    [SerializeField] private GameObject share_Panel;
    [SerializeField] private GameObject inputPanel_Panel;
    [SerializeField] private GameObject Validation_Panel;

    [SerializeField] private GameObject inputField;

    private bool TakeScreenshotOnNextFrame = false;

    public string plush = "";

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
        inputPanel_Panel.SetActive(false);
        Validation_Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    public void ChangePlushName(string s)
    {
        plush = s;
    }

    public void ShowinputPanel()
    {
        inputPanel_Panel.SetActive(true);
    }

    public void UpdatePelucheName()
    {
        print(plush);
        DialogueLua.SetVariable("PlushName", plush); 
    }

    public void ShareScore()
    {
        share_Panel.SetActive(true);

        StartCoroutine("TakeScreenShotAndShare");
    }

    IEnumerator TakeScreenShotAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D tx = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        tx.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tx.Apply();

        string path = Path.Combine(Application.temporaryCachePath, "sharedImage.png");
        File.WriteAllBytes(path, tx.EncodeToPNG());

        Destroy(tx);

        new NativeShare()
            .AddFile(path)
            .SetSubject("This is my score")
            .SetText("Share your scrore with your friends")
            .Share();

        share_Panel.SetActive(false);
    }

    private void OnPostRender()
    {
        if (TakeScreenshotOnNextFrame)
        {
            TakeScreenshotOnNextFrame = false;
            RenderTexture renderTexture = Camera.main.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();

            System.IO.File.WriteAllBytes(Application.dataPath + "/GameCapture.png", byteArray);
            RenderTexture.ReleaseTemporary(renderTexture);
            Camera.main.targetTexture = null;
        }
    }


    private void TakeScreenshot (int width, int height)
    {
        Camera.main.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        TakeScreenshotOnNextFrame = true;
    }

    public static void TakeScreenDhot_Static(int width, int height)
    {
        instance.TakeScreenshot( width,  height);
    }

    #region GETTER & SETTER

    #endregion

    #region Lua region
    void OnEnable()
    {
        Lua.RegisterFunction("InputPanel", this, SymbolExtensions.GetMethodInfo(() => ShowinputPanel()));
    }

    void OnDisable()
    {
        Lua.UnregisterFunction("InputPanel");
    }

    #endregion
}
