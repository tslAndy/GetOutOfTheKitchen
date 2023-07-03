using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDonut : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private List<Transform> points;
    [SerializeField] private int iterations;
    [SerializeField] private float speed;

    private Vector3 _startPosition;
    private State _state;
    private int _pointIndex, _iteration;

    private void Start()
    {
        _startPosition = transform.position;
        points.Add(cam.transform);
    }

    private enum State
    {
        FlyingToCenterOfScreen,
        FlyingToPoints,
        FlyingAway,
        Idle
    }

    private void Update()
    {
        Vector3 target, direction;

        switch (_state)
        {
            case State.FlyingToCenterOfScreen:

                target = cam.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2));
                direction = (target - transform.position).normalized;
 
                transform.position += direction * speed * Time.deltaTime;

                if (Vector2.Distance(transform.position, target) <= 1f)
                    _state = State.FlyingToPoints;

                break;
















            case State.FlyingToPoints:
                if (_iteration == iterations)
                {
                    _state = State.FlyingAway;
                    break;
                }


                Vector3 currentPointPosition = points[_pointIndex].position;
                if (Vector2.Distance(transform.position, currentPointPosition) <= 1f)
                {
                    _pointIndex++;
                    if (_pointIndex >= points.Count)
                    {
                        _pointIndex = 0;
                        _iteration++;
                    }
                }

                target = points[_pointIndex].position;
                direction = (target - transform.position).normalized;
                transform.position += (Vector3)direction * speed * Time.deltaTime;


                break;

            case State.FlyingAway:
                direction = (_startPosition - transform.position).normalized;
                transform.position += (Vector3)direction * speed * Time.deltaTime;
                if (Vector2.Distance(transform.position, _startPosition) <= 1f)
                    _state = State.Idle;

                break;

                case State.Idle:
                break;

            default:
                break;
        }
    }


}
