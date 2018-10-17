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

    private SystemLanguage curLanguage;

    //刷新变量回调
    public Action RefreshHander;
    //切换语言
    public void ChangeLanguage(SystemLanguage language)
    {
        if (!HasThisLanguageConfig(language))
        {
            language = SystemLanguage.English;
        }

        // PlayerPrefs.SetInt("AppLanguage", (int)language);
        curLanguage = language;
        if (RefreshHander != null)
        {
            RefreshHander();
        }

    }

    public void Init()
    {
        ExcelDataInit.Init();
        var language = PlayerPrefs.GetInt("AppLanguage", (int)Application.systemLanguage);
        LocalizationMgr.Instance.ChangeLanguage((SystemLanguage)language);
        // ChangeLanguage(SystemLanguage.English);
    }
    //检测是否有该语言版本
    private bool HasThisLanguageConfig(SystemLanguage language)
    {
        try
        {
            var scriptName = "Localization_" + Enum.Parse(typeof(SystemLanguage), language.ToString()).ToString();
            // Debug.Log("HasThisLanguageConfig:"+scriptName);
            Type.GetType(scriptName).GetMethod("GetData");
            return true;
        }
        catch (System.Exception)
        {
            Debug.LogError("No This Languge Config!");
            return false;
        }
    }
    public string GetWordByDirect(int id)
    {
        switch (curLanguage)
        {
            case SystemLanguage.ChineseSimplified:
                return Localization_ChineseSimplified.GetData(id).value;
            case SystemLanguage.English:
                return Localization_English.GetData(id).value;
            case SystemLanguage.Japanese:
                return Localization_Japanese.GetData(id).value;
            default:
                Debug.LogError("No This Language Data!");
                return "";
        }
    }
    //根据id获得字符
    public string GetWordByReflection(int id)
    {
        if (!HasThisLanguageConfig(curLanguage))
        {
            return "";
        }
        var scriptName = "Localization_" + Enum.Parse(typeof(SystemLanguage), curLanguage.ToString()).ToString();
        var method = Type.GetType(scriptName).GetMethod("GetData");
        var word = method.Invoke(null, new object[] { id });
        return word.GetType().GetField("value").GetValue(word).ToString();
    }

}