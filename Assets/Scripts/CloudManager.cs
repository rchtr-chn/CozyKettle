using System;
using UnityEngine;
using UnityEngine.UI;

public class CloudManager : MonoBehaviour
{
    [SerializeField] private Sprite[] _cloudSprites; // Assign cloud sprites in inspector
    [SerializeField] private Transform _cloudGroup; // Assign cloud container in inspector
    [SerializeField] private GameObject _cloudPrefab; // Assign cloud prefab in inspector

    private float elapsed = 0f;
    private float _spawnInterval = 4f;

    private float _minRangeX = -2100f;
    private float _maxRangeX = -500f;

    private float _minOffsetY = -150f;
    private float _maxOffsetY = 200f;

    private void Start()
    {
        int initialCloudCount = UnityEngine.Random.Range(5, 8);
        for (int i = 0; i < initialCloudCount; i++)
        {
            float rangeX = UnityEngine.Random.Range(_minRangeX, _maxRangeX);
            float offsetY = UnityEngine.Random.Range(_minOffsetY, _maxOffsetY);
            Vector3 spawnPosition = new Vector3(rangeX, offsetY, 0f);
            GameObject cloud = Instantiate(_cloudPrefab, _cloudGroup.transform.position + spawnPosition, Quaternion.identity, _cloudGroup);
            Image image = cloud.GetComponent<Image>();
            image.sprite = _cloudSprites[UnityEngine.Random.Range(0, _cloudSprites.Length)];
        }
    }

    private void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= _spawnInterval)
        {
            SpawnCloud();
            elapsed = 0f;
        }
    }

    private void SpawnCloud()
    {
        float offsetY = UnityEngine.Random.Range(_minOffsetY, _maxOffsetY);
        Vector3 spawnPosition = new Vector3(0, offsetY, 0f); // Spawn off-screen to the left
        GameObject cloud = Instantiate(_cloudPrefab, _cloudGroup.transform.position + spawnPosition, Quaternion.identity, _cloudGroup);
        Image image = cloud.GetComponent<Image>();
        image.sprite = _cloudSprites[UnityEngine.Random.Range(0, _cloudSprites.Length)];
    }
}
