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
    public enum DATATYPE
    {
        ALLMEDALS_STATS
    }

    [HideInInspector] private FirebaseAuth auth;
    DatabaseReference dbRef;

    [SerializeField] string email, passWord;
    public bool Connected;

    public FirebaseProfil_AllMedals_Stats allMedals_Stats;
    void Awake()
    {
        LoginWithEmailAndGetDrive(email, passWord);
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            LoadData(DATATYPE.ALLMEDALS_STATS);
        }
        
        if (Input.GetKeyUp(KeyCode.Q))
        {
            SaveData(allMedals_Stats);
        }

    }

    public void SaveData<T>(T classType)
    {
        string json = JsonUtility.ToJson(classType);
        dbRef.Child("users").Child(auth.CurrentUser.UserId).Child(classType.ToString()).SetRawJsonValueAsync(json);
    }

    public void LoadData(DATATYPE dataType)
    {
        StartCoroutine(LoadDataEnum(dataType));
        
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private IEnumerator LoadDataEnum(DATATYPE dataType)
    {
        string typeName = "";
        if (dataType == DATATYPE.ALLMEDALS_STATS)
            typeName = allMedals_Stats.ToString();

        print(typeName);
        var serverData = dbRef.Child("users").Child(auth.CurrentUser.UserId).Child(typeName).GetValueAsync();
        yield return new WaitUntil(predicate: () => serverData.IsCompleted);

        print("processs is complete");

        DataSnapshot snapshot = serverData.Result;
        string jsonData = snapshot.GetRawJsonValue();

        if (jsonData != null)
        {
            print("data found");
            if (dataType == DATATYPE.ALLMEDALS_STATS)
                allMedals_Stats = JsonUtility.FromJson<FirebaseProfil_AllMedals_Stats>(jsonData);
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
