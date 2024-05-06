using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TestResultsData : IJsonData
{
    [SerializeField] private TestData _testData;
    [SerializeField] private TimeSpan _timer;
    [SerializeField] private List<string> _answers;

    [JsonConstructor]
    public TestResultsData(TestData testData, List<string> answers, TimeSpan timer)
    {
        TestData = testData;
        Answers = answers;
        Timer = timer;
    }
    [JsonProperty]
    public TestData TestData { get => _testData; set => _testData = value; }
    [JsonProperty]
    public List<string> Answers { get => _answers; set => _answers = value; }
    [JsonProperty]
    public TimeSpan Timer { get => _timer; set => _timer = value; }
}
