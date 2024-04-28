using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class ClanUI : BaseUI
{
    [SerializeField] ClanContentBuilder contentBuilder;
    [SerializeField] Button addClanButton;

    public override void Initialize()
    {
        addClanButton.onClick.RemoveAllListeners();
        addClanButton.onClick.AddListener(() => AddClan());
        base.Initialize();
    }

    public void AddClan()
    {
        var ui = UIController.GetUI<AddClanUI>();

        ui.Show();

        ui.onHiddenOneshot += () =>
        {
            Debug.Log(ui.isConfirmed);
            if (ui.isConfirmed)
            {
                SaveContainer.saveData.VerifyClanData(ui.GetID());
            }
        };
    }

    public override void OnBeginShow()
    {
        RefreshContent();
    }

    public void RefreshContent()
    {
        
    }
}