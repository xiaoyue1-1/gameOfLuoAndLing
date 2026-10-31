using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuickSort : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int[]  Quicksort(int[] input)
    {
        int inputLength = input.Length;
        for (int i = 0; i < inputLength - 1; i++)
        {
            for (int j = 0; j < inputLength - i - 1; j++)
            {
                if (input[j] > input[j + 1])
                {
                    int temp = input[j];
                    input[j] = input[j + 1];
                    input[j + 1] = temp;
                }
            }
        }
        return input;
    }
}
