using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Drag_and_Drop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler, IPointerUpHandler
{
    [SerializeField] private Canvas m_Canvas;
    [SerializeField] private int index;

    [SerializeField] private bool canCheck;
    [SerializeField] private bool status;

    private RectTransform rectTranform;
    private CanvasGroup canvasGroup;
    private TMP_Text speechText;

    private Transform m_Parent;

    public int Index { get => index; set => index = value; }
    public bool CanCheck { get => canCheck; set => canCheck = value; }
    public bool Status { get => status; set => status = value; }
    public TMP_Text SpeechText { get => speechText; set => speechText = value; }

    // Start is called before the first frame update
    private void Awake()
    {
        rectTranform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        m_Parent = transform.parent;
        speechText = transform.GetChild(0).GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region Interface

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!Status)
        {
            transform.parent = m_Parent;
            rectTranform.pivot = Vector2.zero;
            Debug.Log("OnBeginDrag");
            canvasGroup.alpha = 0.75f;
            canvasGroup.blocksRaycasts = false;
        }
        //CanvasManager.instance.SpeechButton(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        if (!Status)
        {
            rectTranform.anchoredPosition += eventData.delta / m_Canvas.scaleFactor;

            float newX = Mathf.Clamp(rectTranform.anchoredPosition.x, 0, Screen.width - rectTranform.sizeDelta.x);
            float newY = Mathf.Clamp(rectTranform.anchoredPosition.y, 0, Screen.height - rectTranform.sizeDelta.y);

            rectTranform.anchoredPosition = new Vector2(newX, newY);
            canCheck = false;

            GetComponent<Image>().color = Color.black;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (canCheck && !status)
            LevelManager.instance.CheckSpeech(this);
    }

    #endregion
}
