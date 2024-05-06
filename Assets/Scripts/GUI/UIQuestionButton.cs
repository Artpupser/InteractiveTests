using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIQuestionButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI _title;
    public int index;
    public CreateTestMenu parent { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {

            case PointerEventData.InputButton.Left:
                parent.OpenQuestionForm(index);
                return;
            case PointerEventData.InputButton.Right:
                parent.DeleteNewQuestion(index);
                return;
        }
    }

    public void UpdateUI()
    {
        _title.text = $"#{index + 1}";
    }
}
