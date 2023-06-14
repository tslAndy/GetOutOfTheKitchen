using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerEyes : MonoBehaviour
{
    public float maxDistance;
    private RaycastHit2D _hit;

    public LineRenderer thelineRenderer;
    public Transform lazerPoint;
    Transform m_transform;

    private void Start()
    {
        m_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        ShootLazer();
    }

    void ShootLazer()
    {
        Debug.DrawLine(lazerPoint.position, lazerPoint.position + Vector3.left * maxDistance, Color.red);
        if (Physics2D.Raycast(m_transform.position, - transform.right, maxDistance).collider != null)
        {
            _hit = Physics2D.Raycast(m_transform.position, -transform.right, maxDistance);
            Draw2DLine(lazerPoint.position, _hit.point);
            Debug.Log("hit smth");
        }
        else
        {
            Draw2DLine(lazerPoint.position, lazerPoint.position + Vector3.left * maxDistance);
        }
        
    }

    void Draw2DLine(Vector3 startPos, Vector3 endPos)
    {
        thelineRenderer.SetPosition(0, startPos);
        thelineRenderer.SetPosition(1, endPos);
    }
}
