using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

public class PersonalityData_importer : AssetPostprocessor
{
    private static readonly string filePath = "Assets/ApplicationRoot/Resources/ExcelData/PersonalityData/PersonalityData.xls";
    private static readonly string[] sheetNames = { "PersonalityData", };
    
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets)
        {
            if (!filePath.Equals(asset))
                continue;

            using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read))
            {
                var book = new HSSFWorkbook(stream);

                foreach (string sheetName in sheetNames)
                {
                    var exportPath = "Assets/ApplicationRoot/Resources/ExcelData/PersonalityData/" + sheetName + ".asset";
                    
                    // check scriptable object
                    var data = (Entity_PersonalityData)AssetDatabase.LoadAssetAtPath(exportPath, typeof(Entity_PersonalityData));
                    if (data == null)
                    {
                        data = ScriptableObject.CreateInstance<Entity_PersonalityData>();
                        AssetDatabase.CreateAsset((ScriptableObject)data, exportPath);
                        data.hideFlags = HideFlags.NotEditable;
                    }
                    data.param.Clear();

					// check sheet
                    var sheet = book.GetSheet(sheetName);
                    if (sheet == null)
                    {
                        Debug.LogError("[QuestData] sheet not found:" + sheetName);
                        continue;
                    }

                	// add infomation
                    for (int i=1; i<= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        ICell cell = null;
                        
                        var p = new Entity_PersonalityData.Param();
			
					cell = row.GetCell(0); p.ID = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.name = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p.atk = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(3); p.def = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.s_atk = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(5); p.s_def = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(6); p.spd = (cell == null ? 0.0 : cell.NumericCellValue);

                        data.param.Add(p);
                    }
                    
                    // save scriptable object
                    ScriptableObject obj = AssetDatabase.LoadAssetAtPath(exportPath, typeof(ScriptableObject)) as ScriptableObject;
                    EditorUtility.SetDirty(obj);
                }
            }

        }
    }
}
