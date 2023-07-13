using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PupilsMovement : MonoBehaviour
{
    [SerializeField] private GameObject _leftPupil;
    [SerializeField] private GameObject _rightPupil;
    public void EyesFollowMouse(Vector2 direction)
    {
        _leftPupil.transform.up = direction;
        _rightPupil.transform.up = direction;
    }
}
