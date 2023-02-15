using System.Collections.Generic;
using UnityEngine;


public class InfoObject : MonoCache
{
    [Header("InfoObjects")]
    [SerializeField]
    public List<GameObject> infos;

    private Transform InfoParent;
    private Transform SectionInfo;

    const float speed = 10f;

    private Vector3 desiredScale = Vector3.zero;


    public override void OnTick()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            GameObject go = hit.collider.gameObject;
            if (go.CompareTag("models"))
            {
                OpenInfo(go);
                //Наша информация об объекте выскакивает из объекта
                SectionInfo.localScale = Vector3.Lerp(SectionInfo.localScale, desiredScale, Time.deltaTime * speed);

                //инфопанель двигается за камерой 
                InfoParent.transform.LookAt(transform);
            }
        }
        else if(SectionInfo != null)
        {
            SectionInfo.localScale = Vector3.Lerp(SectionInfo.localScale, Vector3.zero, Time.deltaTime * speed);
        }
    }

    //открытие панели с информацией
    private void OpenInfo(GameObject desiredInfo)
    {
        foreach (GameObject info in infos)
        {
            if(info == desiredInfo)
            {
                SectionInfo = desiredInfo.transform.Find("SectionInfo");
                InfoParent = desiredInfo.transform.Find("SectionInfo").Find("InfoParent");
                desiredScale = Vector3.one;
            }
        }
    }

    //добавление всех инфопанелей в список
    public void FindInfoObjects(Transform bundleParent)
    {
        infos.Clear();
        foreach (Transform child in bundleParent.GetComponentInChildren<Transform>())
        {
            infos.Add(child.gameObject);
        }
    }
}
