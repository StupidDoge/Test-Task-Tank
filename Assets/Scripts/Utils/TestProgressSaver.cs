using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Services.PersistentProgress;
using UnityEngine;

public class TestProgressSaver : MonoBehaviour, ISavedProgress
{
    private const int MinValue = 0;
    private const int MaxValue = 1000;

    private int RandomValue => Random.Range(MinValue, MaxValue);
    private ISaveLoadService _saveLoadService;

    private void Awake()
    {
        _saveLoadService = ServiceLocator.Instance.Single<ISaveLoadService>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _saveLoadService.SaveProgress();
        }
    }

    public void LoadProgress(PlayerProgress playerProgress)
    {
        Debug.Log("LOADED VALUE = " + playerProgress.TestValue);
    }

    public void UpdateProgress(PlayerProgress playerProgress)
    {
        playerProgress.TestValue = RandomValue;
        Debug.Log("SAVED VALUE = " + playerProgress.TestValue);
    }
}
