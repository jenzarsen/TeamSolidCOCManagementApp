using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : BaseUI
{
    public Button addButton;

    public override void Initialize()
    {
        addButton.onClick.RemoveAllListeners();
        addButton.onClick.AddListener(() => ShowClanUI());
    }

    public void ShowClanUI()
    {
        ClanUI ui = UIController.GetUI<ClanUI>();
        ui.Show();
    }
}
