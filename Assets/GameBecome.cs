using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameBecome : MonoBehaviour
{
    public float x;
    public float y;
    public float speet =1;
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
    public void ChangeX(int number)
    {
        Transform myTransform = transform;
        transform.MoveTo(myTransform.position.x +number, myTransform.position.y, speet);
    }
    public void ChangeY(int number)
    {
        Transform myTransform = transform;
        transform.MoveTo(myTransform.position.x, myTransform.position.y+number, speet);
    }

}
