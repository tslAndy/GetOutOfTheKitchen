using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MyDialogs = Game.Data.Dialogs;

public class Say : MonoBehaviour
{
    [SerializeField] private MyDialogs _dialogs;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Choice _choice;

    private int _index = 0;

    private void OnEnable()
    {
        if (_dialogs != null)
        {
            NextDialog();
        }
    }

    public void NextDialog()
    {
        if (_index == _dialogs.Get.Count) return;
        _name.SetText(_dialogs.Get[_index].Name);
        _text.SetText( _dialogs.Get[_index].Text);
        Debug.Log("Next Text");
        _index++;

        CreateChoice();
    }

    public void CreateChoice()
    {
        if (_dialogs.Get[_index].Choices.Length != 0)
        {
            _choice.MajorShow();
            foreach (MyDialogs.ChoiceElement choiceElement in _dialogs.Get[_index].Choices)
                _choice.Add(choiceElement, this);    
        }
    }

    public void Choice(ChoiceButton buttonOfchoice)
    {
        _index = 0;
        _dialogs = buttonOfchoice.Dialogs;
        buttonOfchoice.Hide();
        _choice.MajorHide();
    }
}
