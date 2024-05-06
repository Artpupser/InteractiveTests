using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public class TestQuestionData : IJsonData
{
    [SerializeField] private string _question;
    [SerializeField] private string _answer;

    [JsonConstructor]
    public TestQuestionData(string question, string answer)
    {
        Question = question;
        Answer = answer;
    }

    [JsonProperty]
    public string Question { get => _question; set => _question = value; }
    [JsonProperty]
    public string Answer { get => _answer; set => _answer = value; }
}
