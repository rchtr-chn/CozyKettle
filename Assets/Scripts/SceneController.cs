using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    public OptionsMenu OptionsMenu;

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
        SceneManager.sceneLoaded += OnTeaShopSceneLoaded;
        SceneManager.LoadScene("TeaShopScene");
    }

    public void LoadStartMenuScene()
    {
        SceneManager.LoadScene("StartMenuScene");
    }

    private void OnTeaShopSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        OptionsMenu = FindFirstObjectByType<OptionsMenu>(FindObjectsInactive.Include);

        SceneManager.sceneLoaded -= OnTeaShopSceneLoaded;
    }

    private void Update()
    {
        if (OptionsMenu != null)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                OptionsMenu.gameObject.SetActive(!OptionsMenu.gameObject.activeSelf);
            }
        }
    }
}
