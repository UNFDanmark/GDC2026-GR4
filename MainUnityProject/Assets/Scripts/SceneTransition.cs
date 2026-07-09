using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static bool changingScene = false;
    public static SceneTransition instance;
    Animator anim;

    void Awake()
    {
        anim = transform.GetComponentInChildren<Animator>();
        instance = this;
    }

    public void unloadScene(string nameToLoad)
    {
        StartCoroutine(unload(nameToLoad));
    }

    IEnumerator unload(string n)
    {
        Time.timeScale = 1;
        changingScene = true;
        anim.SetTrigger("unload");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(n);
        changingScene = false;
    }
}
