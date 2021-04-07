using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    [SerializeField]
    GameObject fire;

    [SerializeField]
    float cadencia = 1.5f;

    [SerializeField]
    float intervaloTiro = 0.050f;

    [SerializeField]
    int vidasInvaders = 10;

    float tempoQuePassou = 0f;

    void Update()
    {
        tempoQuePassou += Time.deltaTime;
        if (tag == "Destrutivel")
        {
            if (tempoQuePassou >= cadencia)
            {
                if(Random.value <= intervaloTiro)
                {
                    Instantiate(fire, transform.position, transform.rotation);
                }
                tempoQuePassou = 0f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(tag == "Destrutivel")
        {
            if (collision.gameObject.tag == "ProjectilAmigo")
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
        } else
        {
            if(collision.gameObject.tag == "ProjectilAmigo")
            {
                vidasInvaders -= 1;

                if (vidasInvaders <= 0)
                {
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }
}
