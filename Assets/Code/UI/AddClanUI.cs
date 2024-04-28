using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddClanUI : BaseUI
{
    public Button addButton;
    public Button closeButton;

    public TMP_InputField idField;

    public bool isConfirmed = false;
    public override void Initialize()
    {
        addButton.onClick.RemoveAllListeners();
        addButton.onClick.AddListener(() => OnAddClan());

        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(() => OnClose());
    }

    public string GetID()
    {
        return idField.text.Trim();
    }

    void OnAddClan()
    {
        //
        isConfirmed = true;
        UIController.GetUI<ClanUI>().RefreshContent();
        Hide();
    }

    void OnClose()
    {
        isConfirmed = false;
        Hide();
    }
}
