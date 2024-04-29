using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContentItem : MonoBehaviour
{
    public TextMeshProUGUI nameText;

    public virtual void SetData<T>(T data)
    {

    }

    protected virtual void Setup()
    {

    }
}
