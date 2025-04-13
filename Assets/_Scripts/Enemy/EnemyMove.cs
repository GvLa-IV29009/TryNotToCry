using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform playerPlace;
    private Animator animator;
    private bool isOnCooldown = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distanse = Vector3.Distance(playerPlace.position, transform.position);
        if (distanse > 3f) { agent.SetDestination(playerPlace.position); }
        else if (!isOnCooldown) { StartCoroutine(CoolDown()); }
    }

    private IEnumerator CoolDown()
    {
        isOnCooldown = true;
        animator.SetTrigger(name: "Hit");
        yield return new WaitForSeconds(1f);
        isOnCooldown = false;
    }
}
