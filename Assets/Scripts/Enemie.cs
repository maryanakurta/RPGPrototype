using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemie : MonoBehaviour
{
    public float Hp;
    public float Damage;
    public float AtackSpeed;
    public float AttackRange = 2;


    public Animator AnimatorController;
    public NavMeshAgent Agent;

    private float lastAttackTime = 0;
    private bool isDead = false;

    //cal speed
    private Vector3 lastPosition;
    private Vector3 currentVelocity;

    [SerializeField] private GameObject particle;
    [SerializeField] private GameObject subEnemy;
    [SerializeField] bool isLargeEnemy;

    private Vector3 pos;


    private void Start()
    {
        SceneManager.Instance.AddEnemie(this);
        Agent.SetDestination(SceneManager.Instance.Player.transform.position);


    }

    private void Update()
    {
        SceneManager manage = GameObject.FindWithTag("GameController").GetComponent<SceneManager>();
        if (manage.cooldownNo)
        {
            Agent.speed = 3.5f;
        }
        else {
            Agent.speed = 0;
            return;
        }
        if(isDead)
        {
            return;
        }

        if (Hp <= 0)
        {
            Die();
            Agent.isStopped = true;
            return;
        }

        var distance = Vector3.Distance(transform.position, SceneManager.Instance.Player.transform.position);
     
        if (distance <= AttackRange)
        {
            transform.LookAt(SceneManager.Instance.Player.transform);
            Agent.isStopped = true;
            if (Time.time - lastAttackTime > AtackSpeed)
            {
                lastAttackTime = Time.time;
                SceneManager.Instance.Player.Hp -= Damage;
                AnimatorController.SetTrigger("Attack");
            }
        }
        else
        {
            Agent.SetDestination(SceneManager.Instance.Player.transform.position);
        }
        //AnimatorController.SetFloat("Speed", Agent.speed);
        //Debug.Log(Agent.speed);

        Vector3 currentPosition = transform.position;
        currentVelocity = (currentPosition - lastPosition) / Time.deltaTime;
        float speedCharacter = currentVelocity.magnitude;
        lastPosition = currentPosition;

        AnimatorController.SetFloat("Speed", speedCharacter);

        pos = new Vector3(gameObject.transform.position.x, 0.18f, gameObject.transform.position.z);

    }



    private void Die()
    {
        //SceneManager.Instance.RemoveEnemie(this);
        isDead = true;
        AnimatorController.SetTrigger("Die");
        //debug.Log("Lose");
        if (isLargeEnemy)
        {
            Debug.Log("Lose");
            particle.SetActive(true);
            //StartCoroutine(waitForSub());
            Vector3 posA = new Vector3(pos.x + 2.74f, pos.y, pos.z - 1.4f);
            Vector3 posB = new Vector3(pos.x - 2.74f, pos.y, pos.z - 1.4f);
            GameObject ga = Instantiate(subEnemy, posA, Quaternion.identity);
            GameObject gc = Instantiate(subEnemy, posB, Quaternion.identity);
            //ga.SetActive(false);
            //gc.SetActive(false);
            //GameObject.FindWithTag("GameController").GetComponent<SceneManager>().AddEnemie(ga.GetComponent<Enemie>());
            //GameObject.FindWithTag("GameController").GetComponent<SceneManager>().AddEnemie(gc.GetComponent<Enemie>());
            //GameObject[] gn = new GameObject[] { ga, gc };
            StartCoroutine(waitForSub());
            //SceneManager.Instance.RemoveEnemie(this);
        }
        else {
            StartCoroutine(waitForLose());
            SceneManager.Instance.RemoveEnemie(this);
        }
        //SceneManager.Instance.RemoveEnemie(this);
    }

    IEnumerator waitForSub() {
        yield return new WaitForSeconds(1f);
        //Vector3 pos = new Vector3(gameObject.transform.position.x, 0.18f, gameObject.transform.position.z);
        //Vector3 posA = new Vector3(pos.x + 0.74f, pos.y, pos.z - 0.4f);
        //Vector3 posB = new Vector3(pos.x - 0.74f, pos.y, pos.z - 0.4f);
        //Instantiate(subEnemy, posA, Quaternion.identity);
        //Instantiate(subEnemy, posB, Quaternion.identity);
        //gn[0].SetActive(true);
        //gn[1].SetActive(true);
        //Instantiate(subEnemy, pos, Quaternion.identity);
        SceneManager.Instance.RemoveEnemie(this);
        Destroy(gameObject);
    }


    IEnumerator waitForLose()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
