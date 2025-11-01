using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public void LoadTeaShopScene()
    {
        SceneManager.LoadScene("TeaShopScene");
    }
}
