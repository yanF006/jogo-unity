using System;
using Unity.VisualScripting;
using UnityEngine;

public class AtaqueEspada : MonoBehaviour
{
    public float velocidade = 8f;

    private bool lancado = false;
    private bool puxado = false;
    Vector2 direcao = new Vector2(0, 0);

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform playerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 posicaoMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direcao = posicaoMouse.normalized;

        if (!lancado)
        {
            Girar();

            if (Input.GetKeyDown("mouse 0"))
            {
                Lancar();
            }
        }
        else if(Input.GetKeyDown("mouse 1"))
        {
            Puxar();
        }
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

        transform.localPosition = direcao * 1.5f;
        transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg);
    }

    void Lancar()
    {
        transform.parent = null;

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.linearVelocity = direcao * velocidade;

        lancado = true;
    }

    void Puxar()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = (playerTransform.position - transform.position).normalized * velocidade;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (lancado)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                transform.parent = playerTransform;
                lancado = false;
            }

            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
        }
    }
}
