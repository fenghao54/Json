using System;
using LitJson;
using System.Text;
using UnityEngine;
using System.IO;
using System.Collections.Generic;


public class Data 
{
    public readonly string id;
    public readonly string daoju;
    public readonly string tubiao;
}




public class ConfigUtil:MonoBehaviour
{
    public static ConfigUtil Instance;


    public Dictionary<string, Data> bagConfig;
 

    public void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        bagConfig = Load<Data>();//这个地方要修改成自己的数据txt名称;
        foreach (var obj in bagConfig)
        {
            Debug.Log(obj.Key + "|" + obj.Value.daoju + "|" + obj.Value.tubiao);
        }
        //Load("configBagData");
    }

    private Dictionary<string, T> Load<T>() where T:  class
    {
        string rSheetName = typeof(T).Name; //类型名字提取出来，用来加载文本文件

        TextAsset textAsset = Resources.Load<TextAsset>("Data_Txt/" + rSheetName);

        if (textAsset == null)
        {
            Debug.LogError(rSheetName+"未找到");
        }

        Dictionary<string, T> data = JsonMapper.ToObject<Dictionary<string, T>>(textAsset.text);//把文本文件转化成字典的类型数据

        return data;
    }


    public JsonData Load(string rSheetName)
    {

        TextAsset textAsset = Resources.Load<TextAsset>("Data_Txt/" + rSheetName);

        if (textAsset == null)
        {
            Debug.LogError(rSheetName + "未找到");
        }

        JsonData data = JsonMapper.ToObject(textAsset.text);

        return data;
    } 


}
