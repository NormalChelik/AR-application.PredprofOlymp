using UnityEditor;
using System.IO;
using UnityEngine;

public class CreateAssetBundles 
{
    public static string assetBundleDirectory = "Assets/AssetBundles/";

    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {

        //���� ��������� �������� �� ����������, ������� ���
        if (Directory.Exists(assetBundleDirectory))
        {
            Directory.Delete(assetBundleDirectory, true);
        }

        Directory.CreateDirectory(assetBundleDirectory);

        //�������� ������ ��� �������
        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.Android);
        AppendPlatformToFileName("Android");
        Debug.Log("Android bundle created...");

        RemoveSpacesInFileNames();

        AssetDatabase.Refresh();
        Debug.Log("Process complete!");
    }

    static void RemoveSpacesInFileNames()
    {
        foreach (string path in Directory.GetFiles(assetBundleDirectory))
        {
            string oldName = path;
            string newName = path.Replace(' ', '-');
            File.Move(oldName, newName);
        }
    }

    static void AppendPlatformToFileName(string platform)
    {
        foreach (string path in Directory.GetFiles(assetBundleDirectory))
        {
            //�������� ��� �����
            string[] files = path.Split('/');
            string fileName = files[files.Length - 1];

            //������� �����, ������� ��� �� �����
            if (fileName.Contains(".") || fileName.Contains("Bundle"))
            {
                File.Delete(path);
            }
            else if (!fileName.Contains("-"))
            {
                //�������� ��������� � ����� �����
                FileInfo info = new FileInfo(path);
                info.MoveTo(path + "-" + platform);
            }
        }
    }
}
