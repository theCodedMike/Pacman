using UnityEngine;
using UnityEngine.SceneManagement;

public class Pacdot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Pacman")
        {
            Destroy(gameObject);
            GameObject[] pacdots = GameObject.FindGameObjectsWithTag("pacdot");
            if (pacdots.Length == 1)
                SceneManager.LoadScene("Game");
        }
    }
}
