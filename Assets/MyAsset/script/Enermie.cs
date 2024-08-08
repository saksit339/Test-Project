using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermie : MonoBehaviour
{
    [SerializeField]
    private float hp = 100;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float attackRang;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private float detecPlayer;
    [SerializeField]
    private Player player;    
    [SerializeField]
    private bool meleeAttack;
    [SerializeField]
    private float damage;
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Animator avataAnimator;
    [SerializeField]
    private float avataScale;
    private float currentScale;
    [SerializeField]
    private bool activeGizmos;

    

    private void OnEnable()
    {
        currentScale = avataScale;
        StartCoroutine(WateToplayer());        
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private bool DistanceToPlsyer(float rang)
    {
        bool inRang = false;

        if (Vector2.Distance(transform.position,player.transform.position) <= rang)
        {
            inRang = true;
        }

        return inRang;
    }

    private IEnumerator WateToplayer()
    {
        avataAnimator.SetBool("isRun",false);
        while (!DistanceToPlsyer(detecPlayer))
        {
            yield return null;
        }
        //Debug.Log("move");
        StartCoroutine(MoveToPlayer());
    }

    private IEnumerator MoveToPlayer()
    {
        avataAnimator.SetBool("isRun", true);
        while (!DistanceToPlsyer(attackRang))
        {
            Vector2 playerDirection = player.transform.localPosition - transform.position;
            transform.Translate(playerDirection * speed * Time.fixedDeltaTime);
            AvataDirection(playerDirection);
            yield return null;
        }
        StartCoroutine(AttackPlayer());
    }

    IEnumerator AttackPlayer()
    {
        avataAnimator.SetBool("isAttack", true);
        Debug.Log("attack");
        yield return new WaitForSeconds(fireRate);
        if(meleeAttack)
        {
            if(DistanceToPlsyer(attackRang))
            {
                player.TakeDamage(damage);
            }
        }else
        {
            GameObject newBullet = Instantiate(bullet);
            newBullet.transform.position = transform.position;
            Vector2 dir = player.transform.position - transform.position;
            newBullet.GetComponent<MagicBall>().moveDirection = dir;
        }
        avataAnimator.SetBool("isAttack", false);
        StartCoroutine(MoveToPlayer());
    }

    public void TakeDamage(float damage)
    {
        if(hp - damage > 0)
        {
            hp -= damage;
        }else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        if (activeGizmos)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, detecPlayer);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRang);
        }
    }

    void AvataDirection(Vector2 dir)
    {
        
        Vector2 scal = new Vector3(avataScale,avataScale,avataScale);
        
        if (dir.x > 0.2)
        {
            scal.x = currentScale;
        }
        else if (dir.x < -0.2)
        {
            scal.x = currentScale*-1;
        }
        avataAnimator.transform.localScale = scal;
    }

}
