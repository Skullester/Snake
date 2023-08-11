using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
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
    private GameObject shopSnakes;
    private Transform camPreview;
    private static Transform cam;

    [SerializeField]
    private float speedCam;
    private bool isCameraGoing;

    private void Awake()
    {
        audioSource = GetObject("Platform").GetComponent<AudioSource>();
        cam = Camera.main.GetComponent<Transform>();
        if (gameObject.CompareTag("ShopSnakes"))
        {
            camPreview = GetComponentInChildren<Camera>(true).transform;
            renderers = GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renderers.Length; i++)
                listDefaults.Add(renderers[i].sharedMaterial);
        }
        shopSnakes = GetObject("ShopSnakes");
        startCanvas = GetObject("Canvas").GetComponent<Canvas>();
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
        SetCanvas(ref condition);
        isCameraGoing = true;
        StartCoroutine(SmoothCamTrans(true));
    }

    private void SetCanvas(ref bool condition)
    {
        print(startCanvas);
        print(condition);
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
        while (cam.position != camPreview.position)
        {
            yield return null;
            cam.position = Vector3.MoveTowards(
                cam.position,
                camPreviewPos,
                speedCam * Time.deltaTime
            );
        }
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
