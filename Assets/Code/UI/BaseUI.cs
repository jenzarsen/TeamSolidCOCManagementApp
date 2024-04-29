using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour
{
    public UnityAction onHiddenOneshot = null;
    public UnityAction onShowOneshot = null;


    private void Awake()
    {
        Initialize();
    }
    public virtual void Initialize()
    {

    }

    public virtual void OnBeginShow()
    {

    }

    public virtual void Show()
    {
        OnBeginShow();
        gameObject.SetActive(true);
        OnFinishShow();
    }

    public virtual void OnFinishShow()
    {
        onHiddenOneshot?.Invoke();
        onHiddenOneshot = null;
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
        OnFinishShow();
    }

    public void SetupButton(Button button, UnityAction andThen = null)
    {
        button?.onClick.RemoveAllListeners();
        button?.onClick.AddListener(() => andThen?.Invoke());
    }
}
