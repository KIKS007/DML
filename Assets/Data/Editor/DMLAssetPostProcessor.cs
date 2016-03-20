using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

///
/// !!! Machine generated code !!!
///
public class DMLAssetPostprocessor : AssetPostprocessor 
{
    private static readonly string filePath = "Assets/Data/DML Excel.xlsx";
    private static readonly string assetFilePath = "Assets/Data/DML.asset";
    private static readonly string sheetName = "DML";
    
    static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets) 
        {
            if (!filePath.Equals (asset))
                continue;
                
            DML data = (DML)AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(DML));
            if (data == null) {
                data = ScriptableObject.CreateInstance<DML> ();
                data.sheetName = filePath;
                data.worksheetName = sheetName;
                AssetDatabase.CreateAsset ((ScriptableObject)data, assetFilePath);
                //data.hideFlags = HideFlags.NotEditable;
            }
            
            //data.dataArray = new ExcelQuery(filePath, sheetName).Deserialize<DMLData>().ToArray();		

            //ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
            //EditorUtility.SetDirty (obj);

            ExcelQuery query = new ExcelQuery(filePath, sheetName);
            if (query != null && query.IsValid())
            {
                data.dataArray = query.Deserialize<DMLData>().ToArray();
                ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
                EditorUtility.SetDirty (obj);
            }
        }
    }
}
