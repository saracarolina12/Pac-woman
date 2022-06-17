using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour {
    public void handleOnClickPlayButton() {
        SceneManager.LoadScene("SelectWorldScene");
    }

    public void handleOnClickPacwomanButton() {
        SceneManager.LoadScene("Main-pacwoman");
    }

    public void handleOnClickCodButton() {
        SceneManager.LoadScene("Call-of-duty");
    }
}