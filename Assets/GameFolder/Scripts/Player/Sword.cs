using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public GameObject player;
    private BoxCollider2D box2D;

    private float angle = 0f;
    private bool isAttack = false;
    public float rotPerUpdate;

    // Start is called before the first frame update
    void Start()
    {
        box2D = GetComponent<BoxCollider2D>();
        box2D.enabled = false;

        rotPerUpdate = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J) && !isAttack){
            isAttack = true;
            SFXController.Instance.SFX("Crank", 0.7f);
            box2D.enabled = true;
        }
        UpdateRotation();
    }

    private void UpdateRotation(){
        if(angle < 90f && isAttack){
            angle += rotPerUpdate;
        }else if(angle >= 90f){
            angle = 0f;
            isAttack = false;
            box2D.enabled = false;
        }

        if(player.transform.localScale.x == 1f){
            AngleSwordRotate(transform, angle);
        }else{
            AngleSwordRotate(transform, -angle);
        }
    }

    Vector3 AngleSwordRotate(Transform trans, float ang){
        if(!player.GetComponent<Player>().isSliding){
            return trans.eulerAngles = Vector3.back * ang;
        }else{
            return trans.eulerAngles = Vector3.back * -ang;
        }
    }
}
