using SFB;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnalysisTestMenu : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform _analysisForm;
    [SerializeField] private Transform _startForm;
    [SerializeField] private TMP_InputField _codeInputField;
    [Header("Prefabs")]
    [SerializeField] private UIQuestionAnswer _questionAnswerPrefab;
    [Header("Parents")]
    [SerializeField] private Transform _parentQuestionAnswers;
    [SerializeField] private List<UIQuestionAnswer> _listQuestionAnswers;
    [SerializeField] private TextMeshProUGUI _textAnalysis;
    [SerializeField] private TextMeshProUGUI _textTimer;
    private TestResultsData _testResults {get; set;}
    private void Start()
    {
        _analysisForm.gameObject.SetActive(false);
        _startForm.gameObject.SetActive(true);
        try
        {
            
            var path = StandaloneFileBrowser.OpenFilePanel("Overwrite with .testsaveres","","testsaveres", false)?[0];
            if (path == null || path == "")
            {
                OnExit();
                return;
            }
            FileManager.LoadDecodeFileToPath(path, out TestResultsData content);
            _testResults = content;
        }
        catch (System.Exception e)
        {
            throw e;
        }
    }
    public void OnExit()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void OnEnter()
    {
        if (_codeInputField.text != _testResults.TestData.SecretCode)
            return;
        _analysisForm.gameObject.SetActive(true);
        _startForm.gameObject.SetActive(false);
        LoadResults();
    }
    public void LoadResults()
    {
        var index = 0;
        _textTimer.text = $"{_testResults.Timer:hh\\:mm\\:ss}";
        _testResults.TestData.TestQuestionDatas.ForEach(data =>
        {
            var clone = Instantiate(_questionAnswerPrefab, _parentQuestionAnswers);
            var answerUser = _testResults.Answers[index];
            clone.indexText.text = $"Ответ: #{index + 1}";
            clone.questionText.text = $"Вопрос: {data.Question}";
            clone.answerTeacherText.text = $"Ответ учителя: {data.Answer}";
            clone.answerUserText.text = $"Ответ ученика: {answerUser}";
            clone.machineRight = data.Answer.ToLower() == answerUser.ToLower();
            clone.teacherResultToggle.isOn = clone.machineRight || clone.teacherRight;
            clone.teacherResultToggle.onValueChanged.AddListener((x) =>
            {
                UpdateTextAnalysis();
            });
            clone.machineResultText.text = $"AI: {clone.machineRight}";
            _listQuestionAnswers.Add(clone);
            //code <<
            index++;
        });
        UpdateTextAnalysis();
    }
    public void UpdateTextAnalysis()
    {
        float rightsAnswersMachine = 0;
        float rightsAnswersTeacher = 0;
        _listQuestionAnswers.ForEach((data) =>
        {
            rightsAnswersTeacher += data.teacherResultToggle.isOn ? 1 : 0; ;
            rightsAnswersMachine += data.machineRight ? 1 : 0;
        });
        var maxQuestions = _listQuestionAnswers.Count;
        float procentMachine = rightsAnswersMachine / maxQuestions;
        float procentTeacher = rightsAnswersTeacher / maxQuestions;
        _textAnalysis.text = $"Итог учителя: {procentTeacher * 100f}%, Оценка {procentTeacher * SettingsData.Instance.MaxScore}; Итог AI: {procentMachine * 100f}%, Оценка {procentMachine * SettingsData.Instance.MaxScore}";
    }
}
