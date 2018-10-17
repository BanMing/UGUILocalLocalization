# UUGUI多语言工具
>**写在前面**

最近有一些时间了，想多跟进一下博客。这个工具是谈到海外游戏的时候想到需要这个工具。想着想着就写了，写的差不多的时候，顺便查了一下，结果发现有好多这样的工具，真是白造轮子了。
![assets store](https://upload-images.jianshu.io/upload_images/3438059-93f18b1c85f54f39.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)
221个啊，还有很么多免费的。看到这个就没有太想写了。

>**简单介绍一下**

首先项目是基于我的另一个[工具READ-EXCEL](https://www.jianshu.com/p/f62d2dab8a0a)，一个很简单的转表工具，这次写这个工具，还有修改了几个bug😂。

基本功能：实时切换语言、Text组件自动替换

![](https://upload-images.jianshu.io/upload_images/3438059-9510e5125dee0f33.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

基本原理：**把每一个字符串设置一个key值，我们需要这个字符串的时候通过key来需要当前语言这个key对于的字符串。换个说法就是把文字的key值和字符串用字典存起来，这就得到一个语言的所有字符串与key，然后再拿一个字典存语言key值与刚才得到的一个语言所有字符串的字典。**

获取文字有两种方式：
1、直接获取：

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
优势：速度快。劣势：每次需要重写改这里，如果添加了语言。

2、反射获得：

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
优势：添加语言不需要修改。劣势：速度慢。

差不多就这么多吧，没啥难度的工具。具体的可以看看代码。[GIT](https://github.com/BanMing/UGUILocalLocalization)。unity版本2017.3.1。



