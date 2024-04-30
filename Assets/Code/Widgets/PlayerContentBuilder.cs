using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContentBuilder : ContentBuilder
{
    public override void RefreshContent()
    {
        items.ForEach(x => GameObject.Destroy(x.gameObject));
        items.Clear();

        Debug.Log("Refreshing Player Content...");
        foreach (var player in SaveContainer.saveData.memberList)
        {
            var newItem = GameObject.Instantiate(contentPrefab, contentRoot).GetComponent<PlayerContentItem>();

            newItem.SetData(player);
            newItem.gameObject.SetActive(true);

            items.Add(newItem);
        }
    }
}
