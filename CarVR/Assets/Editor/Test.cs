using UnityEngine;
using System.Collections;
using UnityEditor;

public class Test : Editor
{

    [MenuItem("Custom Editor/Create AssetBunldes Main For Android")]
    static void CreateAssetBunldesMainForAndroid()
    {
        //获取在Project视图中选择的所有游戏对象
        Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        //遍历所有的游戏对象
        foreach (Object obj in SelectedAsset)
        {
            //本地测试：建议最后将Assetbundle放在StreamingAssets文件夹下，如果没有就创建一个，因为移动平台下只能读取这个路径
            //StreamingAssets是只读路径，不能写入
            //服务器下载：就不需要放在这里，服务器上客户端用www类进行下载。
            string targetPath = Application.dataPath + "/StreamingAssets/" + obj.name + "Android" + ".assetbundle";
            if (BuildPipeline.BuildAssetBundle(obj, null, targetPath, BuildAssetBundleOptions.None, BuildTarget.Android))
            {
                Debug.Log(obj.name + "资源打包成功");
            }
            else
            {
                Debug.Log(obj.name + "资源打包失败");
            }
        }
        //刷新编辑器
        AssetDatabase.Refresh();

    }
    [MenuItem("Custom Editor/Create AssetBunldes Main For iPhone")]
    static void CreateAssetBunldesMainForiPhone()
    {
        //获取在Project视图中选择的所有游戏对象
        Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        //遍历所有的游戏对象
        foreach (Object obj in SelectedAsset)
        {
            string targetPath = Application.dataPath + "/StreamingAssets/" + obj.name + "iPhone" + ".assetbundle";
            if (BuildPipeline.BuildAssetBundle(obj, null, targetPath, BuildAssetBundleOptions.None, BuildTarget.iOS))
            {
                Debug.Log(obj.name + "资源打包成功");
            }
            else
            {
                Debug.Log(obj.name + "资源打包失败");
            }
        }
        //刷新编辑器
        AssetDatabase.Refresh();

    }

    [MenuItem("Custom Editor/Create AssetBunldes ALL For Android")]
    static void CreateAssetBunldesALLForAndroid()
    {

        Caching.CleanCache();
        string Path = Application.dataPath + "/StreamingAssets/ALLAndroid.assetbundle";
        Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        foreach (Object obj in SelectedAsset)
        {
            Debug.Log("Create AssetBunldes name :" + obj);
        }

        //这里注意第二个参数就行
        if (BuildPipeline.BuildAssetBundle(null, SelectedAsset, Path, BuildAssetBundleOptions.None, BuildTarget.Android))
        {
            AssetDatabase.Refresh();
        }
        else
        {

        }
    }
    [MenuItem("Custom Editor/Create AssetBunldes ALL For iPhone")]
    static void CreateAssetBunldesALLForiPhone()
    {

        Caching.CleanCache();


        string Path = Application.dataPath + "/StreamingAssets/ALLiPhone.assetbundle";


        Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        foreach (Object obj in SelectedAsset)
        {
            Debug.Log("Create AssetBunldes name :" + obj);
        }

        //这里注意第二个参数就行
        if (BuildPipeline.BuildAssetBundle(null, SelectedAsset, Path, BuildAssetBundleOptions.None, BuildTarget.iOS))
        {
            AssetDatabase.Refresh();
        }
        else
        {

        }
    }
    [MenuItem("Custom Editor/Create Scene For Android")]
    static void CreateSceneALLForAndroid()
    {
        Caching.CleanCache();
        Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        foreach (Object obj in SelectedAsset)
        {
            string Path = Application.dataPath + "/StreamingAssets/" + obj.name + "Android" + ".unity3d";
            string[] levels = { @"Assets/Scenes/ExportedScene/" + obj.name + ".unity" };
            BuildPipeline.BuildPlayer(levels, Path, BuildTarget.Android, BuildOptions.BuildAdditionalStreamedScenes);
            Debug.Log("Craete Scene" + Path + "Complete!!");
        }
        AssetDatabase.Refresh();
    }

    [MenuItem("Custom Editor/Create Scene For iPhone")]
    static void CreateSceneALLForiPhone()
    {
        Caching.CleanCache();
        Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        foreach (Object obj in SelectedAsset)
        {
            string Path = Application.dataPath + "/StreamingAssets/" + obj.name + "iPhone" + ".unity3d";
			string[] levels = { @"Assets/Scenes/ExportedScene/" + obj.name + ".unity" };

//			string Path = Application.dataPath +obj.name+"iPhone"+".unity3d";
//			string  []levels = {"Assets/Scenes/ExportedScene/4sShop_AllCars_Exported.unity"};
            BuildPipeline.BuildPlayer(levels, Path, BuildTarget.iOS, BuildOptions.BuildAdditionalStreamedScenes);
            Debug.Log("Craete Scene" + Path + "Complete!!");
        }
        AssetDatabase.Refresh();
    }

}
