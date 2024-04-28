using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using UnityEngine;
using System.Collections;
using static System.Net.WebRequestMethods;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using static SaveData;
using UnityEngine.Events;
using Unity.VisualScripting;

class NetworkController : Singleton<NetworkController>
{
    static HttpClient client = new HttpClient();

    public string downloadedJSON = string.Empty;

    const string token = 
        "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiIsImtpZCI6IjI4YTMxOGY3LTAwMDAtYTFlYi03ZmExLTJjNzQzM2M2Y2NhNSJ9.eyJpc3MiOiJzdXBlcmNlbGwiLCJhdWQiOiJzdXBlcmNlbGw6Z2FtZWFwaSIsImp0aSI6IjJmYmU4YjgxLTBmYjUtNDc2NC05MGI3LTJmM2NjZmM3NDY4NiIsImlhdCI6MTcxNDEwNDAxMywic3ViIjoiZGV2ZWxvcGVyLzk0YTk3YWE1LWI3M2EtZDRiMS00OTdkLWZkZDM0MzdmNmU1YiIsInNjb3BlcyI6WyJjbGFzaCJdLCJsaW1pdHMiOlt7InRpZXIiOiJkZXZlbG9wZXIvc2lsdmVyIiwidHlwZSI6InRocm90dGxpbmcifSx7ImNpZHJzIjpbIjExMi4yMDQuMjAwLjgwIl0sInR5cGUiOiJjbGllbnQifV19.hQwJkGwh-hIHo-ELc8-su05L6197_h_viviRwlloDaQ6W2AOTN8tHnECpFBVYySd5aWx4Dm531_IsziTrEZ-rQ";
    
    //Get Player Data
    public static async Task<string> GetPlayerAsync(string playerID)
    {
        string data = null;
        HttpResponseMessage response = await client.GetAsync($"https://api.clashofclans.com/v1/players/{playerID}");

        UnityEngine.Debug.Log(playerID);
        response.EnsureSuccessStatusCode();

        if (response.IsSuccessStatusCode)
        {
            data = await response.Content.ReadAsStringAsync();
        }

        UnityEngine.Debug.Log(response.IsSuccessStatusCode);
        return data;
    }

    //Get Clan Data
    public static async Task<string> GetClanAsync(string clanTag)
    {
        UnityEngine.Debug.Log($"Searching for clan tag {clanTag}");
        string data = null;


        if (clanTag.Contains("#"))
        {
            clanTag = clanTag.Replace("#", "%23");
        }
        else
        {
            clanTag += "%23";
        }

        string requestURL = $"https://api.clashofclans.com/v1/clans/{clanTag}/members?limit=50";

        //UnityEngine.Debug.Log(requestURL);


        HttpResponseMessage response = await client.GetAsync(requestURL);

        response.EnsureSuccessStatusCode();

        if (response.IsSuccessStatusCode)
        {
            data = await response.Content.ReadAsStringAsync();
        }

        //UnityEngine.Debug.Log(response.IsSuccessStatusCode);
        return data;
    }

    protected override void Awake()
    {
        base.Awake();
        InitializeNetworkApi();
    }
   
    void InitializeNetworkApi()
    {
        client.BaseAddress = new Uri("https://api.clashofclans.com/v1");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");

        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public void DownloadPlayerData(string playerID)
    {
        Task.Run(() => RunPlayerAsync(playerID));
    }

    public async Task RunPlayerAsync(string playerID)
    {

        UnityEngine.Debug.Log(playerID);
        try
        {
           
            string playerData = await GetPlayerAsync(playerID);
            UnityEngine.Debug.Log(playerData);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("Failed to get player data...");
        }
    }


    [Button]
    public string DownloadClanData(string clanTag)
    {
        return Task.Run(() => RunClanAsync(clanTag)).Result;
    }

    public async Task <string> RunClanAsync(string clanTag)
    {
        try
        {

            string clanData = await GetClanAsync(clanTag);
            //UnityEngine.Debug.Log(clanData);
            return clanData;
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("Failed to get Clan data...");
            return null;
        }
    }
}