using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostMove : MonoBehaviour
{

    public void Move(int x, int y)
    {
        transform.position = new Vector3(x + 1.5f, y + 1, 1);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Pacman")
            SceneManager.LoadScene("Game");
    }
}
