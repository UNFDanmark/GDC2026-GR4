using System;
using Unity.VisualScripting;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    [Header("ENTER STUFF HERE!!! (hover for tooltips)")]
    [Tooltip("Pattern should be the order of spots to hit.\nNo duplicates. -1 = nothing\nFormat is:\n0  1  2\n3  X  4\n5  6  7")]
    public int[] pattern = new int[8]{-1,-1,-1,-1,-1,-1,-1,-1};

    [SerializeField]
    public Sprite patternSprite;

    [Header("no touchie, just look")]
    public P p;
    
    void Start()
    {
        p.pattern = pattern;
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
