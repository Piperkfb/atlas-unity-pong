using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollen : MonoBehaviour
{
    private Rigidbody2D _ridgy;
    public AudioSource CollectSFX;
    public float speed = 7000;
    public GameHandler GH;
    public GameObject pollen;
    // Start is called before the first frame update
    
    private void Awake()
    {
        _ridgy = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        AddStartingForce();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void AddStartingForce()
    {
        float x = Random.value < 0.5f ? -1.0f : 1.0f;
        float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f);
        Vector2 direction = new Vector2(x,y);

        _ridgy.AddForce(direction * this.speed * 3);
    }
    protected void OnTriggerEnter2D(Collider2D boink)
    {
        if (boink.gameObject.CompareTag("Paddle"))
        {
            if (this.gameObject.transform.localPosition.x < 0)
            {
                if (GH.specialBarLeft < 5)
                {
                    GH.specialBarLeft += 1;
                }
            }
            else 
            {
                if (GH.specialBarRight < 5)
                {
                    GH.specialBarRight += 1;
                }
            }
            CollectSFX.Play();
            Destroy(pollen);
        }
        if (boink.gameObject.CompareTag("Wall"))
        {

            Vector2 VelSave = _ridgy.velocity;
            VelSave.y = -VelSave.y;
            _ridgy.velocity = VelSave;
            // sound FX
            // SoundFX.clip = SFXWall;
            // SoundFX.Play();
            //anmiation
        }
        if (boink.gameObject.CompareTag("Goal"))
        {
            Debug.Log("Pollen to goal");
            Vector2 VelSave = _ridgy.velocity;
            float xneg = VelSave.x < 0 ? 1.0f : -1.0f;
            VelSave.x = -VelSave.x + xneg;
            _ridgy.velocity = VelSave;
        }
    }
}
