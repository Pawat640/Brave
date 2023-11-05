using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    [Header("SFX Scene")]
    public AudioSource[] audioSource;

    public static SFXController Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SFX(string sfxName, float volume){
        switch (sfxName){
            case "PlayerJump":
                audioSource[0].volume = volume;
                audioSource[0].Play();
            break;
            case "Crank":
                audioSource[1].volume = volume;
                audioSource[1].Play();
            break;
            case "Gate":
                audioSource[2].volume = volume;
                audioSource[2].Play();
            break;
            case "Gem":
                audioSource[3].volume = volume;
                audioSource[3].Play();
            break;
            case "DeathEnemy":
                audioSource[4].volume = volume;
                audioSource[4].Play();
            break;
            case "LandGround":
                audioSource[5].volume = volume;
                audioSource[5].Play();
            break;
            case "Door":
                audioSource[6].volume = volume;
                audioSource[6].Play();
            break;
            case "Win":
                audioSource[7].volume = volume;
                audioSource[7].Play();
            break;
            case "Lose":
                audioSource[8].volume = volume;
                audioSource[8].Play();
            break;
            
        }
    }
}
