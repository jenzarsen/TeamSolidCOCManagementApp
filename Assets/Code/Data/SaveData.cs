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
    public List<ClanMember> playerList = new ();
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
        public int townHallWeaponLevel;
        public int expLevel;
        public int trophies;
        public int bestTrophies;
        public int warStars;

        public int attackWins;
        public int defenseWins;

        //Builder Hall
        public int builderHallLevel;
        public int builderBaseTrophies;
        public int bestBuilderBaseTrophies;


        public string role;
        public string warPreference;

        public int donations;
        public int donationsReceived;
        public int clanCapitalContributions;

        public Clan clan;

        //Values below exists in Clash of Clans API but will not be implemented yet
        //public League league;
        //public BuilderBaseLeague builderBaseLeague;
        //public LegendStatistics legendStatistics;
        //public Achievements[] achievements;
        //public PlayerHouse playerHouse;
        //public Label[] labels;

        public Troop[] troops;
        public Equipment[] heroEquipment;
        public Spell[] spells;
    }

    [System.Serializable]
    public class Entity
    {
        public string name;
        public string level;
        public string maxLevel;
        public string village;
    }

    [System.Serializable]
    public class Troop : Entity
    {

    }

    [System.Serializable]
    public class Hero : Entity
    {
        public Equipment[] equipment;
    }

    [System.Serializable]
    public class Equipment : Entity
    {

    }


    [System.Serializable]
    public class Spell : Entity
    {
       
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
}
