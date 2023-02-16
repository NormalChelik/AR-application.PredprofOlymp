using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class ProgrammManager : MonoCache
{
    [Header("Marker on plane")]
    [SerializeField]
    private GameObject planeMarker; //маркер
    [Header("Object spawn")]
    [SerializeField]
    public GameObject spawnObject;
    [SerializeField]
    public bool objectIsSelected = false;
    [Header("TransformParent")]
    [SerializeField]
    public Transform bundleParent;


    private ARRaycastManager aRRaycastManager;

    private InfoObject infoObject;
    private UIManager uiManager;

    void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        planeMarker.SetActive(false);
        uiManager = FindObjectOfType<UIManager>();

        infoObject = FindObjectOfType<InfoObject>();
    }

    public override void OnTick()
    {
        if (objectIsSelected)
        {
            Marker();
        }
    }

    private void Marker()
    {
        //создание луча
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        //откуда пускается луч, куда записываем информацию от луча, фиксируем плоскость
        aRRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        if (hits.Count > 0)
        {
            //ставим маркер туда, где упал луч на плоскости
            planeMarker.transform.position = hits[0].pose.position;
            planeMarker.SetActive(true);
        }

        //одно нажатие
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && spawnObject != null)
        {
            //создание объекта на сцене
            Instantiate(spawnObject, hits[0].pose.position, spawnObject.transform.rotation, bundleParent);
            objectIsSelected = false;
            planeMarker.SetActive(false);
            infoObject.FindInfoObjects(bundleParent);
            uiManager.listVerbButton.interactable = true;
        }
    }

}
