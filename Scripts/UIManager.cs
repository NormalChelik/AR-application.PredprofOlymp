using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UIManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField]
    public Button listVerbButton;
    [SerializeField]
    public List<Button> listButtonSpawn;
    [SerializeField]
    private Button destroyButton;
    [SerializeField]
    private Button prefabButton;
    [Header("OtherUI")]
    [SerializeField]
    private GameObject scrollView;
    [SerializeField]
    public float progress;



    private ProgrammManager ProgrammManager;
    private DataBase db;

    private void Start()
    {
        ProgrammManager = FindObjectOfType<ProgrammManager>();
        db = GetComponent<DataBase>();

        //Действия для кнопок
        destroyButton.onClick.AddListener(DestroyModels);
        listVerbButton.onClick.AddListener(listButtonActive);
    }

    private void listButtonActive()
    {
        scrollView.SetActive(!scrollView.activeSelf);

        //Создание кнопок 
        if (listButtonSpawn.Count == 0)
        {
            for (int i = 0; i < db.verbsStr.Count; i++)
            {

                Button bt = Instantiate(prefabButton);
                bt.transform.SetParent(scrollView.GetComponentInChildren<ContentSizeFitter>().transform, worldPositionStays: false) ;

                listButtonSpawn.Add(bt);
                listButtonSpawn[i].GetComponentInChildren<Text>().text = db.verbsStr[i];
                listButtonSpawn[i].name = db.verbsStr[i];
                listButtonSpawn[i].onClick.AddListener(SpawnObjectFunction);
            }
        }
    }
    //удаление моделей со сцены
    private void DestroyModels()
    {
        Transform contentModels = ProgrammManager.bundleParent;
        for (int i = 0; i < contentModels.transform.childCount; i++)
            Destroy(contentModels.transform.GetChild(i).gameObject);
    }
    
    //создание модели на сцене
    private void SpawnObjectFunction()
    {
        listVerbButton.interactable = false;

        ProgrammManager.spawnObject = null;

        string assetBundle = EventSystem.current.currentSelectedGameObject.name;

        db.GetBundleObject(assetBundle);
        scrollView.SetActive(false);
    }
}
