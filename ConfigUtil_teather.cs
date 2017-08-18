using System;
using LitJson;
using System.Text;
using UnityEngine;
using System.IO;
using System.Collections.Generic;


public class configBagData
{
    public readonly string id;
    public readonly string itemName;
    public readonly string spriteName;
    public  int count;
}

 
public class ConfigUtil : MonoBehaviour
{
    public static ConfigUtil Instance;


    public Dictionary<string, configBagData> bagConfig;


    public void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        bagConfig = Load<configBagData>();
    }

    /// <summary>
    /// 导入数据
    /// </summary>
    private Dictionary<string, T> Load<T>() where T : class
    {
        string rSheetName = typeof(T).Name;

        string readFilePath = Application.persistentDataPath + "/" + rSheetName + ".txt";
 

        string str;


        if (File.Exists(readFilePath))
        {
            StreamReader textData = File.OpenText(readFilePath);
            str = textData.ReadToEnd();
            textData.Close();
            textData.Dispose();
        }
        else
        {
            TextAsset textAsset = Resources.Load<TextAsset>("assetsbundles/data/sheet/" + rSheetName);
            if (textAsset == null)
            {
                Debug.LogError(rSheetName + "未找到");
                return null;
            }
            str = textAsset.text;
        }


 
        Dictionary<string, T> data = JsonMapper.ToObject<Dictionary<string, T>>(str);

        return data;
    }

 
    /// <summary>
    /// 导出数据
    /// </summary>
    public void ExportToJson<T>(Dictionary<string, T> rData) where T : class
    {
        string rSheetName = typeof(T).Name;

        string outFilePath = Application.persistentDataPath + "/data/" + rSheetName+".txt";

        string jsonText = JsonMapper.ToJson(rData);


        Debug.Log(outFilePath);
        FileStream fs = new FileStream(outFilePath, FileMode.Create);
        //获得字节数组
        byte[] data = System.Text.Encoding.UTF8.GetBytes(jsonText);
        //开始写入
        fs.Write(data, 0, data.Length);
        //清空缓冲区、关闭流
        fs.Flush();
        fs.Close();
    }
}
