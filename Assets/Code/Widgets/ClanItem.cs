using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static SaveData;

public class ClanItem : ContentItem
{
    SaveData.Clan currentClan;

    [SerializeField] RawImage clanIcon;
    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError)
            Debug.Log(request.error);
        else
            clanIcon.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }

    private void OnEnable()
    {
        //Debug.Log(currentClan.badgeUrls.medium);
        StartCoroutine(DownloadImage(currentClan.badgeUrls.medium));
    }

    public override void Setup(SaveData.Clan clan)
    {
        currentClan = clan;
        base.Setup(clan);
    }
}
