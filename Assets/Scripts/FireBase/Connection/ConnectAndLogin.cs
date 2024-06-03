using Firebase.Auth;
using Firebase;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using Firebase.Extensions;

public class ConnectAndLogin : MonoBehaviour
{
    bool Connected;
    public FirebaseAuth auth;
    void Awake()
    {
        LoginWithEmailAndGetDrive("ambroise.marquet@gmail.com", "pipoudou");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private async void CheckEmailExistsAndSignUp(string email, string password)
    {
        auth = FirebaseAuth.DefaultInstance;
        try
        {
            // Tente de créer l'utilisateur avec l'e-mail et le mot de passe fournis
            var signUpTask = await auth.CreateUserWithEmailAndPasswordAsync(email, password);

            // L'utilisateur est inscrit avec succès
            FirebaseUser newUser = signUpTask.User;
            Debug.Log("Inscription réussie pour l'utilisateur : " + newUser.Email);
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
}
