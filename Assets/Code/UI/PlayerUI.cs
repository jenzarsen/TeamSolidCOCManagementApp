using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : BaseUI
{
    [SerializeField] PlayerContentBuilder contentBuilder;

    public override void OnBeginShow()
    {
        RefreshContent();
    }

    public void RefreshContent()
    {
        contentBuilder.RefreshContent();
    }
}
