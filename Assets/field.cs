using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class field : MonoBehaviour
{
    private Button button;

//    private Image image;
    public int x;
    public int y;
    private LetterManager letterManager;


    void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnBtnClick);
//        image = gameObject.GetComponent<Image>();
        letterManager = GameObject.Find("LetterManager").GetComponent<LetterManager>();
    }

    public void SetField(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    void OnBtnClick()
    {
        Debug.Log("btn click " + x + " " + y);

        if (letterManager.IsCellFilled(x, y) && letterManager.IsPlayers(x, y))
        {
            letterManager.PutBackLetter(x, y);
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "";
            SetTileOpacity(0.02f);
            letterManager.setIsNotPlayers(x, y);
        }
        else if (!letterManager.IsCellFilled(x, y))
        {
            if (!letterManager.IsChosenLetter()) return;
            var letterToPutOnBoard = letterManager.GetChosenLetter();
            SetLetter(letterToPutOnBoard);
            letterManager.TakeLetterFromPlayer(letterToPutOnBoard.ToString());
            letterManager.AddLetter((char) letterToPutOnBoard, x, y, true);
            SetTileOpacity(0.75f);
        }
    }

    public string GetLetter()
    {
        return gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
    }

    public void SetLetter(char? letter)
    {
        if (letter == null) return;
        string letterAsString = letter.ToString();
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = letterAsString.ToUpper();
    }

    public void SetTileOpacity(float value)
    {
        var image = GetComponent<Image>();
        var tempColor = image.color;
        tempColor.a = value;
        image.color = tempColor;
    }
}