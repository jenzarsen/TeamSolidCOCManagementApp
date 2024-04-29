using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : BaseUI
{
    public Button clanListButton;
    public Button refreshDataButton;
    public Button playerListButton;

    public override void Initialize()
    {
        SetupButton(clanListButton, () => ShowClanUI());
        SetupButton(playerListButton, () => ShowPlayerUI());
        SetupButton(refreshDataButton, () => RefreshData());
    }

    public void ShowClanUI()
    {
        UIController.instance.HideAllUI();
        ClanUI ui = UIController.GetUI<ClanUI>();
        ui.Show();
    }

    public void ShowPlayerUI()
    {
        UIController.instance.HideAllUI();
        PlayerUI ui = UIController.GetUI<PlayerUI>();
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
