//*************************************************
//** 姓名 ：张振
//** 日期 ：2015年1月13日
//** 类作用： 自动设置选中图片的纹理格式
//*************************************************
using UnityEngine;
using System.Collections;
using UnityEditor;

public class TextureAuto : ScriptableObject
{



    //车模和场景的贴图
    [MenuItem("Custom Editor/Auto SetTextureFormat/CarAnd3D")]
    static void AutoSetFormatForCarAndBuilding()
    {
        Object[] SelectedAssets = Selection.GetFiltered(typeof(Texture), SelectionMode.DeepAssets);

        foreach (Texture item in SelectedAssets)
        {
            string path = AssetDatabase.GetAssetPath(item);
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
            textureImporter.textureType = TextureImporterType.Advanced;
            textureImporter.npotScale = TextureImporterNPOTScale.ToNearest;
            textureImporter.SetPlatformTextureSettings("iPhone", 1024, TextureImporterFormat.PVRTC_RGBA4);
            textureImporter.SetPlatformTextureSettings("Android", 1024, TextureImporterFormat.ETC2_RGBA8);
            textureImporter.SetPlatformTextureSettings("default", 1024, TextureImporterFormat.DXT5);        //不起作用
            textureImporter.wrapMode = TextureWrapMode.Clamp;
            textureImporter.filterMode = FilterMode.Bilinear;
            textureImporter.mipmapEnabled = true;
       
        }
        AssetDatabase.Refresh();
    }
    [MenuItem("Custom Editor/Auto SetTextureFormat/取消多重采样图")]
    static void AutoSetmipmapEnabled()
    {
        Object[] SelectedAssets = Selection.GetFiltered(typeof(Texture), SelectionMode.DeepAssets);
        foreach (Texture item in SelectedAssets)
        {
            string path = AssetDatabase.GetAssetPath(item);
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
            textureImporter.mipmapEnabled = false;
            AssetDatabase.ImportAsset(path);
        }
        AssetDatabase.Refresh();
    }

    [MenuItem("Custom Editor/Auto SetTextureFormat/UI")]
    static void AutoSetFormatForUISprite()
    {
        Object[] SelectedAssets = Selection.GetFiltered(typeof(Texture), SelectionMode.DeepAssets);

        foreach (Texture item in SelectedAssets)
        {
            string path = AssetDatabase.GetAssetPath(item);
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
            textureImporter.textureType = TextureImporterType.Sprite;
     
            textureImporter.SetPlatformTextureSettings("iPhone", 1024, TextureImporterFormat.AutomaticCompressed);
            textureImporter.SetPlatformTextureSettings("Android", 1024, TextureImporterFormat.AutomaticCompressed);
            textureImporter.SetPlatformTextureSettings("default", 1024, TextureImporterFormat.DXT5);        //不起作用
            textureImporter.spriteImportMode = SpriteImportMode.Single;
            textureImporter.spritePackingTag = "UI";
            textureImporter.alphaIsTransparency = true;
            textureImporter.wrapMode = TextureWrapMode.Clamp;
            textureImporter.filterMode = FilterMode.Bilinear;
            textureImporter.mipmapEnabled = false;
            AssetDatabase.ImportAsset(path);
        }
        AssetDatabase.Refresh();
    }
}
