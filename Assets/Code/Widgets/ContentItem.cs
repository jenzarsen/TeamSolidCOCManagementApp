using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContentItem : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI idText;
    public TextMeshProUGUI thText;


    public void Setup(string name, string id, string thLevel)
    {
        nameText.text = name;
        idText.text = id;
        thText.text = thLevel;
    }
}
