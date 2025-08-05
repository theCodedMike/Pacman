 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

/// <summary>
/// 豆子销毁
/// </summary>
public class Done_Pacdot : MonoBehaviour {
    //public int num = 330;
    void OnTriggerEnter2D(Collider2D co)
    {
       
        if (co.name == "Done_Pacman")
        {
            Destroy(gameObject);
            GameObject[] pacdots = GameObject.FindGameObjectsWithTag("pacdot");
            //num--;

            if (pacdots.Length == 1)
            {
                EditorSceneManager.LoadScene("Done_game");
            }
        }

    }
}
