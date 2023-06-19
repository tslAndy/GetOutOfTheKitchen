using UnityEngine;
using MyDialogs = Game.Data.Dialogs;

public class Choice : MonoBehaviour
{

    [SerializeField] private Canvas _self;
    [SerializeField] private Transform _placeOfButton;
    [SerializeField] private ChoiceButton _buttonPrefab;
    private ChoiceButton currentButton;
    public void MajorShow() => _self.enabled = true;

    public void MajorHide() => _self.enabled=false;

    public void Add(MyDialogs.ChoiceElement choiceElement, Say say)
    {
        currentButton = Instantiate(_buttonPrefab, _placeOfButton);
        currentButton.Say = say;
        currentButton.Show(choiceElement);
    }

    
}
