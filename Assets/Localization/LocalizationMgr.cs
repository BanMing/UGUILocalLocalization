using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;

public class LocalizationMgr
{
    public static LocalizationMgr Instance
    {
        get { return Nest.instance; }
    }

    private class Nest
    {
        static Nest()
        {
        }
        internal static readonly LocalizationMgr instance = new LocalizationMgr();
    }
    private ScriptableObject curWords;

    public Action RefreshHander;
    public void ChangeLanguage(SystemLanguage language)
    {

        // switch (language)
        // {
        //     case:

        //     default:
        // }
        // Assembly.GetCallingAssembly().
        Debug.Log("Localization_" + Enum.Parse(typeof(SystemLanguage), language.ToString()).ToString());
        var method = Type.GetType("Localization_" + Enum.Parse(typeof(SystemLanguage), language.ToString()).ToString()).GetMethod("GetData");
        var word=method.Invoke(null, new object[] { 1 });
        // var info=word.GetType().GetMember();
        // Debug.Log("info:"+info.Length);
        // var value=info.GetValue(word,null);
        Debug.Log("word:"+word.GetType().GetProperty("value").GetValue(word,null));
        if (RefreshHander != null)
        {
            RefreshHander();
        }

    }
    public string GetWordById(int id)
    {
        //    var data= LocalizationConfig.Sheet.GetData(1);
        return "";
    }

}