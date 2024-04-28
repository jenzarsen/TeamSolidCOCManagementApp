using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContentItem : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI tagText;

    public virtual void Setup(string name, string tag)
    {
        nameText.text = name;
        tagText.text = tag;
    }
}
