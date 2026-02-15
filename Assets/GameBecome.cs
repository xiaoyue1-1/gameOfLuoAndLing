using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameBecome : MonoBehaviour
{
    public float x;
    public float y;
    public float speet;
    public bool become;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (become)
        {
            transform.MoveTo(x, y, speet);
            become = false;
        }
    }

}
