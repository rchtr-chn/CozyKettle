using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadTeaShopScene()
    {
        SceneManager.LoadScene("TeaShopScene");
    }

    public void LoadStartMenuScene()
    {
        SceneManager.LoadScene("StartMenuScene");
    }
}
