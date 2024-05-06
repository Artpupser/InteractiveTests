using SFB;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteTestMenu : MonoBehaviour
{
    [SerializeField] private Transform _loadingForm;
    [SerializeField] private Transform _completeForm;
    [SerializeField] private Transform _goTestButton;
    [SerializeField] private Transform _finishTestForm;
    [SerializeField] private TextMeshProUGUI _titleLoadingForm;
    [SerializeField] private TextMeshProUGUI _questionText;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _infoBlockText;
    [SerializeField] private TextMeshProUGUI _finishTitle;
    [SerializeField] private TMP_InputField _qustionInputField;
    [SerializeField] private TestData testData { get; set; }
    private TestResultsData _testResults { get; set; }
    private int _index = -1;
    private TimeSpan _timer;
    private bool _done;
    private void Awake()
    {
        _goTestButton.gameObject.SetActive(false);
    }
    private void Start()
    {
        var path = StandaloneFileBrowser.OpenFilePanel("Overwrite with .testsave", "", "testsave", false)?[0];
        try
        {
            if (!FileManager.LoadDecodeFileToPath(path, out TestData content))
            {
                throw new System.Exception("Error Loading File :(");
            }
            testData = content;
            _goTestButton.gameObject.SetActive(true);
            _titleLoadingForm.text = "Файл успешно загружен :)";
            _infoBlockText.text = testData.GetInfo();
            _testResults = new(testData, new(), new());
        }
        catch (System.Exception e)
        {
            _titleLoadingForm.text = "Произошла ошибка при загрузки файла :(";
            throw e;
        }
    }
    public void GoCompleteTest()
    {
        _done = false;
        StartTimer();
        _loadingForm.gameObject.SetActive(false);
        _completeForm.gameObject.SetActive(true);
        OnContinue();
    }
    public IEnumerator GoTimer()
    {
        _timer = TimeSpan.Zero;
        while (!_done)
        {
            _timer = _timer.Add(TimeSpan.FromSeconds(1));
            _timerText.text = _timer.ToString(@"hh\:mm\:ss");
            yield return new WaitForSeconds(1);
        }
    }
    public void StartTimer()
    {
        StartCoroutine(GoTimer());
    }
    public IEnumerator FinishTest()
    {
        _done = true;
        _testResults.TestData = testData; 
        _testResults.Timer = _timer;
        _loadingForm.gameObject.SetActive(false);
        _completeForm.gameObject.SetActive(false);
        _finishTestForm.gameObject.SetActive(true);
        _finishTitle.text = "Тест завершен, Сохраняю результаты :)";
        yield return new WaitForSeconds(1);
        try
        {
            var path = StandaloneFileBrowser.SaveFilePanel("", "", $"Результаты теста [{_testResults.TestData.Name}]", "testsaveres");
            FileManager.SaveEncodeFileToPath(path, _testResults);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    public void OnContinue()
    {
        if (_index >= 0)
        {
            _testResults.Answers.Add(_qustionInputField.text == "" ? "Пустой ответ" : _qustionInputField.text);
            _qustionInputField.text = "";
        }
        _index++;
        if (_index == testData.TestQuestionDatas.Count)
        {

            StartCoroutine(FinishTest());
            return;
        }
        var question = testData.TestQuestionDatas[_index];
        _questionText.text = $"Вопрос #{_index + 1}: {question.Question}";
    }
    public void OnExit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
