using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using MyDialogs = Game.Data.Dialogs;

public class Say : MonoBehaviour
{
    [SerializeField] private MyDialogs _dialog;      // variable of current dialog
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Choice _choice;
    [SerializeField] private string _nextSceneName;

    private bool _allowSkipping = true;

    private int _index;

    private void OnEnable()
    {
        if (_dialog != null)
        {
            NextDialog();
        }
    }

    public void NextDialog()
    {
        if(_allowSkipping)
        {
            if (_index == _dialog.Get.Count)
            {
                SceneManager.LoadScene(_nextSceneName);   // Loading next scene, if there is no more dialogs
                return;
            }
            AudioManager.Instance.PlaySFX("Wet Click");              // Audio Manager Test

            _name.SetText(_dialog.Get[_index].Name);
            _text.SetText(_dialog.Get[_index].Text);
            Debug.Log("Next Text");
            CreateChoice();
            _index++;
        }
       

    }

    public void CreateChoice()
    {
        if ( _dialog.Get[_index].Choices.Length != 0)
        {
            _allowSkipping = false;

            _choice.ShowButtonCanvas();
            foreach (MyDialogs.ChoiceElement choiceElement in _dialog.Get[_index].Choices)
                _choice.Add(choiceElement, this);    
        }
    }

    public void Choice(ChoiceButton buttonOfchoice)
    {
        _index = 0;
        _dialog = buttonOfchoice.Dialogs;            //Getting new dialog according to the new choice
        buttonOfchoice.DestroyButtonOfChoice();                    
        _choice.HideButtonCanvas();
        _allowSkipping = true;
        NextDialog();
        
    }
}
