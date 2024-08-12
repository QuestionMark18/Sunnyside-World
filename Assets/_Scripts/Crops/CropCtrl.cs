using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropCtrl : MyMonoBehaviour
{
    [SerializeField] private CropSO _cropSO;
    public CropSO CropSO => _cropSO;

    [SerializeField] private CropGrowth _cropGrowth;
    public CropGrowth CropGrowth => _cropGrowth;

    [SerializeField] private CropDespawn _cropDespawn;
    public CropDespawn CropDespawn => _cropDespawn;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCropSO();
        LoadCropGrowth();
        LoadCropDespawn();
    }

    private void LoadCropSO()
    {
        if (_cropSO != null) return;
        _cropSO = Resources.Load<CropSO>("SO/Crops/" + transform.name);
        Debug.Log(transform.name + ": LoadCropSO", gameObject);
    }

    private void LoadCropGrowth()
    {
        if (_cropGrowth != null) return;
        _cropGrowth = transform.GetComponentInChildren<CropGrowth>();
        Debug.Log(transform.name + ": LoadCropGrowth", gameObject);
    }

    private void LoadCropDespawn()
    {
        if (_cropDespawn != null) return;
        _cropDespawn = transform.GetComponentInChildren<CropDespawn>();
        Debug.Log(transform.name + ": LoadCropDespawn", gameObject);
    }
}
