using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Net;

public class HEScriptKeywordReplace : UnityEditor.AssetModificationProcessor {
    public static void OnWillCreateAsset(string path) {
        path = path.Replace(".meta", "");
        int index = path.LastIndexOf(".");
        string file = path.Substring(index);
        if (file != ".cs" && file != ".js" && file != ".boo")
            return;
        string fileExtension = file;

        index = Application.dataPath.LastIndexOf("Assets");
        path = Application.dataPath.Substring(0, index) + path;
        file = System.IO.File.ReadAllText(path);

        file = file.Replace("#CREATIONDATE#", System.DateTime.Now.ToString("yyyy/MM/dd"));
        file = file.Replace("#PROJECTNAME#", PlayerSettings.productName);
        file = file.Replace("#SMARTDEVELOPERS#", "蔡泽深");
        file = file.Replace("#FILEEXTENSION#", fileExtension);

        System.IO.File.WriteAllText(path, file);
        AssetDatabase.Refresh();
    }
}