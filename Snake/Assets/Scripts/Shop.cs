using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private GameObject[] purchaseObjects;
    private Theme themeInstance;
    private Canvas canvasOld;
    private GameObject snakeObj;

    private static GameObject shopSnakesObj;

    private static GameObject canvasShopObj;
    private Renderer[] renderers;
    private List<Material> listDefaults = new List<Material>();

    [SerializeField]
    private Material hoverMaterial;

    [SerializeField]
    private Material[] materialsFor3D;

    void Awake() { }

    void Start()
    {
        if (gameObject.CompareTag("ShopSnakes"))
        {
            renderers = GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renderers.Length; i++)
                listDefaults.Add(renderers[i].sharedMaterial);
            return;
        }
        themeInstance = GetComponent<Theme>();
        themeInstance.Initiliaze(materialsFor3D);
        canvasOld = GetComponentOfObject<Canvas>("Canvas") as Canvas;
        snakeObj = GetObject("Snake");
        shopSnakesObj = GetObject("ShopSnakes");
        canvasShopObj = GetObject("CanvasShop");
    }

    public void Set3DMenu(bool condition)
    {
        canvasOld.enabled = condition;
        snakeObj.SetActive(condition);
        ActivateObjects(!condition);
        themeInstance.Set3DOptions();
        themeInstance.SetItems(condition);
    }

    private void PurchaseItem(bool condition)
    {
        ActivateObjects(condition);
        foreach (var item in purchaseObjects)
            item.SetActive(true);
    }

    void OnMouseEnter()
    {
        SetMaterial(hoverMaterial);
    }

    void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            PurchaseItem(false);
            SetMaterial(listDefaults);
        }
    }

    void OnMouseExit()
    {
        SetMaterial(listDefaults);
    }

    private void ActivateObjects(bool condition)
    {
        ActivateChildren(shopSnakesObj, condition);
        ActivateChildren(canvasShopObj, condition);
    }

    private void ActivateChildren(GameObject obj, bool condition)
    {
        for (int i = 0; i < obj.transform.childCount; i++)
            obj.transform.GetChild(i).gameObject.SetActive(condition);
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
