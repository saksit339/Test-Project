using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Animations;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float cameraSpeed;
    [SerializeField]
    private float hp;
    [SerializeField]
    private float speed;
    [SerializeField]
    private FixedJoystick joystick;
    [SerializeField]
    private GameObject magicBall;
    [SerializeField]
    private GameObject aim;
    
    public Vector2 moveDirection;
    public Vector2 lastDirection;
    
    private void Start()
    {
        hp = 100;
        lastDirection = Vector2.right;
    }

    private void Update()
    {
        Move();
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(transform.position.x, transform.position.y,-5), cameraSpeed * Time.deltaTime);
    }

    private void Move()
    {
        moveDirection = joystick.Direction;
        if (moveDirection != Vector2.zero)
        {
            transform.Translate(moveDirection*speed*Time.deltaTime);
            lastDirection = moveDirection;
            
            Aim();
        }
        
    }
    
    private void Aim()
    {
        Vector2 aimDirection = new Vector2 (transform.position.x, transform.position.y) + moveDirection;       
        aim.transform.position = aimDirection;
        
    }
    public void Attack()
    {        
        GameObject newMagicBall = Instantiate(magicBall, transform.position, transform.rotation);
        newMagicBall.GetComponent<MagicBall>().moveDirection = lastDirection;
    }
    public void TakeDamage(float damage)
    {
        if (hp - damage <= 0)
        {
            //dead
        }else
        {
            hp -= damage;
        }
    }


}
