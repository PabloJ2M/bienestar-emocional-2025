using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class PetController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    float _lastDestinationTime;
    [SerializeField] GameObject[] Points;
    [SerializeField] SpriteRenderer petSprite;
    [SerializeField] List<Mascota> mascotas;

    [SerializeField] string mascota;
    [SerializeField] float tiempoQuieto = 2f;
    [SerializeField] Animator anim;
    RuntimeAnimatorController animatorController;
    bool moving = false;
    bool jumping = false;
    float normalHeight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        for (int i = 0; i < mascotas.Count; i++) 
        {
            if (mascotas[i].name == mascota)
            {
                petSprite.sprite = mascotas[i].sprite;
                animatorController = mascotas[i].animator;
                petSprite.transform.position += Vector3.up * mascotas[i].yOffset;
            }
        }

        anim = GetComponentInChildren<Animator>();

        anim.runtimeAnimatorController = animatorController;
        
        Points = GameObject.FindGameObjectsWithTag("Point");
        normalHeight = transform.position.y;

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

        if (agent.remainingDistance < 0.15 && moving == false)
        {
            StartCoroutine(RandomMov());
        }

        Debug.Log(agent.velocity.y);

        if(transform.position.y > normalHeight + 1 && jumping == false)
        {
            anim.SetTrigger("Jump");
            jumping = true;
        }

        //if( agent.velocity.y > 0 && jumping == false)
        //{
        //    anim.SetTrigger("Jump");
        //    jumping = true;
        //}
    }

    IEnumerator RandomMov()
    {
        anim.SetBool("Run", false);
        
        moving = true;

        jumping = false;
        
        yield return new WaitForSeconds(tiempoQuieto);

        anim.SetBool("Run", true);

        int number = Random.Range(0,Points.Length);

        Vector3 randomDirection = Points[number].transform.position;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomDirection, out hit, 20, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
            

        }

        float dist = Vector2.Distance(agent.transform.position, hit.position);

        moving = false;

        //StartCoroutine(RandomMov());

    }
}

[System.Serializable]
public class Mascota
{
    public Sprite sprite;
    public RuntimeAnimatorController animator;
    public string name;
    public float yOffset;

}
