using System;
using System.Collections;
using UnityEngine;

public class MovimentoPlayer : MonoBehaviour
{
    public float velocidade = 8f;
    public float forcaPulo = 9f;
    public float velocidadeCorrida = 4f;
    public float velocidadeImpulso = 12f;
    public float tempoImpulso = 0.5f;
    public float vida = 100f;
    public float danoInimigo = 10f;
    public float inclinacao = 18f;
    public GameOver gameOver;
    public AudioClip[] clips;

    float movimentoX = 0f;
    private bool estaNoChao = true;
    private bool impulso = false;
    private SpriteRenderer sr;
    private Animator animator;
    private AudioSource audioSrc;

    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public ParticleSystem ps;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        audioSrc = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        movimentoX = Input.GetAxis("Horizontal");

        StartCoroutine(Impulsionar());
        Pular();
        //Correr();
        Morrer();
    }

    // FixedUpdate is called at a fixed interval and is used for physics calculations
    void FixedUpdate()
    {
        if (!impulso)
        {
            Mover();
        }
    }

    void Mover()
    {
        Vector2 movimento = new Vector2(movimentoX * velocidade, rb.linearVelocity.y);
        rb.linearVelocity = movimento;

        transform.localEulerAngles = new Vector3(0, 0, movimentoX * inclinacao);
        if(movimentoX < 0) sr.flipX = true;
        else sr.flipX = false;

        animator.SetFloat("velocidadeX", Math.Abs(movimento.x));
        animator.SetFloat("velocidadeY", movimento.y);

        if(Math.Abs(movimento.y) > 1f)
        {
            animator.SetBool("NoAr", true);
        }

        if (Math.Abs(movimento.x) > 0.5f && estaNoChao && audioSrc.isPlaying == false)
        {
            audioSrc.clip = clips[0];
            audioSrc.loop = true;
            audioSrc.Play();
        }
        else if(estaNoChao)
        {
            audioSrc.Stop();
        }
    }

    void Pular()
    {
        if (Input.GetButtonDown("Jump") && estaNoChao)
        {
            rb.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
            estaNoChao = false;
            audioSrc.clip = clips[1];
            audioSrc.loop = false;
            audioSrc.Play();
        }

        if (Input.GetButtonUp("Jump") && !estaNoChao)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    /*void Correr()
    {
        if (Input.GetKeyDown("left shift"))
        {
            velocidade += velocidadeCorrida;
        }
        if (Input.GetKeyUp("left shift"))
        {
            velocidade -= velocidadeCorrida;
        }
    }*/

    void Morrer()
    {
        if(vida <= 0 || gameObject.transform.position.y <= -9)
        {
            Destroy(gameObject);
            gameOver.Setup();
        }
    }

    IEnumerator Impulsionar()
    {
        if (Input.GetKeyDown("left shift") && !impulso && movimentoX != 0)
        {
            float gravidadeOriginal = rb.gravityScale;

            impulso = true;
            rb.gravityScale = 0f;

            if (movimentoX > 0f)
            {
                rb.linearVelocity = new Vector2(velocidadeImpulso, 0f);
            }
            else if (movimentoX < 0f)
            {
                rb.linearVelocity = new Vector2(-velocidadeImpulso, 0f);
            }

            yield return new WaitForSeconds(tempoImpulso);
            rb.gravityScale = gravidadeOriginal;
            rb.linearVelocity = new Vector2(0f, 0f);
            impulso = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            if (animator.GetBool("NoAr")) animator.SetBool("NoAr", false);
            estaNoChao = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Inimigo"))
        {
            vida -= Time.deltaTime * danoInimigo;
            ps.Play();
        }
    }
}
