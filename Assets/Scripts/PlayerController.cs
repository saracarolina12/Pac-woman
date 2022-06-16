using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int cubitos = 12;
    public float speed = 0; // todo lo que declaremos como public lo podremos modificar en Unity
    public int count = 0; // variable para contar los cubitos
    // public TextMeshProUGUI countText;
    // public GameObject winTextObject;

    // aniadir una cadena fija y la variable de la cuenta de los cubos
    // void SetCountText()
    // {
    //     countText.text = "count: " + count.ToString();
    // }

    // Start is called before the first frame update
    void Start()
    {
        // winTextObject.SetActive(false); // desactivar cuando entre
        rb = GetComponent<Rigidbody>();
        // SetCountText();
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
        print("ontriggerEnter!");
        other.gameObject.SetActive(false); // SetActive dice si va a estar activo o no en el juego. solo se esta ocultando.
        count++;
        // SetCountText();

        if (count >= cubitos) {
            // winTextObject.SetActive(true);
        }
    }
}
