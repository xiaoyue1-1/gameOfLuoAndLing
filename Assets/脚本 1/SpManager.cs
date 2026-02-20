using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class  SpManager
{
    private static int Sp = 100;
    private static int HighSp = 100;
    public static int SpChange(bool High, int change)
    {
        if (change == 0 & High == false)
        {
            return Sp;
        }
        else if ((!(change == 0)) & High == false)
        {
            Sp = change + Sp;
            return 0;
        }
        else if (change == 0 & High == true)
        {
            return HighSp;
        }
        else if (High & (!(change == 0)))
        {
            HighSp = change + HighSp;
            return 0;
        }
        return 0;
    }
    public static int SetSp(bool High,int ChangeSp) 
    { 
        if (!High)
        {
            if(ChangeSp == 0)
            {
                return Sp;
            }
            else { Sp = ChangeSp; return 0; }
        }
        else if (ChangeSp == 0)
        {
            return HighSp;

        }
        else if (!(ChangeSp == 0))
        {
            HighSp = ChangeSp;
        }
        return 0;
    }
}
