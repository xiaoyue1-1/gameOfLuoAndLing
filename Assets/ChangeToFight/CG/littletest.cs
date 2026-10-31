using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class littletest : MonoBehaviour
{
    public float speed;
    public float speed2;
    public CgContral contral;

    public SlAndSave aas;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void test ()
    {
        contral.enterCg(speed,speed2);
        Debug.Log(aas.Become());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
