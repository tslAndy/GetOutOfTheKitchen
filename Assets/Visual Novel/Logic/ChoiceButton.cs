using Game.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using MyDialogs = Game.Data.Dialogs;

public class ChoiceButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private Button _self;
    [SerializeField] private Say _say;

    public Say Say { set => _say = value; }
    public MyDialogs Dialogs { get; private set; }
    public void Show(MyDialogs.ChoiceElement choiceElement)
    {
        _buttonText.SetText(choiceElement.Text);
        Dialogs = choiceElement.Dialogs;
        
        _self.onClick.AddListener(() => _say.Choice(this));
    }

    public void Hide() => Destroy(gameObject);

}
