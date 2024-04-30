using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : BaseUI
{
    public Button clanListButton;
    public Button refreshDataButton;
    public Button playerListButton;
    public Button clearDataButton;

    public override void Initialize()
    {
        SetupButton(clanListButton, () => OnShowClanUI());
        SetupButton(playerListButton, () => OnShowPlayerUI());
        SetupButton(refreshDataButton, () => OnRefreshData());
        SetupButton(clearDataButton, () => OnClearData());
    }

    public void OnClearData()
    {
        UIController.instance.HideAllUI();
        SaveContainer.instance.ClearData();
    }

    public void OnShowClanUI()
    {
        UIController.instance.HideAllUI();
        ClanUI ui = UIController.GetUI<ClanUI>();
        ui.Show();
    }

    public void OnShowPlayerUI()
    {
        UIController.instance.HideAllUI();
        PlayerUI ui = UIController.GetUI<PlayerUI>();
        ui.Show();
    }

    public void OnRefreshData()
    {
        foreach(var clan in SaveContainer.saveData.clanList)
        {
            SaveContainer.saveData.VerifyClanData(clan.tag);
        }
        Debug.Log("Data has been updated");
    }
}
