using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private GameManager gameManager = null;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {        
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        if(transform.position.y > gameManager.MaxPosition.y + 2f)
        {
            Destroy(gameObject);
        }
    }
}
