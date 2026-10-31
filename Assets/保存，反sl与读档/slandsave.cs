using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;


public class SlAndSave : MonoBehaviour
{
    public savedata now = null;
    public int lingPlace;
    string savePath ;

    
    public int getNumber()
    {
        now.nowIndex++;

        return now.index[now.nowIndex];
    }
    public int[] Become()
    {


        for (int i = 0; i < 1000; i++)
        {
            now.index[i] = Random.Range(1, 7);

        }
        int a = 0;
        for (int i = 0; i < 1000; i++)
        {
            if (a < lingPlace - 6)
            {
                a = now.index[i] + a;
            }
            else if (a < lingPlace)
            {
                Debug.Log("修改前最后一个数：" + now.index[i]);
                now.index[i] = lingPlace - a;
                i=999;
                int b = 0;
                for (int j = 0; j <= i; j++)
                {
                    b = now.index[j] + b;
                    if (b == lingPlace)
                    {
                        Debug.Log("成功");
                    }
                    else if (j == i+10)
                    {
                        Debug.LogWarning("i= " + i + "最后一个数为" + now.index[i]);
                    }
                }
            }
            else if(a==lingPlace )
            {
                Debug.Log("数据正好，无需修改：" + now.index[i]);
                i = 999;
                int b = 0;
                for (int j = 0; j <= i; j++)
                {
                    b = now.index[j] + b;
                    if (b == lingPlace && j == i)
                    {
                        Debug.Log("伪随机成功");
                    }
                    else if (j == i)
                    {
                        Debug.LogWarning("伪随机似乎失败了，i= " + i + "最后一个数为" + now.index[i]);
                    }
                }
            }

        }
        return now.index;
    }


    [System.Serializable]
    public class savedata
    {
        public string name;
        public int[] index=new int[1000];
        public int nowIndex;
        public int hp;
        public int sp;
        public int nowPlace;
    }
    public void SaveGame(savedata data, string savePlace)
    {
        // 构建完整路径
        string fullPath = Application.persistentDataPath + savePlace + "/gameData.json";

        // 【新增】获取目录路径（不包含文件名）
        string directory = System.IO.Path.GetDirectoryName(fullPath);

        // 【新增】如果目录不存在，则创建
        if (!System.IO.Directory.Exists(directory))
        {
            System.IO.Directory.CreateDirectory(directory);
        }

        try
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(fullPath, json);
            Debug.Log("存档成功: " + fullPath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("存档失败: " + e.Message);
        }
    }
    public savedata LoadGame(string savePlace)
    {
        savePath = Application.persistentDataPath + savePlace + "/gameData.json";
        if (File.Exists(savePath))
        {
            try
            {
                string json = File.ReadAllText(savePath);
                savedata data = JsonUtility.FromJson<savedata>(json);
                Debug.Log("读档成功");
                now = data;
                return data;
            }
            catch
            {
                Debug.LogError("读档失败，文件损坏");
                return null;
            }
        }
        else
        {
            Debug.Log("未找到存档文件，返回 null");
            return null;
        }
    }
}