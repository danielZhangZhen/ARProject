using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;
public class car4STools : MonoBehaviour
{
    #region 得到材质部分
    //就是前车灯玻璃,车玻璃，车身，车边高光,座椅，脚垫，其他 让其运行一次
    private static bool carBodyB = true, carWindowBoliB = true, frontBoliB = true, blackLineB = true, chairB = true, floorMatsB = true, carHudsB = true, carTyeB = true, centerCtrlB = true, frontBoxB = true, floorMatStage = true;
    [SerializeField]
    private string carBodyS = "cheshen", carWindowBoliS = "boli", frontBoliS = "dengboli", blackLineS = "baibian", chairS = "zuoyi", floorMatsS = "jiaodian_c1", carHudsS = "lungu", carTyeS = "luntai", centerCtrlS = "zhongkong", frontBoxs = "jinshu", floorMatStageS = "jiaodianhuabian";
    private MeshRenderer[] carAllMeshR;
    private Material[] allMats;
    private Shader myS;
    private string[] allMatsName, _allMatsName, _realS;//总材质名，选出不重复的材质名,从车身开始带的
    private int allNum, realNum; //总长度68，真正要运算长度16
    private int[] prossId; //要运行的id号
    #endregion
    #region 处理材质部分
    [SerializeField]
    private GameObject allScriptsObj;
    private Shader myLightMap, myCarBody, myCarGlasses;
    private Cubemap myCubeMapA = null;
    [SerializeField]
    private int cubeMapIndex = 0;
    private Texture2D noLightA, noLightB, glassTex;
    #endregion

