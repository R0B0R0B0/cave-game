using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class EnemyAI : MonoBehaviour
{
    public Transform player;

    public LayerMask backgroundLayers;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    //Pathfinding
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;

    Vector3 target;

    //Basic stuff
    public new Rigidbody2D rigidbody;

    //See stuff

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rigidbody= GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
       
    }

    public void UpdatePath()
    {
        if(target == transform.position) { return; }
        if(seeker.IsDone())
        {
            seeker.StartPath(rigidbody.position, target, OnPathComplete);
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.position - transform.position).normalized,10,backgroundLayers);
        
        Debug.DrawRay(transform.position, (player.position - transform.position).normalized * 10, Color.magenta);
        if (hit)
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                target = hit.point;
                Debug.Log("hello");
            }   
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(target, 0.5f);
    }
}
