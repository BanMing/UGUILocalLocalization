using UnityEngine;

public class ExcelDataInit
{
    public static void Init()
    {
		 Resources.Load<Localization_ChineseSimplified>("DataConfig/Localization_ChineseSimplified").SetDic();
 Resources.Load<Localization_English>("DataConfig/Localization_English").SetDic();
 Resources.Load<Localization_Japanese>("DataConfig/Localization_Japanese").SetDic();
 Resources.Load<TestConfig_Sheet1>("DataConfig/TestConfig_Sheet1").SetDic();

        Resources.UnloadUnusedAssets();
    }
}