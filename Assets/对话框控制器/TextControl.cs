using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextControl : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string Text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = Text;
    }
    public void textChangeTo(string t)
    {
        Text = t;
    }
}
