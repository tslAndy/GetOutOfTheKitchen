using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class TopBlockStateManager: MonoBehaviour
{
    ///// Current State /////
    private BaseTopBlockState _currentState;


    public TP_RestState restState = new TP_RestState();
    public TP_FallState fallState = new TP_FallState();          // Instances of states. Are used to have ability to acess other inheritors form their insides
    public TP_GoUpState goUpState = new TP_GoUpState();

    [HideInInspector] public Rigidbody2D rb;                                         // Variable of KINEMATIC rigidbody

    ///// Vector3 variables /////  
    [HideInInspector] public Vector3 topPosition;                         // Store initial top position of the block
    [HideInInspector] public Vector3 bottomPosition;                             // Position for position on the floor (will use in "TP_GoUpState" for returning block to its initial position)
    [HideInInspector] public Quaternion topRotation;                         // Store initial top rotation of the block
    [HideInInspector] public Quaternion bottomRotation;
    ///// Speed /////
    public float durationOfGoingUp;
    public float speedOfGoingDown;

    /////  Variables for TP_RestState  /////
    public float secondsUntileNextFall;                                      // "seconds" that TP_RestState use to wait before next fall of the script

    ///// Variabled for TP_GoUpState //////
    public float lerpSpeed = 3f;    //for lerp speed of the rotation

    ///// Variabled for TP_fallState //////
    public float maxDistance;                                                //  max distance of raycast



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        topPosition = transform.position;
        topRotation = transform.rotation;
    }

    private void Start()
    {
        SwitchStates(restState);
        _currentState.Enter(this);
    }

    private void Update()
    {

       _currentState.BlockStateUpdate(this);

       
    }

    public void SwitchStates(BaseTopBlockState _nextState)           // Method that inheritors of "BaseTopBlockState" use to Switch to other states 
    {
        if( _currentState != null ) 
        {
            _currentState.Exit(this);                                 //Exit current state
        }   
        _currentState = _nextState;                                     // Set new current state
        _currentState.Enter(this);                                     // Enter into new current State
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Player"))   // THIS IS NOT WORKING WITH FLOOR FOR SOME REASON
        {
            _currentState.OnCollisonEnter2D(this);
            bottomPosition = transform.position;
            Debug.Log("Found floor");
        }
    }

}

