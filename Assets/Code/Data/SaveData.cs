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
    public List<ClanMember> memberList = new ();
    public List<Clan> clanList = new ();

    [System.Serializable]
    public class Clan
    {
        public string tag;
        public string name;
        public BadgeURL badgeUrls;
        public ClanMember[] memberList;

        public void Clone(Clan clan)
        {
            this.tag = clan.tag;
            this.name = clan.name;
            this.badgeUrls = clan.badgeUrls;
            this.memberList = clan.memberList;
        }
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

        //public Clan clan;
        public string clanTag;

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

        public void Clone(ClanMember member)
        {
            this.tag = member.tag;
            this.name = member.name;
            this.townHallLevel = member.townHallLevel;
            this.townHallWeaponLevel = member.townHallWeaponLevel;
            this.expLevel = member.expLevel;    
            this.trophies = member.trophies;
            this.bestTrophies = member.bestTrophies;
            this.attackWins = member.attackWins;
            this.defenseWins = member.defenseWins;

            this.builderHallLevel = member.builderHallLevel;
            this.builderBaseTrophies = member.builderBaseTrophies;
            this.bestBuilderBaseTrophies = member.bestBuilderBaseTrophies;

            this.role = member.role;
            this.warPreference = member.warPreference;

            this.donations = member.donations;
            this.donationsReceived = member.donationsReceived;
            this.clanCapitalContributions = member.clanCapitalContributions;
            //this.clan = member.clan;
            this.clanTag = member.clanTag;

            this.troops = member.troops;
            this.heroEquipment = member.heroEquipment;
            this.spells = member.spells;
        }
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
            //Debug.Log("Data has been downloaded");
            var clanData = JSONController.CreateFromJSON <SaveData.Clan>(data);

            var clan = clanList.FirstOrDefault(x => x.tag.Equals(clanData.tag));

            if(clan == null)
            {
                clan = new Clan();
                clan.Clone(clanData);
                clanList.Add(clan);
            }
            else
            {
                //Update Existing Clan Data
                clan.Clone(clanData);
            }

            foreach(var member in clan.memberList)
            {
                var memberJson = NetworkController.instance.DownloadPlayerData(member.tag);

                var memberData = JSONController.CreateFromJSON<SaveData.ClanMember>(memberJson);

                //Note: Clan Tags are not automatically generated from Json File as doing so may cause serialization loop
                memberData.clanTag = clan.tag;

                var currentMember = memberList.FirstOrDefault(x => x.tag.Equals(memberData.tag, StringComparison.OrdinalIgnoreCase));

                if (currentMember != null)
                {
                    //Overwrite Existing Member Data
                    currentMember.Clone(memberData);
                }
                else
                {
                    //Add New Member Data in List
                    currentMember = new ClanMember();
                    currentMember.Clone(memberData);
                    memberList.Add(currentMember);
                }
            }

            SaveContainer.SetDirty();
        }
    }

    public Clan GetClanByTag(string clanTag)
    {
        return clanList.FirstOrDefault(clan => clan.tag.Equals(clanTag, StringComparison.OrdinalIgnoreCase));
    }
}
