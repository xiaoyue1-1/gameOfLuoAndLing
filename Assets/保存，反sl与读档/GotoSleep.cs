using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoSleep : MonoBehaviour
{
    public CanvasGroup This;
    // Start is called before the first frame update
    void Start()
    {
        Sleep();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Sleep() { This.Show(false,0f);   }
    public void GetUp() { This.Show(true,0f); }
}
