using System;
using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    public static PatternManager instance;
    public List<Pattern> patterns = new List<Pattern>();

    private int idsAssigned = 0;
    
    void Awake()
    {
        instance = this;
        patterns.Clear();
        foreach (Pattern p in transform.GetComponentsInChildren<Pattern>())
        {
            patterns.Add(p);
        }
    }

    public int NextId()
    {
        idsAssigned++;
        return idsAssigned;
    }
}
