using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);

        
        // Faz Atacar
        //if (agent.remainingDistance <= agent.stoppingDistance)
        //{
        //    player.GetComponent<Target>().TakeDamage(10);
        //}
    }
}
