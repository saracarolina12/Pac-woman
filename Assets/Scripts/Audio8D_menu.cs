using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio8D_menu : MonoBehaviour
{
    public AudioSource backgroundAudio;
    private bool up = true;
    public float targetTime =0.5f;

    void Start(){
        backgroundAudio.panStereo = 0.84f;
    }
    void Update()
    {
        targetTime -= Time.deltaTime;
        if (targetTime <= 0.0f)
        {
            Debug.Log(backgroundAudio.panStereo);
            if(backgroundAudio.panStereo <= -0.85f){
                up = true;
            }
            if(backgroundAudio.panStereo >= 0.85f){
                up = false;
            }
            if(up){
                backgroundAudio.panStereo += 0.004f;
            }else{
                backgroundAudio.panStereo -= 0.004f;
            }
            
        }
    }
}
