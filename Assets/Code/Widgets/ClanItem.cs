using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClanItem : MonoBehaviour
{
    public TextMeshProUGUI clanNameText;
    public TextMeshProUGUI tagText;

    public void Setup(string name, string tag, string thLevel)
    {
        clanNameText.text = name;
        tagText.text = tag;
    }

}
