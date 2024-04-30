using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerContentItem : ContentItem
{
    SaveData.ClanMember currentPlayer;

    [SerializeField] TextMeshProUGUI ratingText;

    public override void SetData<T>(T player)
    {
        currentPlayer = player as SaveData.ClanMember;
        Setup();
    }

    protected override void Setup()
    {
        nameText.text = currentPlayer?.name;
        ratingText.text = currentPlayer?.GetTotalArmyLevel().ToString();
    }

}
