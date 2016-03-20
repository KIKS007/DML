using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

///
/// !!! Machine generated code !!!
///
[CustomEditor(typeof(DML))]
public class DMLEditor : BaseExcelEditor<DML>
{	
    public override void OnEnable()
    {
        base.OnEnable();
        
        DML data = target as DML;
        
        databaseFields = ExposeProperties.GetProperties(data);
        
        foreach(DMLData e in data.dataArray)
        {
            dataFields = ExposeProperties.GetProperties(e);
            pInfoList.Add(dataFields);
        }
    }
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        //DrawDefaultInspector();
        if (GUI.changed)
        {
            pInfoList.Clear();
            
            DML data = target as DML;
            foreach(DMLData e in data.dataArray)
            {
                dataFields = ExposeProperties.GetProperties(e);
                pInfoList.Add(dataFields);
            }
            
            EditorUtility.SetDirty(target);
            Repaint();
        }
    }
    
    public override bool Load()
    {
        DML targetData = target as DML;

        string path = targetData.SheetName;
        if (!File.Exists(path))
            return false;

        string sheet = targetData.WorksheetName;

        ExcelQuery query = new ExcelQuery(path, sheet);
        if (query != null && query.IsValid())
        {
            targetData.dataArray = query.Deserialize<DMLData>().ToArray();
            EditorUtility.SetDirty(targetData);
            AssetDatabase.SaveAssets();
            return true;
        }
        else
            return false;
    }
}
