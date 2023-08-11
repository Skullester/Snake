using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private Canvas canvasPurchase;

    [SerializeField]
    private GameObject snakePreview;
    private Renderer[] renderers;
    private List<Material> listDefaults = new List<Material>();

    [SerializeField]
    private Material hoverMaterial;
    private Canvas startCanvas;
    private GameObject shopSnakes;
    private Transform camPreview;
    private static Transform cam;

    [SerializeField]
    private float speedCam;
    private bool isCameraGoing;

    [SerializeField]
    private float t = 0.5f;
    private Transform snake;

    private void Awake()
    {
        cam = Camera.main.GetComponent<Transform>();
        if (gameObject.CompareTag("ShopSnakes"))
        {
            camPreview = GetComponentInChildren<Camera>(true).transform;
            print(camPreview.gameObject);
            renderers = GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renderers.Length; i++)
                listDefaults.Add(renderers[i].sharedMaterial);
        }

        //snake = snakePreview.transform.Find("Snake3D");
        shopSnakes = GetObject("ShopSnakes");
        startCanvas = GetObject("CanvasShop").GetComponent<Canvas>();
    }

    void OnMouseEnter()
    {
        if (!isCameraGoing)
            SetMaterial(hoverMaterial);
    }

    public void ActivatePreview(bool condition)
    {
        //  StartCoroutine(SmoothCamTrans(condition));
        SetCanvas(ref condition);
        isCameraGoing = true;
        StartCoroutine(SmoothCamTrans(true));
    }

    private void SetCanvas(ref bool condition)
    {
        startCanvas.enabled = !condition;
        canvasPurchase.enabled = condition;
    }

    void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1") && !isCameraGoing)
        {
            ActivatePreview(true);
            SetMaterial(listDefaults);
        }
    }

    IEnumerator SmoothCamTrans(bool condition)
    {
        Vector3 camPos = cam.position;
        Vector3 camPreviewPos = camPreview.position;
        if (!condition)
        {
            Vector3 tmp = cam.position;
            camPos = camPreviewPos;
            camPreviewPos = tmp;
        }
        cam.position = camPos;
        //Vector3 direction = camPreviewPos - camPos;
        // Quaternion rotation = Quaternion.LookRotation(direction);
        while (cam.position != camPreview.position)
        {
            print("a");
            yield return null;
            // cam.rotation = Quaternion.Lerp(cam.rotation, rotation, t * Time.deltaTime);
            cam.position = Vector3.MoveTowards(
                cam.position,
                camPreviewPos,
                speedCam * Time.deltaTime
            );
        }
        //  shopSnakes.SetActive(!condition);
    }

    void OnMouseExit()
    {
        SetMaterial(listDefaults);
    }

    private void SetMaterial(Material material)
    {
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].sharedMaterial = material;
    }

    private void SetMaterial(List<Material> material)
    {
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].sharedMaterial = material[i];
    }

    /// <summary>
    ///  Find object by (name) and take its component with type (T)
    /// </summary>
    private Component GetComponentOfObject<T>(string name)
    {
        return GameObject.Find(name).GetComponent(typeof(T));
    }

    private GameObject GetObject(string nameObj)
    {
        return GameObject.Find(nameObj);
    }
}
