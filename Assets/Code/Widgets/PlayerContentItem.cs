using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContentItem : ContentItem
{
    SaveData.Player currentPlayer;

    public override void SetData<T>(T player)
    {
        currentPlayer = player as SaveData.Player;
        Setup();
    }

    protected override void Setup()
    {
        nameText.text = currentPlayer?.name;
        tagT.text = currentPlayer?.tag;
    }
}
