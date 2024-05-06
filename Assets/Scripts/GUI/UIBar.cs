using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    [SerializeField] private UICheckbox _circleStep;
    [SerializeField] private Transform _bar;
    [SerializeField] public int countSteps;
    [HideInInspector] public List<UICheckbox> circleSteps;
    [HideInInspector] public UnityEvent BarCompleted;
    [HideInInspector] public int currentStep;
    private void Awake()
    {
        currentStep = 0;
        for (var i = 0; i < countSteps; i++)
        {
            var cloneStep = Instantiate(_circleStep, _bar);
            cloneStep.text.text = $"{i + 1}";
            circleSteps.Add(cloneStep);
        }
        CheckCompleted();
    }
    public bool  CompleteStep()
    {
        if (currentStep == countSteps)
        {
            return false;
        }
        currentStep++;
        var circle = circleSteps[currentStep - 1];
        circle.text.gameObject.SetActive(false);
        circle.applyImage.gameObject.SetActive(true);
        CheckCompleted();
        return true;
    }
    public bool BackStep()
    {
        if (currentStep == 0 || currentStep == countSteps)
        {
            return false;
        }
        currentStep--;
        var circle = circleSteps[currentStep + 1];
        circle.text.gameObject.SetActive(true);
        circle.applyImage.gameObject.SetActive(false);
        CheckCompleted();
        return transform;
    }
    private void CheckCompleted()
    {
        if (currentStep == countSteps)
        {
            BarCompleted.Invoke();
        }
    }
}
