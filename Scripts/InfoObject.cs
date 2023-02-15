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
                //���� ���������� �� ������� ����������� �� �������
                SectionInfo.localScale = Vector3.Lerp(SectionInfo.localScale, desiredScale, Time.deltaTime * speed);

                InfoParent.transform.LookAt(transform);
            }
        }
        else if(SectionInfo != null)
        {
            SectionInfo.localScale = Vector3.Lerp(SectionInfo.localScale, Vector3.zero, Time.deltaTime * speed);
        }
    }

    //�������� ������ � �����������
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

    public void FindInfoObjects(Transform bundleParent)
    {
        infos.Clear();
        foreach (Transform child in bundleParent.GetComponentInChildren<Transform>())
        {
            infos.Add(child.gameObject);
        }
    }
}
