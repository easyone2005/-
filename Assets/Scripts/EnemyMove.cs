using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private int score = 100;
    [SerializeField]
    private int hp = 2;
    [SerializeField]
    protected float speed = 1f;

    private bool isDamaged = false;
    protected bool isDead = false;
    protected GameManager gameManager = null;
    private Animator animator = null;
    private SpriteRenderer spriteRenderer = null;

    protected virtual void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        if (isDead) return;

        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (transform.position.y < gameManager.MinPosition.y - 2f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;
        
        if (collision.CompareTag("Bullet"))
        {
            if (isDamaged) return;
            isDamaged = true;
            Destroy(collision.gameObject);
            StartCoroutine(Damaged());
            if (hp <= 0)
            {
                isDead = true;
                gameManager.AddScore(score);
                StartCoroutine(Dead());
            }
        }
    }

    private IEnumerator Damaged()
    {
        hp--;
        spriteRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 0f));
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material.SetColor("_Color", new Color(0f, 0f, 0f, 0f));
        isDamaged = false;
    }

    private IEnumerator Dead()
    {
        spriteRenderer.material.SetColor("_Color", new Color(0f, 0f, 0f, 0f));
        animator.Play("Explosion");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
