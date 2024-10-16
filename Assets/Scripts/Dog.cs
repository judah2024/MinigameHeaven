using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public GameObject food;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ShootFood", 0.0f, 0.2f);
    }

    void Update()
    {
        if (!GameManager.Instance.IsPlaying)
            return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float x = Mathf.Clamp(mousePos.x, -8.5f, 8.5f);

        transform.position = new Vector2(x, transform.position.y);
    }
    private void ShootFood()
    {
        float x = transform.position.x;
        float y = transform.position.y + 2.0f;

        Instantiate(food, new Vector2(x, y), Quaternion.identity);
    }
}
