using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System;
using Firebase.Extensions;
using UnityEngine.Networking;
using Firebase.Storage;
using TMPro;
using UnityEngine.UI;


public class DataBase : MonoBehaviour
{
    [Header("VerbStr")]
    [SerializeField]
    public List<string> verbsStr;
    [Header("InfoObjects")]
    [SerializeField]
    private TMP_FontAsset textMaterialVerb;
    [SerializeField]
    private TMP_FontAsset textMaterialVerbTranslate;
    [Header("UI")]
    [SerializeField]
    private Scrollbar loadingBarModel;
    [SerializeField]
    private Text textProgress;


    private ProgrammManager ProgrammManager;

    private DatabaseReference dbRef;
    private FirebaseStorage storage;
    private StorageReference storageReference;

    private void Start()
    {
        ProgrammManager = FindObjectOfType<ProgrammManager>();

        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        storage = FirebaseStorage.DefaultInstance;
        storageReference = storage.GetReferenceFromUrl("ссылка на папку, где лежат модели, в StorageFirebase");

        StartCoroutine(LoadData());
    }

    //загрузка assetBundle
    public void GetBundleObject(string assetName)
    {
        assetName = assetName.Replace(" ", "_");

        StorageReference storageModelBundle = storageReference.Child(assetName);
        Debug.Log(assetName + "  " + storageModelBundle);

        storageModelBundle.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
        {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                StartCoroutine(Load3DModels(Convert.ToString(task.Result))); 
            }
            else
            {
                Debug.Log(task.Exception);
            }
        });
    }

    //загрузка глаголов из БД и добавление их в список
    public IEnumerator LoadData()
    {
        var verbs = dbRef.Child("buttons").Child("verbs").GetValueAsync();

        yield return new WaitUntil(predicate: () => verbs.IsCompleted);

        if (verbs.Exception != null)
        {
            Debug.LogError(verbs.Exception);
        }
        else if (verbs.Result.Value == null)
        {
            Debug.Log("null");
        }
        else
        {
            DataSnapshot snapshot = verbs.Result;
            foreach(DataSnapshot childSnapshot in snapshot.Children)
            {
                verbsStr.Add(childSnapshot.Key.ToString());
            }
        }
    }
    //загрузка и распаковка assetBundle
    IEnumerator Load3DModels(string assetName)
    {

        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(assetName))
        {
            var operation = www.SendWebRequest();

                loadingBarModel.gameObject.SetActive(true);
                //визуализация загрузки модели
                while (!operation.isDone)
                {
                    textProgress.text = "загрузка модели";
                    loadingBarModel.size += 0.05f;
                    yield return null;
                }
                loadingBarModel.size = 0f;
                loadingBarModel.gameObject.SetActive(false);
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
                if (bundle != null)
                {

                    string rootAssetPath = bundle.GetAllAssetNames()[0];
                    ProgrammManager.spawnObject = bundle.LoadAsset(rootAssetPath) as GameObject;

                    ProgrammManager.spawnObject.transform.Find("SectionInfo").Find("InfoParent").Find("Verb").GetComponent<TMP_Text>().font = textMaterialVerb;
                    ProgrammManager.spawnObject.transform.Find("SectionInfo").Find("InfoParent").Find("VerbTranslate").GetComponent<TMP_Text>().font = textMaterialVerbTranslate;

                    bundle.Unload(false);

                    ProgrammManager.objectIsSelected = true;
                }

                else
                {
                    Debug.Log("Not a valid asset bundle");
                }
        }
    }
}

