using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AtaqueEspada : MonoBehaviour
{
    public float distancia = 1.5f;
    public float velocidade = 8f;
    public float distanciaMaxima = 10f;
    public float distanciaMinima = 0.5f;
    public float tempoAtaque = 0.1f;
    public float energia = 100f;
    public Color corIdle = new Color(1f, 0.5f, 0f, 1f);
    public Color corLancado = new Color(1, 0.5f, 0f, 0.25f);

    private bool espada = false;
    private bool atacado = false;
    private bool lancado = false;
    private bool puxado = false;
    Vector2 direcao = new Vector2(0, 0);

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Transform playerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 posicaoMouse = Input.mousePosition - Camera.main.WorldToScreenPoint(playerTransform.position);
        direcao = posicaoMouse.normalized;

        if(Input.GetKeyDown("tab"))
        {
            espada = !espada;
        }

        if (!lancado)
        {
            Girar();

            if (Input.GetKeyDown("mouse 0") && energia >= 10)
            {
                if (espada)
                {
                    StartCoroutine(Atacar());
                }
                else
                {
                    Lancar();
                }
            }
        }
        else if (Input.GetKeyDown("mouse 1") && !espada)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            puxado = true;
        }

        if (puxado)
        {
            Puxar();
        }

        if (lancado || puxado)
        {
            Parar();
        }

        if (energia < 100) energia += Time.deltaTime * 1f;
        else if (energia > 100) energia = 100f;
    }

    void Girar()
    {
        if (direcao.y < 0)
        {
            if (direcao.x >= 0)
            {
                direcao.x = 1;
            }
            else
            {
                direcao.x = -1;
            }

            direcao.y = 0;
        }

        transform.localPosition = direcao * distancia;
        transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg);
    }

    void Lancar()
    {
        transform.parent = null;

        gameObject.GetComponent<Collider2D>().enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.linearVelocity = direcao * velocidade;

        sr.color = corLancado;

        lancado = true;

        energia -= 10f;
    }

    void Puxar()
    {
        rb.linearVelocity = (playerTransform.position - transform.position).normalized * velocidade;
    }

    void Parar()
    {
        float distancia = Vector2.Distance(playerTransform.position, transform.position);

        if(distancia < distanciaMinima)
        {
            transform.parent = playerTransform;
            lancado = false;
            puxado = false;

            sr.color = corIdle;

            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
        else if (distancia > distanciaMaxima)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            puxado = true;
        }
    }

    IEnumerator Atacar()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
        atacado = true;
        sr.color = corLancado;

        energia -= 10f;

        yield return new WaitForSeconds(tempoAtaque);

        atacado = false;
        sr.color = corIdle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(lancado && !collision.gameObject.CompareTag("Inimigo"))
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((lancado || puxado || atacado) && collision.gameObject.CompareTag("Inimigo"))
        {
            Destroy(collision.gameObject);
        }
    }
}
