using Firebase.Database;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CropGrowth : MyMonoBehaviour
{
    [SerializeField] private float _growthTime = 10f;
    [SerializeField] private long _plantedTime;
    [SerializeField] private bool _isFullyGrow = false;
    [SerializeField] private int _growthStage;
    public int GrowthStage => _growthStage;


    [SerializeField] private List<Sprite> _spriteList;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        BePlanted();
    }

    private void Update()
    {
        if (!_isFullyGrow)
        {
            GrowthCrop();
        }
    }

    public void BePlanted()
    {
        _plantedTime = FirebaseManager.Instance.ServerUnixTime;
        _growthStage = 1;
        _isFullyGrow = false;
        _spriteRenderer.sprite = _spriteList[0];
    }

    private bool GrowthCrop()
    {
        long serverTime = FirebaseManager.Instance.ServerUnixTime;

        long timeSincePlanted = serverTime - _plantedTime;
        CheckGrowthStage(timeSincePlanted);
        _isFullyGrow = timeSincePlanted >= _growthTime;
        return _isFullyGrow;
    }

    private void CheckGrowthStage(long timeSincePlanted)
    {
        int stage = (int)(timeSincePlanted/(_growthTime/3)) + 1;
        if (stage > 4) stage = 4;
        UpdateGrowthStage(stage);
    }

    private void UpdateGrowthStage(int stage)
    {
        _growthStage = stage;
        //Debug.Log("UpdateGrowthStage:" + _growthStage);
        _spriteRenderer.sprite = _spriteList[_growthStage-1];
    }

    public async Task LoadPlantedTimeFromFirebase()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        var task = await reference.Child("User").Child(FirebaseManager.Instance.User.UserId).GetValueAsync();
        if (task.Exists)
        {
            long plantedTime = (long)task.Value;
            Debug.Log(plantedTime);
        }
    }
}
