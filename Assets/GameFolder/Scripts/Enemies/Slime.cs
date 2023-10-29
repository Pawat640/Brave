using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{

    public Transform a,b;

    private bool goRight;
    [Header("Velocity Movement")]

    public float speedMove = 7f;
    // Start is called before the first frame update
    void Start()
    {
        
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

    void Death(){
        Destroy(this.gameObject);
    }
}
