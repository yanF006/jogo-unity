using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject tutorial;

    public void Mostrar()
    {
        tutorial.SetActive(!tutorial.activeSelf);
    }

    public void Jogar()
    {
        SceneManager.LoadScene(1);
    }

    public void Sair()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
