using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagerr : MonoBehaviour
{
    public void GoToGameScene()
    {
        SceneManager.LoadScene("Game");
    }
}
