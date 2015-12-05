using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

public class TypeAffinityData_importer : AssetPostprocessor
{
    private static readonly string filePath = "Assets/ApplicationRoot/Resources/ExcelData/TypeAffinityData/TypeAffinityData.xls";
    private static readonly string[] sheetNames = { "TypeAffinityData", };
    
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
                    var exportPath = "Assets/ApplicationRoot/Resources/ExcelData/TypeAffinityData/" + sheetName + ".asset";
                    
                    // check scriptable object
                    var data = (Entity_TypeAffinityData)AssetDatabase.LoadAssetAtPath(exportPath, typeof(Entity_TypeAffinityData));
                    if (data == null)
                    {
                        data = ScriptableObject.CreateInstance<Entity_TypeAffinityData>();
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
                        
                        var p = new Entity_TypeAffinityData.Param();
			
					cell = row.GetCell(0); p.ID = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.name = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p.normal = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(3); p.fire = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.water = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(5); p.electric = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(6); p.grass = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(7); p.ice = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(8); p.fighting = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(9); p.poison = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(10); p.ground = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(11); p.flying = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(12); p.psychic = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(13); p.bug = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(14); p.rock = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(15); p.ghost = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(16); p.dragon = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(17); p.dark = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(18); p.steel = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(19); p.fairlie = (cell == null ? 0.0 : cell.NumericCellValue);

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
