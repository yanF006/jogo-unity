using TMPro;
using UnityEngine;

public class BarraVida : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI tm;
    [SerializeField] public MovimentoPlayer mp;
    [SerializeField] public RectTransform rt;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tm.text = $"{mp.vida:F0} / 100";
        rt.localScale = new Vector3(mp.vida / 100, 1f, 1f);
    }
}
