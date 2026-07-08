using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        instance = this;
    }

    public void unloadScene(string nameToLoad)
    {
        StartCoroutine(unload(nameToLoad));
    }

    IEnumerator unload(string n)
    {
        anim.SetTrigger("unload");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(n);
    }
}