    [ContextMenu("Car4s")]
    void Car4s()//总程序
    {
        Init();
        doMatsFuntion();
        doMats();
        AddScriptsToObj();
    }
    [ContextMenu("closeShadow")]
    void closeShadow()
    {
        doMatsFuntion();
    }
    private void Init()//初始化最常用东西
    {
        myLightMap = Shader.Find("DQZ/LightMap/UnlitA");
        myCarBody = Shader.Find("Car/CarPain2 Bump");
        myCarGlasses = Shader.Find("Car/CarGlass2");
        if (cubeMapIndex == 0) { myCubeMapA = (Cubemap)Resources.Load("NoLightMap/car01House"); }
        else if (cubeMapIndex == 1) { myCubeMapA = (Cubemap)Resources.Load("NoLightMap/car02House"); }
        else { myCubeMapA = (Cubemap)Resources.Load("NoLightMap/car01House"); }
        noLightA = (Texture2D)Resources.Load("NoLightMap/lightMapNO");
        noLightB = (Texture2D)Resources.Load("NoLightMap/SparkleNoiseMap23");
        glassTex = (Texture2D)Resources.Load("NoLightMap/SparkleNoiseMap");
    }
    private void doMatsFuntion()//得到所有材质
    {
        this.GetComponent<Animation>().playAutomatically = false;
        carAllMeshR = this.GetComponentsInChildren<MeshRenderer>();
        allNum = carAllMeshR.Length;
        allMats = new Material[allNum];
        allMatsName = new string[allNum];

        for (int i = 0; i < allNum; i++)
        {
            carAllMeshR[i].receiveShadows = false;
            carAllMeshR[i].useLightProbes = false;
            carAllMeshR[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            allMats[i] = carAllMeshR[i].sharedMaterial;//得到所有材质,前面是去阴影接受阴影
            allMatsName[i] = allMats[i].name;
            //Debug.Log(allMatsName[i].ToString());
        }
        _allMatsName = DelRepeatData(allMatsName);//合并其名称
        realNum = _allMatsName.Length;
        _realS = new string[realNum];
        _realS = realMats();
        prossId = new int[realNum];
        prossId = findIndex();//先得到一个实际要修改的材质,再得到实际用到的id号
        // Debug.Log(prossId[5].ToString());
    }
    private string[] realMats()//重新带列
    {
        string[] real_Mats = new string[realNum];
        for (int j = 0; j < realNum; j++)
        {
            if (_allMatsName[j].Contains(carBodyS)) { real_Mats[0] = _allMatsName[j].ToString(); }    //汽车是0
            else if (_allMatsName[j].Contains(carWindowBoliS)) { real_Mats[1] = _allMatsName[j].ToString(); }//车玻璃1
            else if (_allMatsName[j].Contains(frontBoliS)) { real_Mats[2] = _allMatsName[j].ToString(); }//前车灯玻璃2
            else if (_allMatsName[j].Contains(blackLineS)) { real_Mats[3] = _allMatsName[j].ToString(); }//外发光边3
            else if (_allMatsName[j].Contains(chairS)) { real_Mats[4] = _allMatsName[j].ToString(); }//座椅4
            else if (_allMatsName[j].Contains(floorMatsS)) { real_Mats[5] = _allMatsName[j].ToString(); }//脚垫5
            else if (_allMatsName[j].Contains(carHudsS)) { real_Mats[6] = _allMatsName[j].ToString(); }//轮毂6
            else if (_allMatsName[j].Contains(carTyeS)) { real_Mats[7] = _allMatsName[j].ToString(); }//轮胎7
            else if (_allMatsName[j].Contains(centerCtrlS)) { real_Mats[8] = _allMatsName[j].ToString(); }//中控8
            else if (_allMatsName[j].Contains(frontBoxs)) { real_Mats[9] = _allMatsName[j].ToString(); }//前包箱9
            else if (_allMatsName[j].Contains(floorMatStageS)) { real_Mats[10] = _allMatsName[j].ToString(); }//脚垫边9
            for (int k = 11; k < realNum; k++)//其余的也要放入
            {
                real_Mats[k] = _allMatsName[k].ToString();
            }
        }
        return real_Mats;
    }
    private int[] findIndex()// 得到最重要的id号
    {
        int[] myinn = new int[realNum];
        for (int i = 0; i < allNum; i++)
        {
            if (allMatsName[i] == _realS[0]) { if (carBodyB) { myinn[0] = i; carBodyB = false; } }
            if (allMatsName[i] == _realS[1]) { if (carWindowBoliB) { myinn[1] = i; carWindowBoliB = false; } }
            if (allMatsName[i] == _realS[2]) { if (frontBoliB) { myinn[2] = i; frontBoliB = false; } }
            if (allMatsName[i] == _realS[3]) { if (blackLineB) { myinn[3] = i; blackLineB = false; } }
            if (allMatsName[i] == _realS[4]) { if (chairB) { myinn[4] = i; chairB = false; } }
            if (allMatsName[i] == _realS[5]) { if (floorMatsB) { myinn[5] = i; floorMatsB = false; } }
            if (allMatsName[i] == _realS[6]) { if (carHudsB) { myinn[6] = i; carHudsB = false; } }
            if (allMatsName[i] == _realS[7]) { if (carTyeB) { myinn[7] = i; carTyeB = false; } }
            if (allMatsName[i] == _realS[8]) { if (centerCtrlB) { myinn[8] = i; centerCtrlB = false; } }
            if (allMatsName[i] == _realS[9]) { if (frontBoxB) { myinn[9] = i; frontBoxB = false; } }
            if (allMatsName[i] == _realS[10]) { if (floorMatStage) { myinn[10] = i; floorMatStage = false; } }
        }
        return myinn;

    }
    private string[] DelRepeatData(string[] a)//合并名称
    {
        return a.GroupBy(p => p).Select(p => p.Key).ToArray();
    }

    private void AddScriptsToObj()//脚本自动添加
    {
        allScriptsObj.GetComponent<ColorTool>().shareMat = allMats[prossId[0]];
        allScriptsObj.GetComponent<changeTransparency>().shareMat = allMats[prossId[1]];
        allScriptsObj.GetComponent<turnCarChair>().shareMat = allMats[prossId[4]];
        allScriptsObj.GetComponent<turnCarHud>().shareMat = allMats[prossId[6]];
        allScriptsObj.GetComponent<turnFloorMat>().shareMat = allMats[prossId[5]];

        thisCarParts();
    }
    private void thisCarParts()//车本身添加
    {
        GameObject cheMen3 = this.transform.FindChild("@chemen_3").gameObject;
        GameObject cheMen4 = this.transform.FindChild("@chemen_4").gameObject;
        GameObject cheDing = this.transform.FindChild("@qiege").gameObject;

        allScriptsObj.GetComponent<ShopCarShowHide>().allGameObj[0] = cheDing;
        allScriptsObj.GetComponent<ShopCarShowHide>().allGameObj[1] = cheMen3;
        allScriptsObj.GetComponent<ShopCarShowHide>().allGameObj[2] = cheMen4;

        GameObject chelunObj = this.transform.FindChild("cheLun").gameObject;
        Animation cheLunAni = chelunObj.GeCompontentForMiss<Animation>();
        cheLunAni.playAutomatically = false;
        AnimationClip CheLunAniClip = (AnimationClip)Resources.Load("NoLightMap/CarWheel");
        cheLunAni.clip = CheLunAniClip;
        cheLunAni.AddClip(CheLunAniClip, "CarWheel");

        // GameObject cheLunRuning = GameObject.Find("carLunRunning");
        // cheLunRuning.GetComponent<playCheLun>().CheLun = chelunObj;
    }
    void doMats()//所有材质控作如下
    {
        CarBodyMat(allMats[prossId[0]]);
        CarFGlassMat(allMats[prossId[2]]);
        CarGlassMat(allMats[prossId[1]]);
        blackLiness(allMats[prossId[3]]);
        forwordMat(allMats[prossId[9]]);//前控

        Material[] myA_mats = new Material[2] { allMats[prossId[5]], allMats[prossId[7]] };//座椅，脚垫,轮胎
        Material[] myB_mats = new Material[4] { allMats[prossId[4]], allMats[prossId[6]], allMats[prossId[8]], allMats[prossId[10]] };//轮毂,中控,脚垫边
        otherMat(myA_mats, myB_mats);//其他常用
    }
    private void otherMat(Material[] otherMatsA, Material[] otherMatsB)//其他材质
    {
        for (int i = 0; i < otherMatsA.Length; i++)
        {
            otherMatsA[i].shader = myLightMap;
            otherMatsA[i].SetColor("_Color", new Color(.6f, .6f, .6f, 1f));
            otherMatsA[i].SetFloat("_OutLineTilingValue", .19f);
            otherMatsA[i].SetFloat("_LightValue", 2.2f);
            otherMatsA[i].SetFloat("_CubeValue", 0f);
            otherMatsA[i].SetTexture("_Lightmap", noLightB);
        }

        for (int i = 0; i < otherMatsB.Length; i++)
        {
            otherMatsB[i].shader = myLightMap;
            otherMatsB[i].SetColor("_Color", new Color(.6f, .6f, .6f, 1f));
            otherMatsB[i].SetFloat("_OutLineTilingValue", .01f);
            otherMatsB[i].SetFloat("_LightValue", 2.0f);
            otherMatsB[i].SetFloat("_CubeValue", 0f);
            otherMatsB[i].SetTexture("_Lightmap", noLightA);
        }
    }

    private void CarBodyMat(Material carBody)//汽车漆
    {

        Texture2D myT = carBody.mainTexture as Texture2D;
        carBody.shader = myCarBody;
        carBody.SetTexture("_MainTex", myT);

        carBody.SetColor("_Color", new Color(0.625f, 0.336f, 0.000f, 1.000f));
        carBody.SetColor("_SpecColor", new Color(0.235f, 0.235f, 0.235f, 1.000f));
        carBody.SetColor("_ReflectionColor", new Color(0.322f, 0.318f, 0.338f, 1.000f));

        carBody.SetFloat("_Shininess", 2);
        carBody.SetFloat("_BumpDens", 23.5f);
        carBody.SetFloat("_FresnelScale", .97f);
        carBody.SetFloat("_FresnelPower", 1.68f);
        carBody.SetTexture("_Cube", myCubeMapA);

    }
    private void forwordMat(Material forwordM)
    {
        forwordM.shader = myLightMap;
        forwordM.SetColor("_Color", new Color(.586f, .586f, .586f, 1f));
        forwordM.SetColor("CUBEColor", new Color(.36f, .36f, .36f, 1f));
        forwordM.SetFloat("_CubeValue", .3f);
        forwordM.SetTexture("_Reflections", myCubeMapA);
        forwordM.SetTexture("_Lightmap", noLightA);
    }
    private void blackLiness(Material blackLine)//发光边
    {
        blackLine.shader = myLightMap;
        blackLine.SetColor("_Color", new Color(.8f, .8f, .8f, 1f));
        blackLine.SetFloat("_CubeValue", .9f);
        blackLine.SetTexture("_Reflections", myCubeMapA);
    }

    private void CarFGlassMat(Material frontBoli)//车前玻璃
    {
        frontBoli.shader = myCarGlasses;
        frontBoli.SetColor("_Color", new Color(0.000f, 0.252f, 0.588f, 0.422f));
        frontBoli.SetColor("_SpecColor", new Color(1f, 1f, 1f, 1f));
        frontBoli.SetColor("_AmbientColor", new Color(0.721f, 0.721f, 0.721f, 0.434f));
        frontBoli.SetColor("_AmbientColor2", new Color(0.000f, 0.251f, 0.588f, 0.441f));
        frontBoli.SetColor("_ReflectionColor", new Color(0.309f, 0.309f, 0.309f, 1.000f));

        frontBoli.SetFloat("_Shininess", 2);
        frontBoli.SetFloat("_Reflect", .112f);
        frontBoli.SetFloat("_FresnelScale", 1.94f);
        frontBoli.SetFloat("_FresnelPower", 2.32f);
        frontBoli.SetFloat("_MetalicScale", .84615f);
        frontBoli.SetFloat("_MetalicPower", 4.23f);
        frontBoli.SetFloat("_CandyScale", 3.7692f);
        frontBoli.SetFloat("_CandyPower", 17.3f);
        frontBoli.SetTexture("_MainTex", glassTex);
        frontBoli.SetTextureScale("_MainTex", new Vector2(10f, 10f));
        frontBoli.SetTexture("_Cube", myCubeMapA);

    }
    private void CarGlassMat(Material carWindowBoli)//车玻璃
    {
        carWindowBoli.shader = myCarGlasses;
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
        carWindowBoli.SetTexture("_MainTex", glassTex);
        carWindowBoli.SetTextureScale("_MainTex", new Vector2(10f, 10f));
        carWindowBoli.SetTexture("_Cube", myCubeMapA);

    }
}

/*
 * 主要功能是得到所有材质并对材质最主要部分进行处理
 * 放在车上，之后,点击，右上角小图标，运行Car4s
 * 还有自动切车轮动画，其实也快
 * */






