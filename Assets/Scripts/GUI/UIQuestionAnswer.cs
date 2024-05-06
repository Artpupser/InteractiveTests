using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIQuestionAnswer : MonoBehaviour
{
    public TextMeshProUGUI indexText;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI answerTeacherText;
    public TextMeshProUGUI answerUserText;
    public TextMeshProUGUI machineResultText;
    public Toggle teacherResultToggle;
    [HideInInspector] public bool machineRight = false;
    [HideInInspector] public bool teacherRight = false;
}
