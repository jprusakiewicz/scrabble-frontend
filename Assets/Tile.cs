using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
//    private TextMeshProUGUI text;
    [SerializeField] Button button;
    private LetterManager letterManager;
    private Player player;
    public bool isChosen;

    void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnBtnClick);
        letterManager = GameObject.Find("LetterManager").GetComponent<LetterManager>();
        player = GameObject.Find("Player").GetComponent<Player>();


//        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public void SetLetter(string letter)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = letter.ToUpper();
    }

    void OnBtnClick()
    {
        if (isChosen)
        {
            letterManager.ResetChosenLetter();
            Deselect();
            return;
        }

        player.ResetTilesOpacity();
        Select();
        string currentLetter = gameObject.GetComponentInChildren<TextMeshProUGUI>().text;

        letterManager.SetChosenLetter(char.Parse(currentLetter));
//        player.RemoveLetter(currentLetter);
    }

    void Select()
    {
        isChosen = true;
        var image = GetComponent<Image>();
        var tempColor = image.color;
        tempColor.a = 0.5f;
        image.color = tempColor;
    }

    public void Deselect()
    {
        isChosen = false;
        var image = GetComponent<Image>();
        var tempColor = image.color;
        tempColor.a = 1f;
        image.color = tempColor;
    }
}