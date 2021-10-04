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
        GetComponentInChildren<TextMeshProUGUI>().text = letter;
    }
    
    void OnBtnClick()
    {
        player.ResetTilesOpacity();
        SetTileOpacity();
        string currentLetter = gameObject.GetComponentInChildren<TextMeshProUGUI>().text;

        letterManager.SetChosenLetter(char.Parse(currentLetter));
//        player.RemoveLetter(currentLetter);
    }

    void SetTileOpacity()
    {
        var image = GetComponent<Image>();
        var tempColor = image.color;
        tempColor.a = 0.5f;
        image.color = tempColor;
    }
}
