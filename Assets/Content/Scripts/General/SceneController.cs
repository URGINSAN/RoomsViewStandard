using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public PlayerController Player;
    public CameraController CamController;
    public float ScreenDivisionValue = 0.7f;

    void Start()
    {
        Application.targetFrameRate = 120;
        return;
        //var resolution = Screen.currentResolution;
        //var height = (int)(resolution.height * ScreenDivisionValue);
        //var width = (int)(resolution.width * ScreenDivisionValue);
        //Screen.SetResolution(width, height, true);
        MeshFilter[] m = FindObjectsOfType<MeshFilter>();
        for (int i = 0; i < m.Length; i++)
        {
            m[i].mesh.UploadMeshData(true);
            print(m[i].name);
        }
        print(m.Length);
    }
}