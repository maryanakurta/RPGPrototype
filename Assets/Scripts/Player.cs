using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float Hp;
    public float Damage;
    public float AtackSpeed;
    public float AttackRange = 2;
    public GameObject damagePos;

    public float attackSpeedC;
    public float attackSpeedB;//super attack
    [SerializeField] private Text superAttackText;

    private float lastAttackTime = 0;
    private bool isDead = false;
    public Animator AnimatorController;
    [SerializeField] private Move move;
    [SerializeField] private Slider barh;

    private void Start() {
        AtackSpeed = attackSpeedC;
        barh.maxValue = Hp;
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }

        if (Hp <= 0)
        {
            Die();
            return;
        }

        if (isDead)
        {

        }
        else
        {

            if (Input.GetMouseButtonDown(0))
            {
                attack();
            }

            if (Input.GetKey(KeyCode.G)) {
                attackC();
            }

            attackB();
        }

        barh.value = Hp;
    }

    private void Die()
    {
        if (isDead == false)
        {
            AnimatorController.SetTrigger("Die");
        }
        isDead = true;
        move.noMove = true;

        SceneManager.Instance.GameOver();
    }

    private void attack() {
  

        if (Time.time - lastAttackTime > AtackSpeed)
        {
            lastAttackTime = Time.time;
            AnimatorController.SetTrigger("Attack");
            AtackSpeed = attackSpeedC;
        }
    }

    public void attackN() {
        attackC();
    }

    private void attackC()
    {


        if (Time.time - lastAttackTime > AtackSpeed)
        {
            lastAttackTime = Time.time;
            AnimatorController.SetTrigger("Chan");
            AtackSpeed = attackSpeedC;
        }
    }

    private void attackB() {
        chosB();
    }


    //attack
    public void chos()
    {
        var enemies = SceneManager.Instance.Enemies;
        Enemie closestEnemie = null;

        for (int i = 0; i < enemies.Count; i++)
        {
            var enemie = enemies[i];
            if (enemie == null)
            {
                continue;
            }

            if (closestEnemie == null)
            {
                closestEnemie = enemie;
                continue;
            }

            var distance = Vector3.Distance(damagePos.transform.position, enemie.transform.position);
            var closestDistance = Vector3.Distance(damagePos.transform.position, closestEnemie.transform.position);

            if (distance < closestDistance)
            {
                closestEnemie = enemie;
            }

        }

        if (closestEnemie != null)
        {
            var distance = Vector3.Distance(damagePos.transform.position, closestEnemie.transform.position);
            if (distance <= AttackRange)
            {
                closestEnemie.Hp -= Damage;
            }
            else
            {

            }
        }
    }

    //attack
    public void chosD()
    {
        var enemies = SceneManager.Instance.Enemies;
        Enemie closestEnemie = null;

        for (int i = 0; i < enemies.Count; i++)
        {
            var enemie = enemies[i];
            if (enemie == null)
            {
                continue;
            }

            if (closestEnemie == null)
            {
                closestEnemie = enemie;
                continue;
            }

            var distance = Vector3.Distance(damagePos.transform.position, enemie.transform.position);
            var closestDistance = Vector3.Distance(damagePos.transform.position, closestEnemie.transform.position);

            if (distance < closestDistance)
            {
                closestEnemie = enemie;
            }

        }

        if (closestEnemie != null)
        {
            var distance = Vector3.Distance(damagePos.transform.position, closestEnemie.transform.position);
            if (distance <= AttackRange)
            {
                closestEnemie.Hp -= Damage+Damage;
            }
            else
            {

            }
        }
    }

    //super
    public void chosB()
    {
        var enemies = SceneManager.Instance.Enemies;
        Enemie closestEnemie = null;

        for (int i = 0; i < enemies.Count; i++)
        {
            var enemie = enemies[i];
            if (enemie == null)
            {
                continue;
            }

            if (closestEnemie == null)
            {
                closestEnemie = enemie;
                continue;
            }

            var distance = Vector3.Distance(damagePos.transform.position, enemie.transform.position);
            var closestDistance = Vector3.Distance(damagePos.transform.position, closestEnemie.transform.position);

            if (distance < closestDistance)
            {
                closestEnemie = enemie;
            }

        }

        if (closestEnemie != null)
        {
            var distance = Vector3.Distance(damagePos.transform.position, closestEnemie.transform.position);
            if (Time.time - lastAttackTime > AtackSpeed)
            {
                if (distance <= AttackRange)
            {
                if (Input.GetKey(KeyCode.H))
                {
                    if (Time.time - lastAttackTime > AtackSpeed)
                    {
                        closestEnemie.Hp -= Damage + Damage;
                        lastAttackTime = Time.time;
                        AnimatorController.SetTrigger("Hit");
                        AtackSpeed = attackSpeedB;
                    }
                }
                superAttackText.text = "Press H to super attack";
            }
            else {
                superAttackText.text = "";
            }
            }
        }
    }


}
