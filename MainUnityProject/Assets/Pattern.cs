using System;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    public int[] topRow = new int[3]{-1,-1,-1};
    public int[] midRow = new int[2]{-1,-1};
    public int[] botRow = new int[3]{-1,-1,-1};

    [Header("no touchie, just look")]
    public P p;
    
    void Start()
    {
        int n = 0;
        foreach(int i in topRow)
        {
            p.pattern[n] = i;
            n++;
        }
        foreach(int i in midRow)
        {
            p.pattern[n] = i;
            n++;
        }
        foreach(int i in botRow)
        {
            p.pattern[n] = i;
            n++;
        }
        AssignId(PatternManager.instance.NextId());
        p.name = gameObject.name;
    }

    public void AssignId(int id)
    {
        p.id = id;
    }
}

[System.Serializable]
public class P
{
    public string name;
    public int[] pattern = new int[8]{-1,-1,-1,-1,-1,-1,-1,-1};
    public int id;

    P()
    {
        id = 1;
    }
}
