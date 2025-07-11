using UnityEngine;

public class AtaqueComum : MonoBehaviour
{
    public float raio;
    public float dano;

    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("mouse 0"))
        {
            Ataque();
        }
    }

    void Ataque()
    {
        Collider2D[] acerto = Physics2D.OverlapCircleAll(transform.position, raio);

        foreach (Collider2D alvo in acerto)
        {
            if (alvo.CompareTag("Inimigo"))
            {
                //alvo.GetComponent<Inimigo>().ReceberDano(dano);
                Destroy(alvo.gameObject);
            }
        }

        animator.SetTrigger("Ataque");
    }
}
