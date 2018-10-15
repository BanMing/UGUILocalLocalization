using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.Text;

public class Localization_ChineseSimplified_Importer : AssetPostprocessor
{
    private static readonly string filePath = "Assets/ReadExcel/Config/Excel/Localization.xlsx";
    private static readonly string sheetName = "ChineseSimplified" ;
    

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets)
        {
            if (!filePath.Equals(asset))
                continue;

            using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
				IWorkbook book = null;
				if (Path.GetExtension (filePath) == ".xls") {
					book = new HSSFWorkbook(stream);
				} else {
					book = new XSSFWorkbook(stream);
				}


					if (!Directory.Exists("Assets/ReadExcel/Config/Resources/DataConfig/"))
            	    {
            	        Directory.CreateDirectory("Assets/ReadExcel/Config/Resources/DataConfig/");
            	    }
                    var exportPath = "Assets/ReadExcel/Config/Resources/DataConfig/Localization_ChineseSimplified" + ".asset";
                    
                    // check scriptable object
                    var data = (Localization_ChineseSimplified)AssetDatabase.LoadAssetAtPath(exportPath, typeof(Localization_ChineseSimplified));
                    if (data == null)
                    {
                        data = ScriptableObject.CreateInstance<Localization_ChineseSimplified>();
                        AssetDatabase.CreateAsset((ScriptableObject)data, exportPath);
                        data.hideFlags = HideFlags.NotEditable;
                    }
                    data.Params.Clear();
					data.hideFlags = HideFlags.NotEditable;

					//Creat DataInit Script
					ReadExcel.CreatDataInitCs();

					// check sheet
                    var sheet = book.GetSheet(sheetName);
                    if (sheet == null)
                    {
                        Debug.LogError("[QuestData] sheet not found:" + sheetName);
                        continue;
                    }

                	// add infomation
                    for (int i=2; i<= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        ICell cell = null;
                        
                        var p = new Localization_ChineseSimplified.Param();
			
                        cell = row.GetCell(0); p.id = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(1); p.value = (cell == null ? "" : cell.StringCellValue);

                        data.Params.Add(p);
                    }
                    
                    // save scriptable object
                    ScriptableObject obj = AssetDatabase.LoadAssetAtPath(exportPath, typeof(ScriptableObject)) as ScriptableObject;
                    EditorUtility.SetDirty(obj);
                }
            

        }
    }
}
