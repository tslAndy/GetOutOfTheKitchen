using UnityEngine;

public class TestBottle : MonoBehaviour
{
    [SerializeField] private float forceOfTheJump;
    private Rigidbody2D _localRb;
    private State _bottleState = State.FallState;
    Vector3 rotationAmount = new Vector3(0f, 0f, 2f);

    int collision;
    private void Start()
    {
        _localRb = gameObject.GetComponent<Rigidbody2D>();
        _localRb.isKinematic = false;
        _localRb.AddForce(Vector2.up * 200f);
    }
    private enum State
    {
        FallState,
        JumpState,
        Explode
    }
    private void Update()
    {
        switch (_bottleState)
        {

            case State.JumpState:
                transform.Rotate(rotationAmount, Space.Self);
                break;

            case State.Explode:
                Destroy(gameObject);
                break;

            default:
                break;

        }  
    }

    private void OnCollisionEnter2D(Collision2D other)
    {     
        if(collision == 0)
        {
            _localRb.AddForce(new Vector2(0.5f, 0.5f) * forceOfTheJump);
            _bottleState = State.JumpState;
            collision++;
        }
        else
        {
            _bottleState = State.Explode;
        }
              
    }
}
