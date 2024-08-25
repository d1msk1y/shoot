using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextShower : MonoBehaviour
{

    public Text txtField;
    public string key, afterValueString,beforeValueString;
    private int value;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        value = PlayerPrefs.GetInt(key);
        txtField.text = beforeValueString + value + afterValueString;
    }
}
