using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public int totalGems;
    public GameObject[] UIGems;

    public GameObject player;
    [Header("Camera System")]
    public CameraFollow cam;
    [Header("GameObject")]
    public Transform focusGate;

    public static GameController instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateUIGems(){
        totalGems++;
        switch(totalGems){
            case 1:
            UIGems[0].SetActive(true);
            break;
            case 2:
            UIGems[1].SetActive(true);
            break;
            case 3:
            UIGems[2].SetActive(true);
            break;
        }

        if(totalGems >= 3){
            camChangeFocus(focusGate, 2f);
            StartCoroutine(ieDisableGate(1f));
        }
    }

    public void camChangeFocus(Transform transform, float time){
        if(cam != null){cam.target = transform;}
        player.GetComponent<Player>().isPlayerStopped = true;
        StartCoroutine(ieReturnFocusPlayer(time));
    }

    IEnumerator ieReturnFocusPlayer(float time){
        yield return new WaitForSeconds(time);
        if(player != null){cam.target = player.transform;}
        player.GetComponent<Player>().isPlayerStopped = false;
    }

    IEnumerator ieDisableGate(float time){
        yield return new WaitForSeconds(time);
        focusGate.GetComponent<Animator>().Play("explosion");
        SFXController.Instance.SFX("Door", 0.7f);
        yield return new WaitForSeconds(time);
        focusGate.gameObject.SetActive(false);
    }
}
