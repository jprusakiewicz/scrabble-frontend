using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class buttons : MonoBehaviour
{
    // wymiary 15 na 15
    // srodek 8
    // Start is called before the first frame update
    [SerializeField] GameObject button;
    [SerializeField] GameObject board;
    private LetterManager letterManager;

    void Start()
    {
        letterManager = GameObject.Find("LetterManager").GetComponent<LetterManager>();

//        SpawnButtons();
    }


    public void SpawnButtons()
    {
        RemoveButtons();
        var buttonSize = board.GetComponent<RectTransform>().rect.width / 15;

        for (int x = 1; x <= 15; x++)
        {
            for (int y = 1; y <= 15; y++)
            {
                var newButton = Instantiate(button, new Vector3(), gameObject.transform.rotation);
                newButton.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonSize, buttonSize);
                newButton.transform.SetParent(gameObject.transform);
                newButton.transform.localPosition = new Vector3((x-1) * buttonSize, (y-1) * buttonSize * -1);
                var field = newButton.GetComponent<field>();
                field.SetField(x, y);

                char? letter = letterManager.GetLetter(x, y);
                if (letter != null)
                {
                    field.SetLetter(letter);
                    newButton.GetComponent<field>().SetTileOpacity(0.75f);
                }
            }
        }
    }

    public void RemoveButtons()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void HandleServerData(List<BoardConfig> itemBoard)
    {
        letterManager.HandleServerData(itemBoard);
        SpawnButtons();
    }

    public List<Dictionary<string, dynamic>> SendDataToServer()
    {
        var validFields = new List<Dictionary<string, dynamic>>();
        foreach (Transform child in transform)
        {
            var field = child.GetComponent<field>();
            if (letterManager.IsPlayers(field.x, field.y) == true)
            {
                validFields.Add(new Dictionary<string, dynamic>()
                {
                    ["x"] = field.x,
                    ["y"] = field.y,
                    ["letter"] = field.GetLetter(),
                });
            }
        }

        return validFields;
    }
}