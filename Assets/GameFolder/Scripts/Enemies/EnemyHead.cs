using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == 6){
            anim.gameObject.GetComponent<Slime>().speedMove = 0f;
            anim.Play("death");
            other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(
                other.gameObject.GetComponent<Rigidbody2D>().velocity.x,15f);
        }
    }
}
