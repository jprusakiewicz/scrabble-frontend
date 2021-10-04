using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nicks : MonoBehaviour
{
    [SerializeField] private GameObject FirstNick;
    [SerializeField] private GameObject SecondNick;
    [SerializeField] private GameObject ThirdNick;
    [SerializeField] private GameObject FourthNick;

    // Start is called before the first frame update
    void Start()
    {
       // DeactivateNicks();
    }

    public void DeactivateNicks()
    {
        FirstNick.SetActive(false);
        SecondNick.SetActive(false);
        ThirdNick.SetActive(false);
        FourthNick.SetActive(false);
    }

    public void ActivateNicks(Dictionary<string, Dictionary<string, dynamic>> nicks)
    {
        DeactivateNicks();
        if (nicks.TryGetValue("1", out var first))
        {
            foreach (var go in FirstNick.GetComponentsInChildren<TMPro.TextMeshProUGUI>())
            {
                if (go.name == "nick")
                    go.GetComponent<TMPro.TextMeshProUGUI>().text = (string) first["nick"];
                if (first["has_turn"] == true && go.name == "arrow")
                {
                    go.gameObject.SetActive(true);
                }
            }

            FirstNick.SetActive(true);
        }

        if (nicks.TryGetValue("2", out var second))
        {
            foreach (var go in SecondNick.GetComponentsInChildren<TMPro.TextMeshProUGUI>())
            {
                if (go.name == "nick")
                    go.GetComponent<TMPro.TextMeshProUGUI>().text = (string) second["nick"];
                if (second["has_turn"] == true && go.name == "arrow")
                {
                    go.gameObject.SetActive(true);
                }
            }

            SecondNick.SetActive(true);
        }

        if (nicks.TryGetValue("3", out var third))
        {
            foreach (var go in ThirdNick.GetComponentsInChildren<TMPro.TextMeshProUGUI>())
            {
                if (go.name == "nick")
                    go.GetComponent<TMPro.TextMeshProUGUI>().text = (string)  third["nick"];
                if (third["has_turn"] == true && go.name == "arrow")
                {
                    go.gameObject.SetActive(true);
                }
            }

            ThirdNick.SetActive(true);
        }

        if (nicks.TryGetValue("4", out var fourth))
        {
            foreach (var go in FourthNick.GetComponentsInChildren<TMPro.TextMeshProUGUI>())
            {
                if (go.name == "nick")
                    go.GetComponent<TMPro.TextMeshProUGUI>().text = (string) fourth["nick"];
                if (fourth["has_turn"] == true && go.name == "arrow")
                {
                    go.gameObject.SetActive(true);
                }
            }

            FourthNick.SetActive(true);
        }
    }
}