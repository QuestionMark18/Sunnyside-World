using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.Rendering;
using UnityEngine;

public class FirebaseManager : MyMonoBehaviour
{
    private static FirebaseManager _instance;
    public static FirebaseManager Instance => _instance;

    private DatabaseReference _dbReference;
    public DatabaseReference DbReference => _dbReference;

    private DateTime _serverDateTime;
    public DateTime ServerDateTime => _serverDateTime;

    private long _serverUnixTime;
    public long ServerUnixTime => _serverUnixTime;

    private FirebaseUser _user;
    public FirebaseUser User => _user;

    protected override void Awake()
    {
        if (_instance != null) return;
        _instance = this;

        base.Awake();

        _user = FirebaseAuth.DefaultInstance.CurrentUser;
        Debug.Log("Login user: " + _user.UserId);

        FirebaseApp app = FirebaseApp.DefaultInstance;
        _dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void Start()
    {
        /*TilebaseDetail tile = new(1, 1, ECellState.Ground);
        //Debug.Log(tile.ToJson());
        WriteData("2", tile.ToJson());*/
    }

    private async void Update()
    {
        await GetServerTime();
    }

    private async Task GetServerTime()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(".info/serverTimeOffset");
        var task = await reference.GetValueAsync();

        if (task.Exists && long.TryParse(task.Value.ToString(), out long serverTimeOffset))
        {
            DateTime serverTime = DateTime.UtcNow.AddMilliseconds(serverTimeOffset);
            _serverDateTime = serverTime;
            _serverUnixTime = ToUnixTime(serverTime);
            //Debug.Log("Server datetime: " + _serverDateTime);
            //Debug.Log("Server unixtime: " + _serverUnixTime);
        }
    }

    public void WriteDataToFirebase(object data, string path)
    {
        _dbReference.Child($"User/{_user.UserId}/{path}").SetValueAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted) Debug.Log("Luu du lieu thanh cong");
            else Debug.Log("Luu du lieu that bai:" + task.Exception);
        });
    }

    public void ReadDataFromFirebase()
    {
        Debug.Log("test");
        _dbReference.Child("User").Child(_user.UserId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Debug.Log("Doc du lieu thanh cong: " + snapshot.Value.ToString());
            }
            else Debug.Log("Doc du lieu that bai: " + task.Exception);
        });
    }

    public long ToUnixTime(DateTime dateTime)
    {
        return new DateTimeOffset(dateTime, TimeSpan.Zero).ToUnixTimeSeconds();
    }

    public DateTime FromUnixTime(long unixTime)
    {
        return DateTimeOffset.FromUnixTimeSeconds(unixTime).UtcDateTime;
    }
}
