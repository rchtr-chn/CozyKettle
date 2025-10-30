using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip BackgroundMusic;

    public AudioClip CustomerArrivalSFX;
    public AudioClip OrderPlacedSFX;
    public AudioClip BeverageServedSFX;
    public AudioClip CustomerSatisfiedSFX;
    public AudioClip CustomerUnsatisfiedSFX;
    public AudioClip CashRegisterSFX;

    public AudioClip PlantSeedSFX;
    public AudioClip WateringCanSFX;
    public AudioClip HarvestingSFX;

    public AudioClip DispenserBeepSFX;
    public AudioClip PouringTeaSFX;
    public AudioClip FrenchPressSFX;
    public AudioClip WaterPourSFX;
    public AudioClip MatchaWhiskSFX;
    public AudioClip BoilingWaterSFX;
    public AudioClip MinigameSuccessSFX;
    public AudioClip LockSFX;

    public AudioClip OpenBookSFX;
    public AudioClip PageFlipSFX;

    public AudioClip UIClickSFX;
    public AudioClip BuyItemSFX;
    public AudioClip StartOfDaySFX;
    public AudioClip EndOfDaySFX;
    public AudioClip ClickOnJarSFX;
    public AudioClip PhoneClickSFX;

    public AudioClip rainSFX;

    [Header("Audio Sources")]
    public AudioSource MusicSource;
    public AudioSource SFXSource;

    [Header("Instance")]
    public static SoundManager Instance;

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

    public void PlayMusic()
    {
        MusicSource.clip = BackgroundMusic;
        MusicSource.loop = true;
        MusicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void StartChargingSFX(AudioClip clip)
    {
        SFXSource.clip = clip;
        SFXSource.loop = true;
        SFXSource.Play();
    }

    public void StopChargingSFX()
    {
        SFXSource.Stop();
        SFXSource.loop = false;
        SFXSource.clip = null;
    }
}
