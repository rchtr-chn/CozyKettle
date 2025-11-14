using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

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


    public void DeleteSaveData(Button delButton)
    {
        PlayerStaticData.SetMoney(500f);
        foreach (BrewingStaticData.Items item in System.Enum.GetValues(typeof(BrewingStaticData.Items)))
        {
            BrewingStaticData.SetItemQuantity(item, 10);
        }
        PlayerPrefs.SetInt("IsNewGame", 1);
        PlayerPrefs.DeleteAll();

        delButton.interactable = false;

        if (SceneManager.GetActiveScene().name == "TeaShopScene")
        {
            SceneController.Instance.LoadStartMenuScene();
        }

        Debug.Log("Save data deleted.");
    }

    public void SaveGame()
    {
        PlayerPrefs.SetFloat("PlayerMoney", PlayerStaticData.GetMoney());
        foreach(BrewingStaticData.Items item in System.Enum.GetValues(typeof(BrewingStaticData.Items)))
        {
            int quantity = BrewingStaticData.GetItemQuantity(item);
            PlayerPrefs.SetInt(item.ToString() + "_Quantity", quantity);
        }
        if(TutorialManager.Instance != null)
        {
            PlayerPrefs.SetInt("IsNewGame", TutorialManager.Instance.GetTutorialValue());
        }
        PlayerPrefs.Save();
        Debug.Log("Game saved.");
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("PlayerMoney"))
        {
            float money = PlayerPrefs.GetFloat("PlayerMoney");
            PlayerStaticData.SetMoney(money);
        }

        foreach (BrewingStaticData.Items item in System.Enum.GetValues(typeof(BrewingStaticData.Items)))
        {
            string key = item.ToString() + "_Quantity";
            if (PlayerPrefs.HasKey(key))
            {
                int quantity = PlayerPrefs.GetInt(key);
                BrewingStaticData.SetItemQuantity(item, quantity);
            }
        }

        if (PlayerPrefs.HasKey("IsNewGame"))
        {
            int tutorialValue = PlayerPrefs.GetInt("IsNewGame");
            bool isNewGame;
            if(tutorialValue == 0)
            {
                isNewGame = false;
            }
            else
            {
                isNewGame = true;
            }
            TutorialManager.Instance.SetTutorial(isNewGame);
        }
        Debug.Log("Game loaded.");
    }
}
