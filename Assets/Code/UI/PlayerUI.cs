using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : BaseUI
{
    [SerializeField] PlayerContentBuilder contentBuilder;
    [SerializeField] TMP_Dropdown dropdown;

    public override void Initialize()
    {
        base.Initialize();

        List<string> dropdownOptions = new();

        int options = (int)SortOptions.Last;

        for(int i = 0; i < options; i++)
        {
            var option = (SortOptions)i;
            dropdownOptions.Add(option.ToString());
        }
        dropdown.ClearOptions();
        dropdown.AddOptions(dropdownOptions);
    }

    public override void OnBeginShow()
    {
        RefreshContent();
    }

    public void RefreshContent()
    {
        contentBuilder.RefreshContent();
    }
}


public enum SortOptions
{
    Army,
    Heroes,
    Last
}