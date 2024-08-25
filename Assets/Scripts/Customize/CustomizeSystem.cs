using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeSystem : MonoBehaviour
{
    public Color[] colors;
    public SpriteRenderer playerRenderer;
    public ParticleSystem playerdie;
    public Button buyButton;
    public ColorButton colorButton;
    public bool isSelected;
    public int colorIndex;//0 = red, 1 = green, 2 = blue, 3 = purple, 4 = pink, 5 = yellow   
    public static CustomizeSystem instance;


    private void Awake()
    {
        playerdie = playerdie.gameObject.GetComponent<ParticleSystem>();
        instance = this;
        colorIndex = PlayerPrefs.GetInt("colorIndex");
    }

    private void Start()
    {
        playerRenderer.color = colors[colorIndex];        
    }

    // Update is called once per frame
    void Update()
    {
        if(colorButton == null)
        {
            buyButton.interactable = false;
        }
        else if(colorButton != null)
        {
            buyButton.interactable = true;
        }

    }

    public void Select()
    {
        colorIndex = colorButton.colorIndex;
        playerRenderer.color = colors[colorIndex];

        var main = playerdie.main;
        main.startColor = colors[colorIndex];
        PlayerPrefs.SetInt("colorIndex", colorIndex);
        Debug.Log("Select Color.");
    }

}
