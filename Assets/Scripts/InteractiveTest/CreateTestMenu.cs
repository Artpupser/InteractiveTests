using SFB;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateTestMenu : MonoBehaviour
{
    [SerializeField] private UIBar _progressBar;
    [SerializeField] private List<Transform> _uiSteps;
    [SerializeField] private Transform _finalStep;
    [SerializeField] private TextMeshProUGUI _debugTitle;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private TestData _testData;
    [Header("Components UI")]
    [SerializeField] private UICustomInput _nameInput;
    [SerializeField] private UICustomInput _objectLessonInput;
    [SerializeField] private UICustomInput _authorInput;
    [SerializeField] private UICustomInput _classInput;
    [SerializeField] private UICustomInput _codeInput;
    [SerializeField] private List<UIQuestionButton> _questionButtons;
    [Header("Form Components UI")]
    [SerializeField] private Transform _questionForm;
    [SerializeField] private UIQuestionButton _questionButtonPrefab;
    [SerializeField] private Transform _questionsParent;
    [SerializeField] private UICustomInput _questionInput;
    [SerializeField] private UICustomInput _answerInput;
    [SerializeField] private TextMeshProUGUI _formTitle;
    private int _lastOpenedFormIndex = 0;
    private void Awake()
    {
        _testData = new("", "", new(),"","","");
        _continueButton.gameObject.SetActive(true);
        _backButton.gameObject.SetActive(false);
    }
    public void CheckStep()
    {
        var a = _progressBar.currentStep == 0;
        var b = _progressBar.currentStep == _progressBar.countSteps - 1;
        var c = a || !a && !b;
        _continueButton.gameObject.SetActive(a || c);
        _backButton.gameObject.SetActive(b || c);
    }
    public void OnExit()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void OnSave()
    {
        try
        {
            var path = StandaloneFileBrowser.SaveFilePanel("", "",$"Новый тест [{_testData.Name}]","testsave");
            if (path == "" || path == null)
            {
                _debugTitle.text = "Сохранение файла отменено";
                return;
            }
            _debugTitle.text = "Файл успешно сохранен!";
            FileManager.SaveEncodeFileToPath(path, _testData);
        }
        catch (Exception e)
        {
            _debugTitle.text = "Произошла ошибка при сохранении файла :(";
            throw e;
        }
    }
    public void CreateNewQuestion()
    {   
        if (_testData.TestQuestionDatas.Count == 39)
        {
            return;
        }
        _testData.TestQuestionDatas.Add(new("", ""));
        var clone = Instantiate(_questionButtonPrefab, _questionsParent);
        clone.index = _testData.TestQuestionDatas.Count - 1;
        clone.UpdateUI();
        clone.parent = this;
        _questionButtons.Add(clone);
    }
    public void DeleteNewQuestion(int index)
    {
        var question = _questionButtons[index];
        _testData.TestQuestionDatas.Remove(_testData.TestQuestionDatas[index]);
        Destroy(question.gameObject);
        for (var i = 0; i < _testData.TestQuestionDatas.Count; i++)
        {
            var button = _questionButtons[index];
            button.index = index;
            button.UpdateUI();
        }
    }
    public void OpenQuestionForm(int index)
    {
        _lastOpenedFormIndex = index;
        _questionForm.gameObject.SetActive(true);
        LoadQuestionForm();
    }
    public void SaveQuestionForm()
    {
        var question = _testData.TestQuestionDatas[_lastOpenedFormIndex];
        question.Question = _questionInput.inputField.text;
        question.Answer = _answerInput.inputField.text;
        _questionForm.gameObject.SetActive(false);
    }
    public void CancelQuestionForm()
    {
        _questionForm.gameObject.SetActive(false);
    }
    public void LoadQuestionForm()
    {
        if (_lastOpenedFormIndex >= _testData.TestQuestionDatas.Count)
        {
            return;
        }
        var question = _testData.TestQuestionDatas[_lastOpenedFormIndex];
        _formTitle.text = $"Вопрос #{_lastOpenedFormIndex + 1}";
        _questionInput.inputField.text = question.Question;
        _answerInput.inputField.text = question.Answer;
    }
    public void OnComplete()
    {
        _uiSteps[_progressBar.currentStep].gameObject.SetActive(false);
        _testData.Name = _nameInput.inputField.text;
        _testData.ObjectLesson = _objectLessonInput.inputField.text;
        _testData.SecretCode = _codeInput.inputField.text;
        _testData.Author = _authorInput.inputField.text;
        _testData.SchoolClass = _classInput.inputField.text;
        _progressBar.CompleteStep();
        _finalStep.gameObject.SetActive(true);
        _continueButton.gameObject.SetActive(false);
        _backButton.gameObject.SetActive(false);
    }
    public void NextStep()
    {
        _uiSteps[_progressBar.currentStep].gameObject.SetActive(false);
        _progressBar.CompleteStep();
        _uiSteps[_progressBar.currentStep].gameObject.SetActive(true);
        CheckStep();
    }
    public void PreviousStep()
    {
        if (_progressBar.currentStep != _progressBar.countSteps)
        {
            _uiSteps[_progressBar.currentStep].gameObject.SetActive(true);
        }
        _progressBar.BackStep();
        _uiSteps[_progressBar.currentStep].gameObject.SetActive(false);
        CheckStep();
    }
}
