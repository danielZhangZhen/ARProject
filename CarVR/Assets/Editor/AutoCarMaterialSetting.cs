using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEditor;

[Serializable]
public class MaterialName
{
    public string carBody = "cheshen",
        carBoli = "boli",
        carLamp = "dengboli",
        whiteBorder = "baibian",
        blackBorder = "heibian",
        chair = "zuoyi",
        floorMat = "jiaodian",
        carHud = "lungu",
        carTye = "luntai",
        centerCtrl = "zhongkong",
        carMetal = "jinshu",
        floorMatStage = "jiaodianhuabian";
    public MaterialName()
    {
    }
    private List<string> mList;
    public List<string> list
    {
        get
        {
            if (mList == null)
                mList = new List<string>() { carBody, carBoli, carLamp, whiteBorder, chair, floorMat, carHud, carTye, centerCtrl, carMetal, floorMatStage };
            return mList;
        }
    }

}
public class AutoCarMaterialSetting : MonoBehaviour
{
    public string assetPah = "Assets/Models/inCars/car/";            //图片资源路径
    public int textureNums = 5;
    public MaterialName mMaterialName = new MaterialName();

    #region 处理材质部分
    public GameObject allScriptsObj;
    public Cubemap mCubeMap;
    private Shader mLightMap, mCarBody, mCarGlasses;
    private Texture2D mMetalTexture2D;            //金属材质图片

    private Texture2D mLightOffA, mLightOffB, mGlassTex;
    #endregion

    [ContextMenu("AutoSetCarMaterials")]
    void InitSence()
    {
        InitTextureOrMaterialsObj();
        SetMaterialValue();
        InitWheel();
        AssetDatabase.Refresh();
        //EditorUtility.SetDirty();
    }

    //赋值所有图片和材质
    private void InitTextureOrMaterialsObj()
    {
        if (allScriptsObj == null)
            return;
        //车转换视角时隐藏部分
        ShopCarShowHide carShowHide = allScriptsObj.GetComponent<ShopCarShowHide>();
        carShowHide.allGameObj[0] = GameObject.Find("@qiege").gameObject;
        carShowHide.allGameObj[1] = GameObject.Find("@chemen_3").gameObject;
        carShowHide.allGameObj[2] = GameObject.Find("@chemen_4").gameObject;

        //隔热膜
        changeTransparency changeTrans = allScriptsObj.GetComponent<changeTransparency>();
        changeTrans.shareMat = GetObjByName<Material>(mMaterialName.carBoli, 1)[0];


        //轮毂lungu
        turnCarHud hud = allScriptsObj.GetComponent<turnCarHud>();
        hud.shareMat = GetObjByName<Material>(mMaterialName.carHud, 1)[0];
        Texture2D[] objHuds = GetObjByName<Texture2D>(mMaterialName.carHud, textureNums);
        hud.SetTexture(objHuds);

        //脚垫 jiaodian
        turnFloorMat floorMat = allScriptsObj.GetComponent<turnFloorMat>();
        floorMat.shareMat = GetObjByName<Material>(mMaterialName.floorMat, 1)[0];
        Texture2D[] objFloors = GetObjByName<Texture2D>(mMaterialName.floorMat, textureNums);
        floorMat.SetTexture(objFloors);

        //座椅 zuoyi
        turnCarChair chair = allScriptsObj.GetComponent<turnCarChair>();
        chair.shareMat = GetObjByName<Material>(mMaterialName.chair, 1)[0];
        Texture2D[] objChairs = GetObjByName<Texture2D>(mMaterialName.chair, textureNums);
        chair.SetTexture(objChairs);

        //车身材质
        ColorTool color = allScriptsObj.GetComponent<ColorTool>();
        color.shareMat = GetObjByName<Material>(mMaterialName.carBody, 1)[0];
    }

    private void InitWheel()
    {
        GameObject chelunObj = this.transform.FindChild("cheLun").gameObject;
        Animation cheLunAni = chelunObj.GeCompontentForMiss<Animation>();
        cheLunAni.playAutomatically = false;
        AnimationClip CheLunAniClip = (AnimationClip)Resources.Load("NoLightMap/CarWheel");
        cheLunAni.clip = CheLunAniClip;
        cheLunAni.AddClip(CheLunAniClip, "CarWheel");
    }
    //设置所有材质球应有的效果
    private void SetMaterialValue()
    {
        GetMaterials();
        otherMat();
        MetalMat();
        CarBodyMat();
        CarBorder();
        CarFGlassMat();
        CarGlassMat();
    }
    private void GetMaterials()
    {
        mLightMap = Shader.Find("DQZ/LightMap/UnlitA");
        mCarBody = Shader.Find("Car/CarPain2 Bump");
        mCarGlasses = Shader.Find("Car/CarGlass2");
        mLightOffA = (Texture2D)Resources.Load("NoLightMap/lightMapNO");
        mLightOffB = (Texture2D)Resources.Load("NoLightMap/SparkleNoiseMap23");
        mGlassTex = (Texture2D)Resources.Load("NoLightMap/SparkleNoiseMap");
        mMetalTexture2D = (Texture2D)Resources.Load("NoLightMap/car_jinshu");
    }

