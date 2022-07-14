using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Calculate : MonoBehaviour
{
    public GameObject formula;
    Text formulaText;
    public Text itemText;
    private double leftValue;
    private double rightValue;
    private bool pushNumber;
    private string preText;
    private bool isPuls;
    private bool isMinus;
    private bool isMulti;
    private bool isDivi;
    private bool isFull;
    private HistoryText[] historyText;
    public Transform historyTf;

    void Start()
    {
        itemText = GetComponent<Text>();
        itemText.text = "0";
        leftValue = 0;
        pushNumber = false;
        preText = "0";
        isPuls = false;
        isMinus = false;
        isMulti = false;
        isDivi = false;
        formulaText = formula.gameObject.GetComponent<Text>();
        historyText = historyTf.GetComponentsInChildren<HistoryText>();
        isFull = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Getvalue(string value)
    {
        if (value == "+")
        {
            if (pushNumber == true)
            {
                PushEqual();
            }
            GetLeftValue();
            isPuls = true;
            isMinus = false;
            isMulti = false;
            isDivi = false;
            formulaText.text = leftValue.ToString() + "+";
            pushNumber = false;
        }
        else if (value == "-")
        {
            if (pushNumber == true)
            {
                PushEqual();
            }
            GetLeftValue();
            isPuls = false;
            isMinus = true;
            isMulti = false;
            isDivi = false;
            formulaText.text = leftValue.ToString() + "-";
            pushNumber = false;

        }
        else if (value == "*")
        {
            if (pushNumber == true)
            {
                PushEqual();
            }
            GetLeftValue();
            isPuls = false;
            isMinus = false;
            isMulti = true;
            isDivi = false;
            formulaText.text = leftValue.ToString() + "x";
            pushNumber = false;

        }
        else if (value == "/")
        {
            if (pushNumber == true)
            {
                PushEqual();
            }
            GetLeftValue();
            isPuls = false;
            isMinus = false;
            isMulti = false;
            isDivi = true;
            formulaText.text = leftValue.ToString() + "÷";
            pushNumber = false;

        }

        else if (itemText.text == "0" && value == "0")
        {
            itemText.text = value;
        }
        else if (value == "=")
        {
            PushEqual();
            isPuls = false;
            isMinus = false;
            isMulti = false;
            isDivi = false;

        }
        else if (value == "C")
        {
            init();
            pushNumber = false;

        }
        else if (value == "CE")
        {
            pushNumber = false;

            itemText.text = "0";
        }
        else if (value == "Del")
        {
            pushNumber = false;

            string str = itemText.text;
            if (leftValue.ToString() != str)
            {
                str = str.Substring(0, str.Length - 1);
                if (str.Length < 1)
                {
                    itemText.text = "0";
                }
                else
                {
                    itemText.text = str;
                }
            }

        }
        else if (value == "Per")
        {
            pushNumber = false;

            GetLeftValue();
            itemText.text = (leftValue / 100).ToString();
            isPuls = false;
            isMinus = false;
            isMulti = false;
            isDivi = false;
            formulaText.text = "0";
            AddHistory(itemText.text);
        }
        else if (value == "PN")
        {
            

            if (/*leftValue.ToString() != itemText.text && */pushNumber == true)
            {
                if (double.Parse(itemText.text) > 0)
                {
                    itemText.text = "-" + itemText.text;
                }
                else if (double.Parse(itemText.text) < 0)
                {
                    itemText.text = (double.Parse(itemText.text) * (-1)).ToString();
                }
                pushNumber = true;

            }
            else
            {
                pushNumber = false;
            }


        }
        else if (value == "point")
        {

            if (/*leftValue.ToString() != itemText.text && */ pushNumber ==true)
            {
                pushNumber = true;

                itemText.text = itemText.text + ".";
            }
            else
            {
                pushNumber = false;
            }
        }
        else if (value == "Frac")
        {
            pushNumber = false;
            formulaText.text = "1/" + itemText.text;
            itemText.text = (1/double.Parse(itemText.text)).ToString();
            
            AddHistory(formulaText.text+"="+itemText.text);
        }
        else if (value == "Sqr")
        {
            pushNumber = false;
            formulaText.text = "Sqr(" + itemText.text + ")";
            itemText.text = ((double.Parse(itemText.text))* (double.Parse(itemText.text))).ToString();

            AddHistory(formulaText.text + "=" + itemText.text);
        }
        else if (value == "Radic")
        {
            pushNumber = false;
            formulaText.text = "√(" + itemText.text+")";
            itemText.text = (Math.Sqrt(double.Parse(itemText.text))).ToString();

            AddHistory(formulaText.text + "=" + itemText.text);
        }
        else if (pushNumber == true)
        {
            itemText.text += value;
        }
        else if (pushNumber == false)
        {

            itemText.text = value;
        }
              

        for (int i = 0; i < 10; i++)
        {
            if (i.ToString() == value/* || value.Equals("point") || value.Equals("PN")*/)
            {
                pushNumber = true;
                break;
            }/*
            else
            {
              //  Debug.Log("pushnam false");
                pushNumber = false;
            }*/
            
        }
        
        preText = value;
    }
    public double Plus()
    {
        return leftValue + rightValue;
    }
    public double Minus()
    {
        return leftValue - rightValue;
    }
    public double multiplication()
    {
        return leftValue * rightValue;
    }
    public double Division()
    {
        return leftValue / rightValue;
    }
    
    public void GetLeftValue()
    {
        leftValue = double.Parse(itemText.text);
    }
    public void GetRightValue()
    {
        rightValue = double.Parse(itemText.text);
    }
    public void PushEqual()
    {
        if (isPuls == true)
        {
            GetRightValue();
            itemText.text = Plus().ToString();
            formulaText.text += rightValue.ToString();
            AddHistory(formulaText.text + "=" + itemText.text);
            pushNumber = false;
        }
        else if (isMinus == true)
        {
            GetRightValue();
            itemText.text = Minus().ToString();
            formulaText.text += rightValue.ToString();
            AddHistory(formulaText.text + "=" + itemText.text);
            pushNumber = false;

        }
        else if (isMulti == true)
        {
            GetRightValue();
            itemText.text = multiplication().ToString();
            formulaText.text += rightValue.ToString();
            AddHistory(formulaText.text + "=" + itemText.text);
            pushNumber = false;

        }
        else if (isDivi == true)
        {
            GetRightValue();
            itemText.text = Division().ToString();
            formulaText.text += rightValue.ToString();
            AddHistory(formulaText.text + "=" + itemText.text);
            pushNumber = false;

        }
    }
    public void AddHistory(string str)
    {
        for(int i=0; i<historyText.Length; i++)
        {
            if (isFull == true)
            {
                if (i == historyText.Length - 1)
                {
                    historyText[i].setText("");
                }
                else
                {
                    historyText[i].setText(historyText[i + 1].getText());
                }

            }
            

            if (historyText[i].getText().Equals(""))
            {
                Debug.Log(str);
                historyText[i].setText(str);
                if (i == historyText.Length - 1)
                {
                    isFull = true;
                }
                break;
            }
            
        }
    }
    private void init()
    {
        leftValue = 0;
        rightValue = 0;
        itemText.text = "0";
        formulaText.text = "0";
        isPuls = false;
        isMinus = false;
        isMulti = false;
        isDivi = false;
    }
}
