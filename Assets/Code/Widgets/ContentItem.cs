using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContentItem : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI tagText;

    public virtual void Setup(SaveData.Clan clan)
    {
        nameText.text = clan?.name;
        tagText.text = clan?.tag;
    }
}
