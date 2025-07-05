using System.Collections;
using UnityEngine;

public class PerseguicaoInimigo : MonoBehaviour
{
    public float velocidade = 4f;
    public float tempo = 30f;

    GameObject player;
    Transform playerTransform;

    [SerializeField] private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        Destroy(gameObject, tempo);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector2 velocidadeBase = (playerTransform.position - transform.position).normalized * velocidade;
        rb.linearVelocity = new Vector2(velocidadeBase.x, rb.linearVelocity.y);
    }
}