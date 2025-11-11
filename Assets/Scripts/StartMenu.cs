using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button _startButton; // Assign in inspector

    private void Awake()
    {
        _startButton.onClick.AddListener(SceneController.Instance.LoadTeaShopScene);
    }
}
