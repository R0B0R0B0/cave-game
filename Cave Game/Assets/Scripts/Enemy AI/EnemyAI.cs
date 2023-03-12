using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Android;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{

    public Transform eyes;

    public LayerMask backgroundLayers;

    public Transform target;


    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    new Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rigidbody= GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
       
    }

    public void UpdatePath()
    {
        if(seeker.IsDone())
        {
            seeker.StartPath(rigidbody.position, target.position, OnPathComplete);
        }
        
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint= 0;
        }
    }

    private void FixedUpdate()
    {
        CanSeePlayer();

        if (path == null)
        {
            return;
        }

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath= true;
            return;
        }
        else
        {
            reachedEndOfPath= false;
        }

        rigidbody.MovePosition(rigidbody.position + ((Vector2)path.vectorPath[currentWaypoint] - rigidbody.position).normalized * speed * Time.deltaTime);

        float distance = Vector2.Distance(rigidbody.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    void CanSeePlayer()
    {
        Debug.Log(Vector3.SignedAngle(transform.position, target.position,Vector3.forward));
        if (Vector2.SignedAngle(transform.position,target.position) < 45 + transform.position.z && Vector2.SignedAngle(transform.position, target.position) > -45 + transform.position.z)
        {
            
            RaycastHit2D hit;
            hit = Physics2D.Raycast(transform.position, target.position, 7, backgroundLayers);
            Debug.DrawRay(transform.position,target.position,Color.magenta);
            if (hit.collider != null)
            {
                Debug.Log("hello");
            }
        }
    }

}
