using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInvaders : MonoBehaviour
{

    [SerializeField]
    GameObject[] invasores;

    [SerializeField]
    GameObject[] invasoresIndestrutiveis;

    [SerializeField]
    int nInvasores = 7;

    [SerializeField]
    float xMin = -3f;

    [SerializeField]
    float yMin = -0.5f;

    [SerializeField]
    float xInc = 1f;

    [SerializeField]
    float yInc = 0.5f;

    [SerializeField]
    float probabilidadeDeIndestrutivel = 0.15f;

    [SerializeField]
    float velocidade = 0.00005f;

    float xMinimo, xMaximo;

    bool mov = true;

    bool verticalMov = true;

    void Awake()
    {
        /*
         * "Pega" neste objecto, duplica e coloca-o "naquele" sítio
         */

        float y = yMin;
        for(int line = 0; line < invasores.Length; line++)
        {
            float x = xMin;
            for (int i = 1; i <= nInvasores; i++)
            {
                if(Random.value <= probabilidadeDeIndestrutivel)
                {
                    GameObject newInvader = Instantiate(invasoresIndestrutiveis[line], transform);
                    newInvader.transform.position = new Vector3(x, y, 0f);
                } else
                {
                    GameObject newInvader = Instantiate(invasores[line], transform);
                    newInvader.transform.position = new Vector3(x, y, 0f);
                }
                x += xInc;
            }
            y += yInc;
        }
    }

    private void Start()
    {
        xMaximo = Camera.main.ViewportToWorldPoint(Vector2.one).x - 3.3f;
        xMinimo = Camera.main.ViewportToWorldPoint(Vector2.zero).x + 3.3f;
    }

    private void Update()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, xMinimo, xMaximo);
        transform.position = position;

        if(mov == true)
        {
            transform.position += velocidade * Vector3.right;
            if (position.x == xMaximo && verticalMov == true)
            {
                if (position.y <= -2)
                {
                    verticalMov = false;
                }else
                {
                    transform.position += 0.2f * Vector3.down;
                }
            }
            if (position.x == xMaximo)
            {
                mov = false;
            }
        }
        if (mov == false)
        {
            transform.position -= velocidade * Vector3.right;
            if (position.x == xMinimo && verticalMov == true)
            {
                if (position.y <= -2)
                {
                    verticalMov = false;
                }
                else
                {
                    transform.position += 0.2f * Vector3.down;
                }
            }
            if (position.x == xMinimo)
            {
                mov = true;
            }
        }
    }
}
