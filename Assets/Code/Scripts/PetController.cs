using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    float _lastDestinationTime;
    [SerializeField] GameObject[] Points;
    [SerializeField] GameObject petSprite;

    [SerializeField] float tiempoQuieto = 2f;
    bool moving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Points = GameObject.FindGameObjectsWithTag("Point");

    }

    void Awake()
    {
    
        agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(agent.velocity.x);

        if(agent.velocity.x > 0 )
        {
            petSprite.transform.localScale = new Vector3(-1, petSprite.transform.localScale.y, petSprite.transform.localScale.z);
        }
        if(agent.velocity.x < 0)
        {
            petSprite.transform.localScale = new Vector3(1, petSprite.transform.localScale.y, petSprite.transform.localScale.z);
        }

        if(agent.velocity.magnitude < 0.1 && moving == false)
        {
            StartCoroutine(RandomMov());
        }
    }

    IEnumerator RandomMov()
    {
        moving = true;
        
        yield return new WaitForSeconds(tiempoQuieto);
        
        int number = Random.Range(0,Points.Length);

        Vector3 randomDirection = Points[number].transform.position;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomDirection, out hit, 20, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);

        }

        float dist = Vector2.Distance(agent.transform.position, hit.position);

        moving = false;

    }
}
