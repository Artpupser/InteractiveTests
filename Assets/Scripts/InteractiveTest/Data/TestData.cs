using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public record TestData : IJsonData
{
    [SerializeField] private string _name;
    [SerializeField] private string _objectLesson;
    [SerializeField] private List<TestQuestionData> _testQuestionDatas;
    [SerializeField] private string _author;
    [SerializeField] private string _schoolClass;
    [SerializeField] private string _secretCode;

    [JsonConstructor]
    public TestData(string name, string objectLesson, List<TestQuestionData> testQuestionDatas, string author, string schoolClass, string secretCode)
    {
        Name = name;
        ObjectLesson = objectLesson;
        TestQuestionDatas = testQuestionDatas;
        Author = author;
        SchoolClass = schoolClass;
        SecretCode = secretCode;
    }
    public string GetInfo()
    {
        return $"Тема: {Name}\nПредмет: {ObjectLesson}\nКласс: {SchoolClass}\nАвтор: {Author}";
    }
    [JsonProperty]
    public string Name { get => _name;  set => _name = value; }
    [JsonProperty]
    public string ObjectLesson { get => _objectLesson;  set => _objectLesson = value; }
    [JsonProperty]
    public List<TestQuestionData> TestQuestionDatas { get => _testQuestionDatas;  set => _testQuestionDatas = value; }
    [JsonProperty]
    public string Author { get => _author;  set => _author = value; }
    [JsonProperty]
    public string SchoolClass { get => _schoolClass;  set => _schoolClass = value; }
    [JsonProperty]
    public string SecretCode { get => _secretCode; set => _secretCode = value; }
}
