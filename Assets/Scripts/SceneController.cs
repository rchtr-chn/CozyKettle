using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadTeaShopScene()
    {
        SceneManager.LoadScene("TeaShopScene");
    }
}
