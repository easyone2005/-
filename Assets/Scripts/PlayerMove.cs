using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float fireRate = 0.2f;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private Transform bulletPosition = null;
    [SerializeField]
    private GameObject bulletPrefab = null;

    private Rigidbody2D rigid = null;
    private Vector2 targetPosition = Vector2.zero;
    private GameManager gameManager = null;
    private SpriteRenderer spriteRenderer = null;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Fire());
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.x = Mathf.Clamp(targetPosition.x, 
                gameManager.MinPosition.x, gameManager.MaxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y,
                gameManager.MinPosition.y, gameManager.MaxPosition.y);
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, targetPosition, speed * Time.deltaTime);
        }
    }
    private IEnumerator Fire()
    {
        while (true)
        {
            GameObject newBullet = Instantiate(bulletPrefab, bulletPosition);
            newBullet.transform.position = bulletPosition.position;
            newBullet.transform.SetParent(null);
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDamaged) return;
        StartCoroutine(Damage());
    }

    private bool isDamaged = false;

    private IEnumerator Damage()
    {
        gameManager.Dead();
        isDamaged = true;
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
        isDamaged = false;
    }
}
