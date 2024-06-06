using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Extensions;

public class FirebaseProfile : MonoBehaviour
{
    public bool Connected;
    [HideInInspector] private FirebaseAuth auth;
    DatabaseReference dbRef;

    [SerializeField] string email, passWord;
    public dataToSave dataToSave;
    void Awake()
    {
        LoginWithEmailAndGetDrive(email, passWord);
    }

    private void Start()
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            print("click");
            LoadData(ref dataToSave);
        }

    }

    public void SaveData<T>(T classType)
    {
        string json = JsonUtility.ToJson(classType);
        dbRef.Child("users").Child(auth.CurrentUser.UserId).Child(classType.ToString()).SetRawJsonValueAsync(json);
    }

    public void LoadData<T>(ref T classType)
    {
        T copy = classType;
        var myData = new DataWrapper<T>(copy);
        StartCoroutine(LoadDataEnum(myData));
        print(myData.Value.);
        classType = myData.Value;
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private IEnumerator LoadDataEnum<T>(DataWrapper<T> dataWrapper)
    {
        var serverData = dbRef.Child("users").Child(auth.CurrentUser.UserId).Child(dataWrapper.Value.ToString()).GetValueAsync();
        yield return new WaitUntil(predicate: () => serverData.IsCompleted);

        print("processs is complete");

        DataSnapshot snapshot = serverData.Result;
        string jsonData = snapshot.GetRawJsonValue();

        if (jsonData != null)
        {
            print("data found");
            dataWrapper.Value = JsonUtility.FromJson<T>(jsonData);
        }
        else
            print("NO data found");
    }

    //====================================================
    //==========================
    //===========
    public async void CheckEmailExistsAndSignUp(string email, string password)
    {
        auth = FirebaseAuth.DefaultInstance;
        try
        {
            // Tente de créer l'utilisateur avec l'e-mail et le mot de passe fournis
            var signUpTask = await auth.CreateUserWithEmailAndPasswordAsync(email, password);

            // L'utilisateur est inscrit avec succès
            FirebaseUser newUser = signUpTask.User;
            Debug.Log("Inscription réussie pour l'utilisateur : " + newUser.Email);
            Connected = true;
        }
        catch (AggregateException ex)
        {
            foreach (var innerException in ex.InnerExceptions)
            {
                if (innerException is FirebaseException firebaseEx)
                {
                    if (firebaseEx.Message.Contains("EMAIL_EXISTS"))
                    {
                        Debug.LogWarning("L'adresse e-mail existe déjà dans Firebase Authentication.");
                        break;
                    }
                }
            }
        }
    }

    public void LoginWithEmailAndGetDrive(string email, string password)
    {
        auth = FirebaseAuth.DefaultInstance;

        // Connexion avec l'adresse e-mail et le mot de passe
        var signInTask = auth.SignInWithEmailAndPasswordAsync(email, password);
        signInTask.ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Connexion échouée : " + task.Exception);
                return;
            }

            // Connexion réussie, récupération de l'utilisateur depuis la tâche
            Connected = true;
            FirebaseUser user = auth.CurrentUser;
            Debug.Log("Utilisateur connecté : " + user.Email);
        });
    }
    //===========
    //==========================
    //====================================================

    public class DataWrapper<T>
    {
        public T Value { get; set; }

        public DataWrapper(T initialValue)
        {
            Value = initialValue;
        }
    }
}
