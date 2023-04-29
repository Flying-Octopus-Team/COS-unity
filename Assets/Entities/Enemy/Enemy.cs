using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private LayerMask detectionMask;
    [SerializeField] private LayerMask obstaclesMask;

    [SerializeField] private float detectionRange;
    [SerializeField] private float closedetectionRange;
    private Collider[] cols;
    [SerializeField] private float viewAngle = 35.0f;
    private float angleToPlayer;
    private NavMeshAgent nvmAgent;
    bool knowsWherePlayerIs = false;
    private float agentSpeed;

    [SerializeField] private PlayerReference pcRef;

    private void Start()
    {
        nvmAgent= GetComponent<NavMeshAgent>();
        agentSpeed = nvmAgent.speed;
    }

    private void FixedUpdate()
    {
        knowsWherePlayerIs = false;
        if (!nvmAgent) return;
        cols = Physics.OverlapSphere(transform.position, detectionRange, detectionMask);
        bool playerInRange = false;
        foreach (Collider c in cols)
        {
            if (c.transform == this.transform) continue; //w sumie mozna usunac
            if (!(c.transform.CompareTag("Player")))continue;

            playerInRange = true;
            angleToPlayer = CalcAngleToPoint(c.transform);
            float distanceToPlayer = Vector3.Distance(transform.position, c.transform.position);
            float detectionModifier = PlayerDetectionModificator();
            if (distanceToPlayer <= (closedetectionRange*(detectionModifier*2)))
            {
                nvmAgent.SetDestination(c.transform.position);
                knowsWherePlayerIs= true;
                continue;
            }
            if (angleToPlayer > (viewAngle* detectionModifier)) continue;

            //Player is in front of us
            //checking if can be seen
            Vector3 directionToPlayer = c.transform.position - transform.position;
            
            RaycastHit hit;

            if (!Physics.Raycast(transform.position, directionToPlayer, out hit, (detectionRange+1)* detectionModifier, obstaclesMask)) continue;

            if (hit.collider && hit.collider.CompareTag("Player"))
            {
                nvmAgent.SetDestination(c.transform.position);
                knowsWherePlayerIs = true;
                Debug.DrawRay(transform.position, directionToPlayer, Color.green, 1f);
            }
            else
            {
                Debug.DrawRay(transform.position, directionToPlayer, Color.red, 1f);
            }
        }
        if (!playerInRange)
        {
            angleToPlayer = 0;
        }

        if(!knowsWherePlayerIs && (!nvmAgent.pathPending && !nvmAgent.hasPath))
        {
            nvmAgent.speed = agentSpeed / 4;
            Vector3 randomDirection = (Random.insideUnitSphere * 200)+ transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 200, 1);
            nvmAgent.SetDestination(hit.position);
        }
        else
        {
            nvmAgent.speed = agentSpeed;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange* PlayerDetectionModificator());
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position+(transform.forward*detectionRange * PlayerDetectionModificator()));
    }

    private float CalcAngleToPoint(Transform point)
    {
        Vector3 vectorToPoint = transform.position - point.position;
        return (180 - Vector3.Angle(transform.forward, vectorToPoint));
    }
    private float PlayerDetectionModificator()
    {
        if (!pcRef) return 1;
        return pcRef.GetPc().GetPlayerDetectionLevel();
    }
}
