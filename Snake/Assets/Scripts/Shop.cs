using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private Theme theme;

    [SerializeField]
    private ThemeChanger themeChanger;
    private static Transform empty;
    private Button btnBack;

    [SerializeField]
    private AudioClip[] menuSounds;
    private static AudioSource audioSource;

    [SerializeField]
    private Canvas canvasPurchase;
    private Renderer[] renderers;
    private List<Material> listDefaults = new List<Material>();

    [SerializeField]
    private Material hoverMaterial;
    private Canvas startCanvas;
    private Transform camPreview;
    private static Transform cam;

    [SerializeField]
    private float speedCam;
    private bool isCameraGoing;

    private void Awake()
    {
        audioSource = GetObject("Sun").GetComponent<AudioSource>();
        cam = Camera.main.GetComponent<Transform>();
        if (gameObject.CompareTag("ShopSnakes"))
        {
            camPreview = GetComponentInChildren<Camera>(true).transform;
            renderers = GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renderers.Length; i++)
                listDefaults.Add(renderers[i].sharedMaterial);
        }
        btnBack = GetObject("ButtonBack").GetComponent<Button>();
        btnBack.onClick.AddListener(() => ActivatePreview(false));
        startCanvas = GetObject("Canvas").GetComponent<Canvas>();
        empty = GetObject("EmptyObj").transform;
    }

    public void PlaySound(int index)
    {
        audioSource.PlayOneShot(menuSounds[index]);
    }

    void OnMouseEnter()
    {
        if (!isCameraGoing)
        {
            PlaySound(0);
            SetMaterial(hoverMaterial);
        }
    }

    public void ActivatePreview(bool condition)
    {
        if (canvasPurchase.enabled)
        {
            theme.SetMaterials3D();
            theme.SetItems(true);
            canvasPurchase.enabled = false;
        }
        startCanvas.enabled = !condition;
        isCameraGoing = condition;
        SmoothCamTrans(ref condition);
    }

    void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1") && !isCameraGoing)
        {
            themeChanger.ChangeTheme(Convert.ToInt32(gameObject.name));
            ActivatePreview(true);
            SetMaterial(listDefaults);
        }
    }

    private void SmoothCamTrans(ref bool condition)
    {
        if (condition)
            StartCoroutine(CameraMoving(camPreview.position, condition));
        else
            StartCoroutine(CameraMoving(empty.position, condition));
    }

    private IEnumerator CameraMoving(Vector3 pos, bool condition)
    {
        while (cam.position != pos)
        {
            yield return null;
            cam.position = Vector3.MoveTowards(cam.position, pos, speedCam * Time.deltaTime);
        }
        canvasPurchase.enabled = condition;
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

    private Component GetComponentOfObject<T>(string name)
    {
        return GameObject.Find(name).GetComponent(typeof(T));
    }

    private GameObject GetObject(string nameObj)
    {
        return GameObject.Find(nameObj);
    }
}
