using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpControler : MonoBehaviour
{
    public TextMeshProUGUI SpOfGUI;
    public int SpOfHigh = SpManager.SpChange(true,0);
    public int SpOfNow = SpManager.SpChange(false,0);
    public bool BecomeChange = false;
    public int ChangenumoberOfSp;
    public int ChangenumoberOfHighSp;
    public bool BecomeSet = false;
    public int SetSp;
    public int SetHighSp;

    void Update()
    {
        SpOfHigh = SpManager.SpChange(true, 0);
        SpOfNow = SpManager.SpChange(false, 0);
        SpOfGUI.text = $"{SpOfNow}/{SpOfHigh}";
        if (BecomeChange)
        {
            SpManager.SpChange(true, ChangenumoberOfHighSp);
            SpManager.SpChange(false, ChangenumoberOfSp);
            BecomeChange = false;
        }
        if (BecomeSet) { SpManager.SetSp(true, SetHighSp);SpManager.SetSp(false, SetSp); BecomeSet = false; }
    }
}
