using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public string matchPattern(List<RectTransform> order)
    {
        int[] orderNumbers = new int[8]{-1,-1,-1,-1,-1,-1,-1,-1};
        int i = 0;
        foreach (RectTransform r in order)
        {
            orderNumbers[i] = Int32.Parse(r.gameObject.name);
            i++;
        }

        foreach (Pattern p in patterns)
        {
            string pPattern = p.p.pattern.ToCommaSeparatedString();
            pPattern = pPattern.Replace(" ", "");
            pPattern = pPattern.Replace(",-1", "");
            string orderPattern = "";
            foreach (int o in orderNumbers)
            {
                orderPattern += o + ",";
            }
            orderPattern = orderPattern.Substring(0, orderPattern.Length - 1);
            orderPattern = orderPattern.Replace(" ", "");
            orderPattern = orderPattern.Replace(",-1", "");
            if (pPattern == orderPattern)
            {
                return p.p.name;
            }
        }

        return null;
    }
}
