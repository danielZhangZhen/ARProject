using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using UnityEngine.UI;

public class MakeMyAtlas : EditorWindow
{
    static public string getPath = "/Models/NGUI/inDoorCar/MiddleScreenTex/New Folder";
    static public string setPath = "/Resources/UI/Sprite/texture";
    [MenuItem("MyMenu/AtlasMaker")]
    static private void MakeAtlas()
    {
        string spriteDir = Application.dataPath + setPath;
        Debug.Log(spriteDir);

        if (!Directory.Exists(spriteDir))
        {
            Directory.CreateDirectory(spriteDir);
        }
        Debug.Log("222");
        DirectoryInfo rootDirInfo = new DirectoryInfo(Application.dataPath + getPath);
        Debug.Log(rootDirInfo);
        foreach (FileInfo pngFile in rootDirInfo.GetFiles("*.png", SearchOption.AllDirectories))
        {
            Debug.Log("333");
            string allPath = pngFile.FullName; //文件在文件夹中的位置
            //因为项目名称也含有Assets字符，所以需要先去除项目名称上的字符
            string assetPath = allPath.Substring(allPath.IndexOf("Assets"));
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
            GameObject go = new GameObject(sprite.name);
            go.AddComponent<SpriteRenderer>().sprite = sprite;
            allPath = spriteDir + "/" + sprite.name + ".prefab";
            string prefabPath = allPath.Substring(allPath.IndexOf("Assets"));
            PrefabUtility.CreatePrefab(prefabPath, go);//创建预设
            GameObject.DestroyImmediate(go);
        }
    }
}

