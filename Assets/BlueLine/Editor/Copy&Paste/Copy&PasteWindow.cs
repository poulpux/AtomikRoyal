using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

struct Bouton
{
    public string name, content;
    public Bouton(string name, string content)
    {
        this.name = name;
        this.content = content; 
    }
}

public class CopyPasteWindow : ToolsGF
{
    //For adding a content, just put a .txt file in Assets/BlueLine/Editor/Copy&Paste/AllCopyPastTxt folder with the right name
    private static void setAllBouton()
    {
        listBouton.Clear();

        AddButton("Singleton", "Singleton");
        AddButton("Tooltips", "Tooltips");
        AddButton("TextArea", "TextArea");
        AddButton("Cooldown", "Cooldown");
        AddButton("Ligne fine", "Ligne fine");
        AddButton("Grosse parenthese", "Grosse parenthese");
    }


    private string safeTxt;
    private static List<Bouton> listBouton = new List<Bouton>();
    protected  override Vector2 defaultWindowSize { get { return new Vector2(300f, 1500f ); } }
    public static CopyPasteWindow _window;
    private bool enabled;

    [MenuItem("BlueLine/BlueGlue", false, 12)]
    public static void OpenScript()
    {
         _window = CreateWindow(ref _window, "BlueGlue");
         _window.position = new Rect(1650f, 0f, 10f, 10f);      
    }
    protected override void OnGUI()
    {
        if (_window == null)
        {
            _window = CreateWindow(ref _window, "BlueGlue");
            _window.position = new Rect(1650f, 0f, 10f, 10f);
        }
        if (_window != null && !EditorApplication.isPlaying)
        {
            if (!Start)
                setAllBouton();

            base.OnGUI();
            StartOnGUI(_window, defaultWindowSize);
            displayAllCopyBouton();
            displayTxtArea();
            displayAddBouton();

        }
         
    }

    private void displayAllCopyBouton()
    {
        foreach (var bouton in listBouton)
        {
            if (EditorGUIUtility.systemCopyBuffer == bouton.content)
                GUI.skin.button.normal.background = defaultButtonStylePress;
            else
                GUI.skin.button.normal.background = defaultButtonStyleNotPress;

            if (GUILayout.Button(bouton.name, GUILayout.Width(300f), GUILayout.Height(30f)))
                EditorGUIUtility.systemCopyBuffer = bouton.content;
        }
    }

    private void displayTxtArea()
    {
        safeTxt = GUILayout.TextArea(safeTxt);
    }

    private void displayAddBouton()
    {
        GUI.skin.button.normal.background = defaultButtonStyleNotPress;
        if (GUILayout.Button(("+"), GUILayout.Width(300f), GUILayout.Height(30f))&&!string.IsNullOrEmpty(safeTxt))
        {
            string singletonContent = safeTxt;
            Bouton newBouton = new Bouton(singletonContent, singletonContent);
            listBouton.Add(newBouton);
            safeTxt = "";
        }
    }

    private static void AddButton(string name, string filePath)
    {
        Bouton button = new Bouton(name, GetContent(filePath));
        listBouton.Add(button);
    }

    private static string GetContent(string filePath)
    {
        string realFilePath = "Assets/BlueLine/Editor/Copy&Paste/AllCopyPastTxt/" + filePath + ".txt";
        if (File.Exists(realFilePath))
            return File.ReadAllText(realFilePath);
        else
        {
            Debug.LogError("Le fichier n'existe pas : " + realFilePath);
            return "";
        }
    }
}
