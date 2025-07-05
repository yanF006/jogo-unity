using System.Collections;
using UnityEngine;

public class SpawnerChaos : MonoBehaviour
{
    public float distanciaChao = 3f;
    public float distanciaPlayerMin = 3f;
    public float amplitudeDistribuicao = 4f;
    public float amplitudeTamanho = 4f;
    public float frequencia = 3f;

    private float altura = 0f;
    private bool par = false;

    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject chaoPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(altura + distanciaChao - playerTransform.position.y < distanciaPlayerMin)
        {
            altura = altura + distanciaChao;
            par = !par;

            if(par)
            {
                for (int i = 0; i < Random.Range(1, frequencia); i++)
                {
                    int p = i % 2;
                    if (p == 0) p = -1;

                    chaoPrefab.transform.localScale = new Vector3(Random.Range(0.5f, 1f) * amplitudeTamanho, 1f, 1f);

                    Vector3 posicaoChao = new Vector3(Random.Range(0f, 2f) * p * amplitudeDistribuicao, altura, 0f);
                    Instantiate(chaoPrefab, posicaoChao, Quaternion.identity);
                }
            }
            else
            {
                for (int i = 0; i < Random.Range(1, frequencia); i++)
                {
                    chaoPrefab.transform.localScale = new Vector3(Random.Range(0.5f, 1f) * amplitudeTamanho, 1f, 1f);

                    Vector3 posicaoChao = new Vector3(Random.Range(-1f, 1f) * amplitudeDistribuicao, altura, 0f);
                    Instantiate(chaoPrefab, posicaoChao, Quaternion.identity);
                }
            }
        }
    }
}
