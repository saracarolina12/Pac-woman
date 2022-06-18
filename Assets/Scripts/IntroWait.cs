using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroWait : MonoBehaviour
{
    public float waitTime;
    void Start()
    {
        StartCoroutine(Wait());       
    }

    IEnumerator Wait(){
        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(1); //0,1,... etc
    }
}
