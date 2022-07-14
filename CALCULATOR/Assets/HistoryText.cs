using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryText : MonoBehaviour
{
    public Text historyText;

    // Start is called before the first frame update
    void Start()
    {
        historyText = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public string getText()
    {
        return historyText.text;
    }
    public void setText(string str)
    {
        historyText.text = str;
    }
}
