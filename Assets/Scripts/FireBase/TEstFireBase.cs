using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFireBase : MonoBehaviour
{
    public dataToSave dts;
    public nbFamililier familier;
    public string userId;
    DatabaseReference dbRef;
    ConnectAndLogin login;
    private void Start()
    {
        login = GetComponent<ConnectAndLogin>();
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        SaveDataFn();

        //SaveClassData(familier, "pop/fsdf");
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            print("click");
            SaveDataFn();
        }

    }

    public void SaveDataFn()
    {
        string json = JsonUtility.ToJson(dts);
        dbRef.Child("users").Child(login.auth.CurrentUser.UserId).SetRawJsonValueAsync(json);
    }
}

[Serializable]
public class dataToSave
{
    public string userName;
    public int totalCoins;
    public int crrLevel;
    public int hightScore;
    public List<int> objects = new List<int>();
}

[Serializable]
public class nbFamililier
{
    public int nbFamilier;
}
