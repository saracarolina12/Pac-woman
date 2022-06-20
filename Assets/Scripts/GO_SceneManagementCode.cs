using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GO_SceneManagementCode : MonoBehaviour
{
    public void MenuScene(){
        Debug.Log("menuuuuuuuuuuuuuuuuuuuu");
        SceneManager.LoadScene("SelectWorldScene");
    }

    public void TryAgain(){
        Debug.Log("pcmwn");
        SceneManager.LoadScene("Main-pacwoman");
    }

    public void TryAgain_COD(){
        Debug.Log("cod");
        SceneManager.LoadScene("Call-of-duty");
    }
}
