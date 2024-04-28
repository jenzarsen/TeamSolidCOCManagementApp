using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using System.Runtime.CompilerServices;

[System.Serializable]
public class SaveData
{
    public List<Player> playerList = new ();
    public List<Clan> clanList = new ();

    [System.Serializable]
    public class Clan
    {
        public string tag;
        public string name;
        public BadgeURL badgeUrls;
        public ClanMember[] memberList;
    }

    [System.Serializable]
    public class BadgeURL
    {
        public string small;
        public string large;
        public string medium;
    }


    [System.Serializable]
    public class ClanMember
    {
        public string tag;
        public string name;
        public int townHallLevel;
    }

    public void VerifyClanData(string clanTag)
    {
        var data = NetworkController.instance.DownloadClanData(clanTag);

        if(string.IsNullOrEmpty(data) == false)
        {
            Debug.Log("Data has been downloaded");
            var clanData = JSONController.CreateClanFromJSON(data);

            var clan = clanList.FirstOrDefault(x => x.tag.Equals(clanData.tag));

            Debug.Log(clanData.badgeUrls.medium);
            if(clan == null)
            {
                clan = new Clan() {
                    tag = clanData.tag, 
                    name = clanData.name, 
                    memberList = clanData.memberList, 
                    badgeUrls = clanData.badgeUrls};
                clanList.Add(clan);
            }
            else
            {
                clan.tag = clanData.tag;
                clan.name = clanData.name;
                clan.memberList = clanData.memberList;
                clan.badgeUrls = clanData.badgeUrls;
            }

  

            SaveContainer.SetDirty();
        }
    }

    public Clan GetClanByTag(string clanTag)
    {
        return clanList.FirstOrDefault(clan => clan.tag.Equals(clanTag, StringComparison.OrdinalIgnoreCase));
    }

    public void AddClan(string clanTag, string clanName, List<string> members)
    {

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
            SaveContainer.SetDirty();
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

        SaveContainer.SetDirty();
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
