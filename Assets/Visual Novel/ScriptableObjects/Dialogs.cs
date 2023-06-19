using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "New Dialogs", menuName = "SciptableObjects/Visual Novel/" + nameof(Dialogs))]
    public class Dialogs : ScriptableObject
    {
        [System.Serializable]
        public class Dialog
        {
            [SerializeField] private string _name;
            [SerializeField][TextArea] private string _text;
            [SerializeField] private ChoiceElement[] _choices;

            public string Name => _name;
            public string Text => _text;
            public ChoiceElement[] Choices => _choices;

        }
        [System.Serializable]
        public class ChoiceElement
        {
            [SerializeField] private string _text;
            [SerializeField] private Dialogs _dialogs;

            public string Text => _text;
            public Dialogs Dialogs => _dialogs;
        }



        [SerializeField] private List<Dialog> _dialogs;

        public List<Dialog> Get => _dialogs;

    }

}
