using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : BaseUI
{
    public Button clanListButton;
    public Button refreshDataButton;

    public override void Initialize()
    {
        clanListButton.onClick.RemoveAllListeners();
        clanListButton.onClick.AddListener(() => ShowClanUI());

        refreshDataButton.onClick.RemoveAllListeners();
        refreshDataButton.onClick.AddListener(() => RefreshData());
    }

    public void ShowClanUI()
    {
        ClanUI ui = UIController.GetUI<ClanUI>();
        ui.Show();
    }

    public void RefreshData()
    {
        foreach(var clan in SaveContainer.saveData.clanList)
        {
            SaveContainer.saveData.VerifyClanData(clan.tag);
        }
        Debug.Log("Data has been updated");
    }
}
