using UnityEngine;
using MyDialogs = Game.Data.Dialogs;

public class Choice : MonoBehaviour
{
    // The code is responsible for activating the canvas and button models (prefabs) with button choices. But ChoiceButton script is responsible for the text filling of the button/s and assigning a next dialog..  
    [SerializeField] private Canvas _self;
    [SerializeField] private Transform _parent;
    [SerializeField] private ChoiceButton _buttonPrefab;
    private ChoiceButton currentButton;
    public void ShowButtonCanvas() => _self.enabled = true;

    public void HideButtonCanvas() => _self.enabled=false;

    public void Add(MyDialogs.ChoiceElement choiceElement, Say say)        
    {
        currentButton = Instantiate(_buttonPrefab, _parent);
        currentButton.Say = say;
        currentButton.AssignButtonOfChoice(choiceElement);
    }

    
}
