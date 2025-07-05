using TMPro;
using UnityEngine;

public class BarraEnergia : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI tm;
    [SerializeField] public AtaqueEspada ae;
    [SerializeField] public RectTransform rt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tm.text = $"{ae.energia:F0} / 100";
        rt.localScale = new Vector3(ae.energia / 100, 1f, 1f);
    }
}
