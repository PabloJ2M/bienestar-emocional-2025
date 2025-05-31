using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PetController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    float _lastDestinationTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        RandomMov();
        StartCoroutine(Start());
    }

    void Awake()
    {
    
        agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RandomMov()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 100;
        randomDirection += transform.position;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomDirection, out hit, 100, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);

        }
    }
}
