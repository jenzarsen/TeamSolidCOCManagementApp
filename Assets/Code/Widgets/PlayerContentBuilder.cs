using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerContentBuilder : ContentBuilder
{
    public override void RefreshContent()
    {
        items.ForEach(x => GameObject.Destroy(x.gameObject));
        items.Clear();

        Debug.Log("Refreshing Player Content...");

        var orderedMembers = SaveContainer.saveData.memberList.OrderByDescending(x => x.GetTotalArmyLevel()).ToArray();

        for(int i = 0; i < orderedMembers.Length; i++)
        {
            var member = orderedMembers[i];

            var newItem = GameObject.Instantiate(contentPrefab, contentRoot).GetComponent<PlayerContentItem>();

            newItem.SetIndex(i + 1);
            newItem.SetData(member);
            newItem.gameObject.SetActive(true);

            items.Add(newItem);
        }
    }
}
