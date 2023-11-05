using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{

    private Animator anim;
    public Transform player;
    public float speed;
    private float ang = 0f;
    public float distanceToPlayer = 10f;
    
    private BoxCollider2D box2D;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        box2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector3.Distance(transform.position, player.position) < distanceToPlayer){
            Follow();
            Rot();
        }
    }

    void Follow(){
        float t = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position,new Vector2(player.position.x, player.position.y+0.8f), t);

        if(player.position.x > transform.position.x){
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
        }else{
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
        }
    }

    void Rot(){
        Vector3 dir = player.position - transform.position;

        if(transform.localScale.x == -1f){
            ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 180f;
        }else{
            ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        }

        Quaternion rotation = Quaternion.AngleAxis(ang, Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2f);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer == 6){
            GameController.instance.RestartGame();
        }
        
        if(other.gameObject.layer == 10 || other.gameObject.layer == 11){
            box2D.enabled = false;
            speed = 0f;
            anim.Play("Explosion");
            SFXController.Instance.SFX("DeathEnemy", 0.7f);
        }
    }

    void Destroy(){
        Destroy(this.gameObject);
    }
}
