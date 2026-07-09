using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;
    public TextMeshProUGUI tutorialTextUI;
    public string[] tutorialTexts = new string[10];
    public int currentStep = -1;
    int charIndex;
    public InputAction stepAction;

    public void nextStep()
    {
        if (currentStep == -2) return;
        currentStep++;
        StartCoroutine(typeText());
    }

    void Start() => nextStep();
    void Awake() => instance = this;

    IEnumerator typeText()
    {
        int startStep = currentStep;
        charIndex = 0;
        foreach (char x in tutorialTexts[currentStep])
        {
            if (startStep != currentStep) yield break;
            tutorialTextUI.text = tutorialTexts[currentStep].Substring(0, charIndex + 1);
            charIndex++;
            yield return new WaitForSeconds((0.05f/(1.0f/Time.timeScale)));
        }
        
        if (currentStep == tutorialTexts.Length-1)
        {
            currentStep = -2;
            yield return new WaitForSeconds(3);
            tutorialTextUI.text = "";
        }
    }
}
