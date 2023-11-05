using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{

    public int lifes;

    private SpriteRenderer sprite;

    // public GameObject cherry;

    // Start is called before the first frame update
    void Start()
    {
        lifes = 4;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == 11){
            lifes--;
            sprite.color = Color.red;
            SFXController.Instance.SFX("LandGround", 0.7f);
            if(lifes <= 0){
                // Instantiate(cherry, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.layer == 11){
            sprite.color = Color.white;
        }
    }
}
