using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringHelper
{
    const string HASH = "#";
    const string HASH_WEB = "%23";

    public static string GetWebTagConversion(string tag)
    {
        //Trim Unnecessary Space
        tag = tag.Trim();

        if (tag.Contains(HASH))
        {
            //Replace # with %23
            //Ex. #FFFFFFF to %23FFFFFF
            tag = tag.Replace(HASH, HASH_WEB);
        }
        else
        {
            //In case there's no #, add %23
            //Ex. FFFFFF to %23FFFFFF
            tag += HASH_WEB;
        }

        return tag;
    }
}
