using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GO_SceneManagementCode : MonoBehaviour
{
    public void MenuScene(){
        SceneManager.LoadScene("SelectWorldScene");
    }

    public void TryAgain(){
        SceneManager.LoadScene("Main-pacwoman");
    }

    public void TryAgain_COD(){
        SceneManager.LoadScene("Call-of-duty");
    }
}
