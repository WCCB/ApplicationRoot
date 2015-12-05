using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

public class PokemonData_importer : AssetPostprocessor
{
    private static readonly string filePath = "Assets/ApplicationRoot/Resources/ExcelData/PokemonData/PokemonData.xls";
    private static readonly string[] sheetNames = { "PokemonData", };
    
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
                    var exportPath = "Assets/ApplicationRoot/Resources/ExcelData/PokemonData/" + sheetName + ".asset";
                    
                    // check scriptable object
                    var data = (Entity_PokemonData)AssetDatabase.LoadAssetAtPath(exportPath, typeof(Entity_PokemonData));
                    if (data == null)
                    {
                        data = ScriptableObject.CreateInstance<Entity_PokemonData>();
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
                        
                        var p = new Entity_PokemonData.Param();
			
					cell = row.GetCell(0); p.ID = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.name = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p.characteristic_1 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(3); p.characteristic_2 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(4); p.characteristic_3 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(5); p.type_1 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(6); p.type_2 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(7); p.hp = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(8); p.atk = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(9); p.def = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(10); p.s_atk = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(11); p.s_def = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(12); p.spd = (int)(cell == null ? 0 : cell.NumericCellValue);

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
