using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{

    public Transform a,b;

    private Animator anim;

    private bool goRight;
    [Header("Velocity Movement")]

    public float speedMove = 7f;

    private BoxCollider2D box2D;

    public GameObject objChild;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        box2D = GetComponent<BoxCollider2D>();

        objChild = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        followPoints();
    }

    private void followPoints(){
        if(goRight){
            transform.localScale = new Vector3(1f,1f,1f);
            if(Vector2.Distance(transform.position,b.position) < 0.1f){
                goRight = false;
            }
            transform.position = Vector2.MoveTowards(transform.position, b.position, speedMove * Time.deltaTime);
        }else{
            transform.localScale = new Vector3(-1f,1f,1f);
            if(Vector2.Distance(transform.position,a.position) < 0.1f){
                goRight = true;
            }
            transform.position = Vector2.MoveTowards(transform.position, a.position, speedMove * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer == 10 || other.gameObject.layer == 11){
            box2D.enabled = false;
            speedMove = 0f;
            if(objChild != null){objChild.SetActive(false);}
            anim.Play("death");
            SFXController.Instance.SFX("DeathEnemy", 0.7f);
        }
    }

    void Death(){
        Destroy(this.gameObject);
    }
}
