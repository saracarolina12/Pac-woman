using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Zombie : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator ani;
    public Quaternion angulo;
    public float grado;
    private Vector3 playerCurrPos;

    public GameObject target;
    public bool atacando;

    void Start()
    {
        ani = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Comportamiento_Enemigo();
    }

    public void Final_Ani(){
        ani.SetBool("attack", false);
        atacando = false;
    }

    public void Comportamiento_Enemigo(){
        if(Vector3.Distance(transform.position, target.transform.position) > 5) //if the player is out of sight of the enemy
        {
            ani.SetBool("run", false);
            cronometro += 1*Time.deltaTime;
            if(cronometro >= 4){
                rutina = Random.Range(0,2); //(min,max)
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0:
                    ani.SetBool("walk", false);
                    break;
                case 1:
                    grado = Random.Range(0,360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++; 
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward*1*Time.deltaTime);
                    ani.SetBool("walk", true);
                    break;
            }
        }else{ //enemy follows the player
            if(Vector3.Distance(transform.position, target.transform.position) > 1 && !atacando){
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                
                if (IsPlayerVisible()) {
                    ani.SetBool("walk", false);
                    ani.SetBool("run", true);
                    transform.Translate(Vector3.forward*2*Time.deltaTime);
                }

                ani.SetBool("attack", false);
            }else{ //attack
                if (IsPlayerVisible()) {
                    Debug.Log("attack");
                    ani.SetBool("walk", false);
                    ani.SetBool("run", false);

                    ani.SetBool("attack", true);
                    atacando = true;
                    //Final_Ani();
                }
            }
        }
    }

    bool IsPlayerVisible() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            // Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
            // Debug.Log("Did Hit" + hit.transform.gameObject.tag);
            return (hit.transform.gameObject.tag == "Player");
        }
        return false;
    }
}
