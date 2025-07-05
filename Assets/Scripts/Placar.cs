using TMPro;
using UnityEngine;

public class Placar : MonoBehaviour
{
    private TextMeshProUGUI tm;
    public int maior = -1;

    [SerializeField] public Transform playerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tm = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.y > maior)
        {
            maior = (int)playerTransform.position.y;
            tm.text = maior.ToString() + " m";
        }
    }
}
