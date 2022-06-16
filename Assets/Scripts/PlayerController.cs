using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Image[] lifesLeft;
    public TextMeshProUGUI scoreLabel;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int cubitos = 12;
    public float speed = 0; // todo lo que declaremos como public lo podremos modificar en Unity
    public int count = 0; // variable para contar los cubitos
    // public GameObject winTextObject;
    public int damage=0;

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
        Debug.Log(transform.position.x);
        if(transform.position.z < -14.8)  transform.position = new Vector3(6.83f, 0.46f, 7.95f);
        if(transform.position.z > 7.99) transform.position = new Vector3(2.26f,0.56f, -14.74f);
    }

    void OnMove(InputValue movementValue) // trae la infromacion que hace el usuario con teclas o joystick
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY); // coords donde vamos a mover el objeto
        rb.AddForce(movement * speed); // causa el movimiento
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy")){ 
            Debug.Log(damage);
            if(damage < 2){
                damage++;
                SetDamage(damage);
            }else{
                Debug.Log("Game over");
                //audio game over displays
            }
        }
        else if(other.CompareTag("Collectible")){
            other.gameObject.SetActive(false); // SetActive dice si va a estar activo o no en el juego. solo se esta ocultando.
            count++;
            SetCountText();
            if (count >= cubitos) {
                // winTextObject.SetActive(true);
            }
        }
    }
}
