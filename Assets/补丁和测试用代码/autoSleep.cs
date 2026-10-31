using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoSleep : MonoBehaviour
{
    CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.Show(false, 0.4f);
    }
    public void showUp()
    {
        canvasGroup.Show(true, 0.4f);
    }
    // Update is called once per frame

}
