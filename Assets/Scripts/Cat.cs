using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public enum TYPE
    {
        NORMAL,
        FAT
    }

    public Transform front;

    [Header("Status")]
    public TYPE type;
    public float speed = 7.5f;
    public float full = 5.0f;

    float enery = 0.0f;

    Animator anim;

    void Start()
    {
        float x = Random.Range(-8.5f, 8.5f);
        float y = 30.0f;

        transform.position = new Vector2 (x, y);
        front.localScale = new Vector3(enery / full, 1.0f, 1.0f);

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (enery < full)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;

            if (transform.position.y < -16.0f) 
            {
                GameManager.Instance.OnGameOver();
            }
        }
        else
        {
            float fadeOutSpeed = (transform.position.x > 0.0f? 1.0f : -1.0f) * speed;
            transform.position += Vector3.right * fadeOutSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            enery = Mathf.Clamp(enery + 1.0f, 0.0f, full);
            Destroy(collision.gameObject);
            front.localScale = new Vector3(enery / full, 1.0f, 1.0f);
        }

        if (enery == full)
        {
            anim.SetBool("isFull", true);
            GameManager.Instance.AddScore();
            GetComponent<Collider2D>().enabled = false;

            Destroy(gameObject, 3.0f);
        }
    }
}
