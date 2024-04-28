using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

[System.Serializable]
public class SaveData
{
    public List<Player> playerList = new List<Player>();
    public List<Clan> clanList = new List<Clan>();

    [System.Serializable]
    public class Clan
    {
        public string clanTag;
        public string clanName;
        public List<string> clanMembers = new List<string>();
    }

    public void VerifyClanData(string clanTag)
    {
        var data = NetworkController.instance.DownloadClanData(clanTag);

        if(string.IsNullOrEmpty(data) == false)
        {
            Debug.Log("Data has been downloaded");
            //Debug.Log(data);
        }
    }

    public Clan GetClanByTag(string tag)
    {
        return clanList.FirstOrDefault(clan => clan.clanTag.Equals(tag, StringComparison.OrdinalIgnoreCase));
    }

    public void AddClan(string tag, string name, List<string> members)
    {
        var clan = GetClanByTag(tag);

        if(clan == null)
        {
            clan = new Clan()
            {
                clanTag = tag,
                clanName = name,
                clanMembers = members
            };

            clanList.Add(clan);
        }
        else
        {
            clan.clanTag = tag;
            clan.clanName = name;
            clan.clanMembers = members;
        }
    }


    #region Player
    [System.Serializable]
    public class Player
    {
        public string id;
        public string name;
        public string th;
    }

    public void AddPlayer(string id, string name)
    {
        var player = GetPlayerById(id);

        if(player == null)
        {
            player = new Player();
        }

        player.id = id;
        player.name = name;

        if(playerList.Contains(player) == false)
        {
            playerList.Add(player);
            SaveContainer.IsDirty = true;
        }
    }

    public void VerifyPlayerData(Player newPlayer)
    {
        NetworkController.instance.DownloadPlayerData(newPlayer.id);
        //return;

        //if (IsPlayerIDExists(newPlayer) == false)
        //{
        //    UnityEngine.Debug.Log("Player has been added to the database...");
        //    AddNewPlayer(newPlayer);
        //    return;
        //}
    }

    public string UpdatePlayerData(Player newPlayer)
    {
        return "Player data has been updated...";
    }

    public void AddNewPlayer(Player newPlayer)
    {
        playerList.Add(newPlayer);

        SaveContainer.IsDirty = true;
    }

    public bool IsPlayerIDExists(Player newPlayer)
    {
        return playerList.Any(x => x.id.Equals(newPlayer.id, System.StringComparison.OrdinalIgnoreCase));
    }

    public Player GetPlayerById(string id)
    {
        return playerList.FirstOrDefault(x => x.id == id);
    }

    public Player GetPlayerByName(string name)
    { 
        return playerList.FirstOrDefault(x => x.name == name);
    }
    #endregion
}
