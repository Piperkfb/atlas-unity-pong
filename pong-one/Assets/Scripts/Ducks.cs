using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ducks : MonoBehaviour
{
    public float speed;
    public GameObject duck;

    private void Awake()
    {
        speed = Random.Range(50, 100);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D boink)
    {
        if (boink.gameObject.CompareTag("Destroyer"))
        {
            Destroy(duck);
        }
    }
}
