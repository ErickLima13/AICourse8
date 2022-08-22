using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : MonoBehaviour,IModifiable
{
    private GameObject player;
    private NavMeshAgent agent;

    public GameObject target;

    [Range(1, 100)] public float walkRadius;

    public int health = 50;

    public GameObject[] decalBlood;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();

        animator = GetComponent<Animator>();

        //agent.SetDestination(RandomNavMeshLocation());


    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(player.transform.position);

        //if (agent != null && agent.remainingDistance <= agent.stoppingDistance)
        //{
        //    agent.SetDestination(RandomNavMeshLocation());
        //}

        //Faz Atacar

        if (agent.remainingDistance < agent.stoppingDistance)
        {
            //player.GetComponent<IDamageable>().ApllyDamage(10);
        }
    }

    private Vector3 RandomNavMeshLocation()
    {
        agent.speed = 1;
        Vector3 finalPosistion = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * walkRadius;
        randomPosition += transform.position;

        if(NavMesh.SamplePosition(randomPosition,out NavMeshHit hit, walkRadius,1))
        {
            finalPosistion = hit.position;
        }

        return finalPosistion;
    }

    public void HealthChange(int value)
    {
        animator.Play("Hit");

        health -= value;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        int n = Random.Range(1, decalBlood.Length);
        GameObject effectGO = Instantiate(decalBlood[n], transform.position, Quaternion.identity);
        effectGO.SetActive(true);
        //gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
