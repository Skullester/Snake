using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Shop : MonoBehaviour
{
    private Theme themeInstance;
    private Canvas canvas;
    private GameObject snakeObj;

    [SerializeField]
    private GameObject shopSnakesObj;

    [SerializeField]
    private GameObject canvasShopObj;
    private Renderer[] renderers;
    private List<Material> listDefaults = new List<Material>();

    [SerializeField]
    private Material hoverMaterial;

    [SerializeField]
    private Material[] materialsFor3D;

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
        canvas = GetComponentOfObject<Canvas>("Canvas") as Canvas;
        snakeObj = GetObject("Snake");
    }

    public void Set3DMenu(bool condition)
    {
        canvas.enabled = condition;
        snakeObj.SetActive(condition);
        shopSnakesObj.SetActive(!condition);
        canvasShopObj.SetActive(!condition);
        themeInstance.Set3DOptions();
        themeInstance.SetItems(condition);
    }

    void OnMouseEnter()
    {
        SetMaterial(hoverMaterial);
    }

    void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1"))
            SetMaterial(listDefaults);
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
