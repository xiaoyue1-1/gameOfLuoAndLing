using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public SlAndSave Saves;
    public SlAndSave.savedata NowData;
    // Start is called before the first frame update
    void Start()
    {
    }
    public  void Save1 ()
    {
        Saves.SaveGame(NowData, "/Save1");
    }
    public void Save2()
    {
        Saves.SaveGame(NowData, "/Save2");
    }
    public void Save3()
    {
        Saves.SaveGame(NowData, "/Save3");
    }
    public void Save4()
    {
        Saves.SaveGame(NowData, "/Save4");
    }
    public void Read(string place )
    {
        if (Saves.now.index ==null )
        {
            Debug.Log("´æµµÎ´Ê¹ÓÃ¹ý ");
            Saves.Become();
        }
        Saves.now=Saves.LoadGame(place);
    }

    void Update()
    {
        NowData=Saves.now;
    }
}
