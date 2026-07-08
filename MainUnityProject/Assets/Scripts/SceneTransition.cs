using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public static void unloadScene(string nameToLoad)
    {
    }

    IEnumerator unload(string n)
    {
        anim.SetTrigger("unload");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(n);
    }
}
