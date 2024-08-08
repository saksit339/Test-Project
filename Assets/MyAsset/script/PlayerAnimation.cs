using UnityEditor.Animations;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Player Player;
    [SerializeField]
    private GameObject avata;

    private void Start()
    {
        animator = avata.GetComponent<Animator>();
        Player = GetComponent<Player>();
    }

    private void Update()
    {
        Vector2 dir = Player.lastDirection;
        Vector2 scal = new Vector2(1,1);
        if (dir.x > 0.2)
        {
            scal.x = 1;
        }else if (dir.x < -0.2)
        {
            scal.x = -1;
        }

        avata.transform.localScale = scal;
        SetAnimetion();
    }

    void SetAnimetion()
    {

        Vector2 dir = Player.lastDirection;

        animator.SetFloat("x", dir.x);
        animator.SetFloat("y", dir.y);

        if (Player.moveDirection == Vector2.zero)
        {
            animator.SetBool("isRun", false);
        }
        else
        {
            animator.SetBool("isRun", true);
        }
    }
}