    private void otherMat()//其他材质
    {
        //座椅，脚垫,轮胎  
        List<Material> listA = new List<Material>();
        listA.Add(GetObjByName<Material>(mMaterialName.chair, 1)[0]);
        listA.Add(GetObjByName<Material>(mMaterialName.floorMat, 1)[0]);
        listA.Add(GetObjByName<Material>(mMaterialName.carTye, 1)[0]);
        for (int i = 0; i < listA.Count; i++)
        {
            listA[i].shader = mLightMap;
            listA[i].SetColor("_Color", new Color(.6f, .6f, .6f, 1f));
            listA[i].SetFloat("_OutLineTilingValue", .19f);
            listA[i].SetFloat("_LightValue", 2.2f);
            listA[i].SetFloat("_CubeValue", 0f);
            listA[i].SetTexture("_Lightmap", mLightOffB);
        }
        //轮毂,中控,脚垫边
        List<Material> listB = new List<Material>();
        listB.Add(GetObjByName<Material>(mMaterialName.carHud, 1)[0]);
        listB.Add(GetObjByName<Material>(mMaterialName.centerCtrl, 1)[0]);
        listB.Add(GetObjByName<Material>(mMaterialName.floorMatStage, 1)[0]);
        for (int i = 0; i < listB.Count; i++)
        {
            listB[i].shader = mLightMap;
            listB[i].SetColor("_Color", new Color(.6f, .6f, .6f, 1f));
            listB[i].SetFloat("_OutLineTilingValue", .01f);
            listB[i].SetFloat("_LightValue", 2.0f);
            listB[i].SetFloat("_CubeValue", 0f);
            listB[i].SetTexture("_Lightmap", mLightOffA);
        }
    }
    //汽身漆
    private void CarBodyMat()
    {
        Material[] mats = GetObjByName<Material>(mMaterialName.carBody, 1);
        if (mats.Length > 0 && mats[0] != null)
        {
            Material carBody = mats[0];
            Texture2D myT = carBody.mainTexture as Texture2D;
            carBody.shader = mCarBody;
            carBody.SetTexture("_MainTex", myT);

            carBody.SetColor("_Color", new Color(0, 0, 0, 1.000f));
            carBody.SetColor("_SpecColor", new Color(0.235f, 0.235f, 0.235f, 1.000f));
            carBody.SetColor("_ReflectionColor", new Color(0.322f, 0.318f, 0.338f, 1.000f));

            carBody.SetFloat("_Shininess", 2);
            carBody.SetFloat("_BumpDens", 23.5f);
            carBody.SetFloat("_FresnelScale", .97f);
            carBody.SetFloat("_FresnelPower", 1.68f);
            carBody.SetTexture("_Cube", mCubeMap);
        }
    }
    //金属材质
    private void MetalMat()
    {
        Material[] mats = GetObjByName<Material>(mMaterialName.carMetal, 1);
        if (mats.Length > 0 && mats[0] != null)
        {
            Material matMetal = mats[0];
            matMetal.shader = mLightMap;
            matMetal.SetColor("_Color", new Color(.586f, .586f, .586f, 1f));
            matMetal.SetColor("CUBEColor", new Color(.36f, .36f, .36f, 1f));
            matMetal.SetFloat("_CubeValue", .3f);
            matMetal.SetTexture("_Reflections", mCubeMap);
            matMetal.SetTexture("_Lightmap", mLightOffA);
        }
    }
    //发光边
    private void CarBorder()
    {
        Material[] mats = GetObjByName<Material>(mMaterialName.whiteBorder, 1);
        if (mats.Length > 0 && mats[0] != null)
        {
            Material borderMat = mats[0];
            borderMat.shader = mLightMap;
            borderMat.SetColor("_Color", new Color(.8f, .8f, .8f, 1f));
            borderMat.SetFloat("_CubeValue", .9f);
            borderMat.SetTexture("_MainTex", mMetalTexture2D);
            borderMat.SetTexture("_Reflections", mCubeMap);
        }

        //Material[] matsBlack = GetObjByName<Material>(mMaterialName.blackBorder, 1);
        //if (matsBlack.Length > 0 && matsBlack[0] != null)
        //{
        //    Material borderMat = matsBlack[0];
        //    borderMat.shader = mLightMap;
        //    borderMat.SetColor("_Color", new Color(0.1f, 0.1f, 0.1f, 1f));
        //    borderMat.SetFloat("_CubeValue", .9f);
        //    borderMat.SetTexture("_MainTex", mMetalTexture2D);
        //    borderMat.SetTexture("_Reflections", mCubeMap);
        //}

    }

