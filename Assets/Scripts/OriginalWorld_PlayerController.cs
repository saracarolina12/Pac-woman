using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OriginalWorld_PlayerController : MonoBehaviour
{
    public GameObject redPanel;
    public GameObject MYGHOST;
    public Transform MYPLAYER;
    public Transform CAMERA;
    public Material blueGhost;
    public Material whiteGhost;
    [SerializeField] private AudioSource ouch;
    [SerializeField] private AudioSource wakawaka;
    [SerializeField] private AudioSource winGame;
    [SerializeField] private AudioSource gameOver;
    [SerializeField] private AudioSource turnBlue;
    public Image[] lifesLeft;
    public TextMeshProUGUI scoreLabel;
    public TextMeshProUGUI gameoverText;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int cubitos = 12;
    public float speed = 0; // todo lo que declaremos como public lo podremos modificar en Unity
    public int count = 0; // variable para contar los cubitos
    // public GameObject winTextObject;
    public int damage=0;
    private float targetTime = 2.0f;
    private float blueTime = 4.0f;
    private bool isBlue = false;
    private bool countTime = false;
    private int countGO = 1;


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
        // SetCountText();
    }
    void Update(){
        if(countTime == true) {
            blueTime -= Time.deltaTime;
        }
        targetTime -= Time.deltaTime;
        if(transform.position.x < -11.59)  transform.position = new Vector3(11.37f, 0.63f, 1.87f);
        if(transform.position.x > 11.39) transform.position = new Vector3(-11.57f,0.63f, 1.81f);
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
        if(isBlue){
            if(blueTime <= 0.0f){
                blueTime = 4.0f;
                MYGHOST.GetComponent<Renderer>().material = whiteGhost;
                isBlue = false;
                countTime = false;
            }else{
                countTime = true;
                MYGHOST.GetComponent<Renderer>().material = blueGhost;
            }
            

            //collide
            if(other.CompareTag("Enemy")){ 
                //catch ghost and appear it in the 'cage'
                Debug.Log("enemy");
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


            
        }else{
            Debug.Log("false");
            // GetComponent<Renderer>().material = whiteGhost;
            if(other.CompareTag("Enemy")){ 
                if (targetTime <= 0.0f)
                {
                    targetTime = 2.0f;
                    // Debug.Log(damage);
                    if(damage < 2){
                        if(!ouch.isPlaying){
                            ouch.Play();
                        }
                        damage++;
                        SetDamage(damage);
                    }else{
                        //Red Screen
                        var color = redPanel.GetComponent<Image>().color;
                        color.a = 0.8f ;
                        redPanel.GetComponent<Image>().color = color;
                        //Show Game Over Text
                        gameoverText.color = new Color32(236, 207, 97, 255);
                        ouch.Stop();
                        wakawaka.Stop();
                        if(countGO == 1 && !gameOver.isPlaying){
                            gameOver.Play();
                            countGO++;
                        }
                        Debug.Log("Game over");
                        //audio game over displays
                    }
                    isBlue = false;
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
                isBlue = false;
            }
            else if(other.CompareTag("Big-dot")){
                other.gameObject.SetActive(false); 
                if (!turnBlue.isPlaying)
                {
                    wakawaka.Stop();
                    turnBlue.Play();
                }
                count+=5;
                SetCountText();
                if (count >= cubitos) {
                    ouch.Stop();
                    wakawaka.Stop();
                    if(!winGame.isPlaying){
                        winGame.Play();
                    }
                    // winTextObject.SetActive(true);
                }
                isBlue = true;
            }
        }
    }
}
