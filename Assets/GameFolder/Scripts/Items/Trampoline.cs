using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{

    private Animator animator;

    public float _yForce = 22f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 6){
            animator.SetTrigger("jump");
            SFXController.Instance.SFX("Crank", 0.5f);
            other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(
                other.gameObject.GetComponent<Rigidbody2D>().velocity.x,
                _yForce
            );
        }
    }
}
