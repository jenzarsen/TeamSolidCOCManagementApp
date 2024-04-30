using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ContentItem : MonoBehaviour
{
    public Image bg;
    public CanvasGroup canvasGroup;

    public TextMeshProUGUI indexText;
    public TextMeshProUGUI nameText;

    RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = transform as RectTransform;
    }

    public void SetIndex(int index)
    {
        indexText.text = index.ToString();
    }

    public virtual void SetData<T>(T data)
    {

    }

    protected virtual void Setup()
    {

    }

    private void Update()
    {
        if(bg && canvasGroup)
        {
            var screenPos = Camera.main.WorldToScreenPoint(transform.position);
            bool IsInsideScreen =
            RectTransformUtility.RectangleContainsScreenPoint(UIController.instance.bgRect, screenPos, Camera.main);

            canvasGroup.alpha = IsInsideScreen ? 1 : 0;
        }
    }
}
