using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClanContentBuilder : ContentBuilder
{
    public override void RefreshContent()
    {
        items.ForEach(x => GameObject.Destroy(x.gameObject));
        items.Clear();

        Debug.Log("Refreshing Content");
        foreach(var clan in SaveContainer.saveData.clanList)
        {
            var newItem = GameObject.Instantiate(contentPrefab, contentRoot).GetComponent<ClanItem>();
            newItem.Setup(clan.name, clan.tag);

            newItem.gameObject.SetActive(true);

            items.Add(newItem);
        }
    }
}