    private void CarFGlassMat()//车灯玻璃
    {
        Material[] mats = GetObjByName<Material>(mMaterialName.carLamp, 1);
        if (mats.Length > 0 && mats[0] != null)
        {
            Material lampMat = mats[0];
            lampMat.shader = mCarGlasses;
            lampMat.SetColor("_Color", new Color(0.000f, 0.252f, 0.588f, 0.422f));
            lampMat.SetColor("_SpecColor", new Color(1f, 1f, 1f, 1f));
            lampMat.SetColor("_AmbientColor", new Color(0.721f, 0.721f, 0.721f, 0.434f));
            lampMat.SetColor("_AmbientColor2", new Color(0.000f, 0.251f, 0.588f, 0.441f));
            lampMat.SetColor("_ReflectionColor", new Color(0.309f, 0.309f, 0.309f, 1.000f));

            lampMat.SetFloat("_Shininess", 2);
            lampMat.SetFloat("_Reflect", .112f);
            lampMat.SetFloat("_FresnelScale", 1.94f);
            lampMat.SetFloat("_FresnelPower", 2.32f);
            lampMat.SetFloat("_MetalicScale", .84615f);
            lampMat.SetFloat("_MetalicPower", 4.23f);
            lampMat.SetFloat("_CandyScale", 3.7692f);
            lampMat.SetFloat("_CandyPower", 17.3f);
            lampMat.SetTexture("_MainTex", mGlassTex);
            lampMat.SetTextureScale("_MainTex", new Vector2(10f, 10f));
            lampMat.SetTexture("_Cube", mCubeMap);
        }

    }
    //车玻璃
    private void CarGlassMat()
    {
        Material[] mats = GetObjByName<Material>(mMaterialName.carBoli, 1);
        if (mats.Length > 0 && mats[0] != null)
        {
            Material carWindowBoli = mats[0];
            carWindowBoli.shader = mCarGlasses;
            carWindowBoli.SetColor("_Color", new Color(0f, 0.250f, 0.586f, 0.750f));
            carWindowBoli.SetColor("_SpecColor", new Color(1f, 1f, 1f, 1f));
            carWindowBoli.SetColor("_AmbientColor", new Color(0.721f, 0.721f, 0.721f, 0.434f));
            carWindowBoli.SetColor("_AmbientColor2", new Color(0.000f, 0.251f, 0.588f, 0.441f));
            carWindowBoli.SetColor("_ReflectionColor", new Color(0.309f, 0.309f, 0.309f, 1.000f));

            carWindowBoli.SetFloat("_Shininess", 2);
            carWindowBoli.SetFloat("_Reflect", .112f);
            carWindowBoli.SetFloat("_FresnelScale", 1.94f);
            carWindowBoli.SetFloat("_FresnelPower", 1.06f);
            carWindowBoli.SetFloat("_MetalicScale", .84615f);
            carWindowBoli.SetFloat("_MetalicPower", 4.23f);
            carWindowBoli.SetFloat("_CandyScale", 3.7692f);
            carWindowBoli.SetFloat("_CandyPower", 17.3f);
            carWindowBoli.SetTexture("_MainTex", mGlassTex);
            carWindowBoli.SetTextureScale("_MainTex", new Vector2(10f, 10f));
            carWindowBoli.SetTexture("_Cube", mCubeMap);
        }
    }
    //根据资源名字加载出相应资源
    private T[] GetObjByName<T>(string objName, int count) where T : UnityEngine.Object
    {
        T[] obj = new T[count];

        string suffixName = string.Empty;
        string carTexturePath = string.Empty;
        string carName = gameObject.name;


        suffixName = typeof(T) == typeof(Texture2D) ? ".jpg" : ".mat";
        string carPath = typeof(T) == typeof(Texture2D) ? carName.Substring(carName.LastIndexOf('_') + 1) + "/" : carName.Substring(carName.LastIndexOf('_') + 1) + "/Materials/";

        for (int i = 0; i < count; i++)
        {
            string strIndex = suffixName.Contains(".jpg") ? i.ToString() : string.Empty;
            carTexturePath = assetPah + carPath + carName + "_" + objName + strIndex + suffixName;
            T t = AssetDatabase.LoadAssetAtPath(carTexturePath, typeof(T)) as T;
            if (t == null)
                break;
            obj[i] = t;
        }
        return obj;
    }
}
