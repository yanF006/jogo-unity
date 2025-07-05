using System.Collections;
using UnityEngine;

public class SpawnerInimigos : MonoBehaviour
{
    public float intervalo = 2f;

    [SerializeField] private GameObject inimigoPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(GerarInimigos());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GerarInimigos()
    {
        yield return new WaitForSeconds(intervalo);
        GameObject inimigo = Instantiate(inimigoPrefab, transform.position, Quaternion.identity);
        StartCoroutine(GerarInimigos());
    }
}
