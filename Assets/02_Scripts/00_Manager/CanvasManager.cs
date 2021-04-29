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

    #region GETTER & SETTER

    #endregion

    #region Lua region


    #endregion
}
