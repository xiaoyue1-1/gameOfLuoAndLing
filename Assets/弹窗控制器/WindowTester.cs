using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WindowTester : MonoBehaviour
{
    public string TestText;
    public WindowsAndTextControler windowControl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void  Onclecking()
    {
        windowControl.GetWindow(TestText);
    }
}
