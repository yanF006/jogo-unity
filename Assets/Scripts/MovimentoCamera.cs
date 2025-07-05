using UnityEngine;

public class MovimentoCamera : MonoBehaviour
{
    Vector3 posicaoInicial;
    Vector3 velocidade = Vector3.zero;
    public float tempoTransicao = 0.5f;

    [SerializeField] private Transform playerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posicaoInicial = transform.position;
        posicaoInicial.x = posicaoInicial.y = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, playerTransform.position + posicaoInicial, ref velocidade, tempoTransicao);
    }
}
