using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Placar placar;

    [SerializeField] public TextMeshProUGUI tm;

    public void Setup()
    {
        gameObject.SetActive(true);
        tm.text = "Distância: " + placar.maior + " metros";
    }

    public void Recomecar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Sair()
    {
        SceneManager.LoadScene(0);
    }
}
