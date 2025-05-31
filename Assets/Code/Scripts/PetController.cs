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
        yield return new WaitForSeconds(1.5f);
        RandomMov();
        //StartCoroutine(Start());
    }

    void Awake()
    {
    
        agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(agent.velocity.magnitude);
        if(agent.velocity.magnitude < 0.1)
        {
            RandomMov();
        }
    }

    void RandomMov()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 20;
        randomDirection += transform.position;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomDirection, out hit, 20, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);

        }

        float dist = Vector2.Distance(agent.transform.position, hit.position);

    }
}
