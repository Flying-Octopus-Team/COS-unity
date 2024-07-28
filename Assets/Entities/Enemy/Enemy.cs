using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float detectionRange;
    [SerializeField] private bool enabledAI;
    private NavMeshAgent nvmAgent;

    [SerializeField] private PlayerReference pcRef;
    bool isPlayerInSight = false;
    float playerLastContactTimestamp = - 10;


    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip monsterScream;


    private void Start()
    {
        nvmAgent= GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(enabledAI == false) return;
        RaycastHit hit;
        PlayerController pc = pcRef.GetPc();

        if (pc != null)
        {
            Vector3 direction = pc.transform.position - transform.position;
            if (Physics.Raycast(transform.position, direction, out hit, detectionRange))
            {
                if(hit.transform.gameObject.GetComponent<PlayerController>() != null)
                {
                    Debug.DrawRay(transform.position, direction * detectionRange, Color.green);
                    playerLastContactTimestamp = Time.time;
                }
                Debug.DrawRay(transform.position, direction * detectionRange, Color.yellow);
            }
            else
            {
                Debug.DrawRay(transform.position, direction * detectionRange, Color.white);
            }
        }

        isPlayerInSight = playerLastContactTimestamp >= Time.time - 5;

        if(isPlayerInSight)
        {
            nvmAgent.SetDestination(pcRef.GetPc().transform.position);
        }
        else if(nvmAgent.remainingDistance < 1 && (Time.time - playerLastContactTimestamp) < 30)
        {
            POI[] poi = GameObject.FindObjectsOfType<POI>();
            int randomPoint = Random.Range(0, poi.Length);
            nvmAgent.SetDestination(poi[randomPoint].gameObject.transform.position);
        }
        else
        {
            Debug.Log("Adrenaline burst");
            nvmAgent.SetDestination(pcRef.GetPc().transform.position);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange* PlayerDetectionModificator());
    }

    private float CalcAngleToPoint(Transform point)
    {
        Vector3 vectorToPoint = transform.position - point.position;
        return (180 - Vector3.Angle(transform.forward, vectorToPoint));
    }
    private float PlayerDetectionModificator()
    {
        if (!pcRef) return 1;
        if (!pcRef.GetPc()) return 1;
        return pcRef.GetPc().GetPlayerDetectionLevel();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) return;
        if (!pcRef) return;
        if (!pcRef.GetPc()) return;

        audioSource.PlayOneShot(monsterScream);
        pcRef.GetPc().PlayerDie();
    }
}