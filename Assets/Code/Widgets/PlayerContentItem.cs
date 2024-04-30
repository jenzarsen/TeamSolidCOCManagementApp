using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContentItem : ContentItem
{
    SaveData.ClanMember currentPlayer;

    public override void SetData<T>(T player)
    {
        currentPlayer = player as SaveData.ClanMember;
        Setup();
    }

    protected override void Setup()
    {
        nameText.text = currentPlayer?.name;
    }
}
