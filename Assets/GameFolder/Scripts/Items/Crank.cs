using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crank : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer spriteRenderer;
    public Sprite spr_crank_down;
    public Transform gate;
    private bool isActive;
    public Transform wayPoint;
    [Header("Control Camera")]

    public CameraFollow cam;

    public Transform player;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    IEnumerator MoveGate()
    {
        if(Vector2.Distance(gate.position, wayPoint.position) > 0.1f){  
            gate.position = Vector2.MoveTowards(gate.position, wayPoint.position, 15f * Time.deltaTime);
            yield return new WaitForSeconds(0.03f);
            if(!SFXController.Instance.audioSource[2].isPlaying){
                SFXController.Instance.SFX("Gate", 0.5f);
            }
             StartCoroutine(MoveGate());
        }else{
            if(player != null){cam.target = player;}
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == 6 && !isActive){
            isActive = true;
            spriteRenderer.sprite = spr_crank_down;
            if(cam != null){
                cam.target = gate;
            }
            SFXController.Instance.SFX("Crank", 0.7f);
            StartCoroutine(MoveGate());
        }
    }
}
