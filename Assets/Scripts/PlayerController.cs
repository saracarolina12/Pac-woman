using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject redPanel;
    [SerializeField] private AudioSource ouch;
    [SerializeField] private AudioSource wakawaka;
    [SerializeField] private AudioSource winGame;
    [SerializeField] private AudioSource gameOver;
    public GameObject[] thisZombie;
    public GameObject selector; //selected in the editor
    public Transform MYPLAYER;
    public Transform CAMERA;
    public Image[] lifesLeft;
    public TextMeshProUGUI scoreLabel, gameoverText, tryAgainText, menuText;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int cubitos = 12;
    public float speed = 0; // todo lo que declaremos como public lo podremos modificar en Unity
    public int count = 0; // variable para contar los cubitos
    // public GameObject winTextObject;
    public int damage=0;
    private float targetTime = 2.0f;

    // aniadir una cadena fija y la variable de la cuenta de los cubos
    void SetCountText()
    {
        scoreLabel.text = count.ToString();
    }
    void SetDamage(int i){
        Destroy(lifesLeft[i-1]);
    }
    void Start()
    {
        // winTextObject.SetActive(false); // desactivar cuando entre
        rb = GetComponent<Rigidbody>();
    }
    void Update(){
        targetTime -= Time.deltaTime;
        // Debug.Log(targetTime);
        if(transform.position.z < -14.8)  transform.position = new Vector3(6.83f, 0.46f, 7.95f);
        if(transform.position.z > 7.99) transform.position = new Vector3(2.26f,0.56f, -14.74f);
        for (int i = 0; i < 6; i++){
            if(thisZombie[i].transform.position.z < -14.8)  thisZombie[i].transform.position = new Vector3(6.83f, 0.46f, 7.95f);
            if(thisZombie[i].transform.position.z > 7.99)  thisZombie[i].transform.position = new Vector3(2.26f,0.56f, -14.74f);
        }
    }

    void OnMove(InputValue movementValue) // trae la infromacion que hace el usuario con teclas o joystick
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
        if (movementX > 0) {
            CAMERA.parent = null;
            MYPLAYER.transform.rotation = Quaternion.Euler (0.0f, 90, 0); //derecha
            CAMERA.transform.SetParent(MYPLAYER.transform, true);
        }
        if (movementX < 0){
            CAMERA.parent = null;
            MYPLAYER.transform.rotation = Quaternion.Euler (0.0f, -90, 0);//izquierda
            CAMERA.transform.SetParent(MYPLAYER.transform, true);
        }
        if (movementY > 0){
            CAMERA.parent = null;
            MYPLAYER.transform.rotation = Quaternion.Euler (0.0f, 0, 0);//arriba
            CAMERA.transform.SetParent(MYPLAYER.transform, true);
        }
        if (movementY < 0){
            CAMERA.parent = null;
            MYPLAYER.transform.rotation = Quaternion.Euler (0.0f, 180, 0); //abajo
            CAMERA.transform.SetParent(MYPLAYER.transform, true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY); // coords donde vamos a mover el objeto
        rb.AddForce(movement * speed); // causa el movimiento
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy")){ 
            if (targetTime <= 0.0f)
            {
                targetTime = 2.0f;
                //Debug.Log(damage);
                if(damage < 2){
                    if(!ouch.isPlaying){
                        ouch.Play();
                    }
                    damage++;
                    SetDamage(damage);
                }else{
                    if(!ouch.isPlaying){
                        ouch.Play();
                    }
                    SetDamage(3);
                     //Red Screen
                    var color = redPanel.GetComponent<Image>().color;
                    color.a = 0.8f ;
                    redPanel.GetComponent<Image>().color = color;
                    //Show Game Over Text
                    gameoverText.color = new Color32(236, 207, 97, 255);
                     //SHOW BUTTONS GAME OVER
                    tryAgainText.color = new Color32(221,218,205,255);
                    menuText.color = new Color32(221,218,205,255);
                    // ouch.Stop();
                    wakawaka.Stop();
                    winGame.Stop();
                    if(!gameOver.isPlaying){
                        gameOver.Play();
                    }
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    Debug.Log("Game over");
                    //audio game over displays
                }
            }
        }
        else if(other.CompareTag("Collectible")){
            other.gameObject.SetActive(false); // SetActive dice si va a estar activo o no en el juego. solo se esta ocultando.
            if (!wakawaka.isPlaying)
            {
                wakawaka.Play();
            }
            count++;
            SetCountText();
            if (count >= cubitos) {
                ouch.Stop();
                wakawaka.Stop();
                if(!winGame.isPlaying){
                    winGame.Play();
                }
                // winTextObject.SetActive(true);
            }
        }
    }
}