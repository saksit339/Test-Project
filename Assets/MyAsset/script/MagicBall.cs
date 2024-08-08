using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    public float speed;
    public Vector2 moveDirection;
    public string targetTag;
    public float damage = 10;

    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,5);  

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection*speed*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            if (targetTag == "Player")
            {
                collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            }
            if (targetTag == "Enermie")
            {
                collision.gameObject.GetComponent<Enermie>().TakeDamage(damage);
            }
            
            Destroy(gameObject);

        }
    }

}
