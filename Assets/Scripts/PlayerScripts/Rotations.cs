using PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerScripts
{
    [RequireComponent(typeof(InputHandler))]
    public class Rotations : MonoBehaviour
    {
        [SerializeField] GameObject arm1, arm2;
        [SerializeField] GameObject pupil1, pupil2;
        [SerializeField] Player player;


        bool _facingRight = true;

        public void SetRotationsOnStart(MainInputActions inputActions, Camera cam)                  // it used in Input Handler script's Start() to prevent misposition of the arms
        {
            Vector2 mousePosition = cam.ScreenToWorldPoint(inputActions.Player.MousePosition.ReadValue<Vector2>());
            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

            RotateArms(direction, mousePosition);
            RotateEyes(direction);
        }


        public void RotateArms(Vector3 direction, Vector3 mousePosition)
        {
            arm1.transform.right = direction;
            arm2.transform.right = direction;
            if (mousePosition.x > transform.position.x && !_facingRight)
            {
                FlipPlayer();
            }
            else if (mousePosition.x < transform.position.x && _facingRight)
            {
                FlipPlayer();
            }

        }
        public void RotateEyes(Vector3 direction)
        {
            pupil1.transform.up = direction;
            pupil2.transform.up = direction;
        }

        public void RotateWeapon(Vector3 direction)
        {
            player.WeaponManager.CurrentWeapon.transform.up = direction;
        }

        private void FlipPlayer()
        {
            _facingRight = !_facingRight;
            transform.Rotate(0f, 180f, 0f);
        }

    }
}
