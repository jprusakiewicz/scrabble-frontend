using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class iii
{
    public string nick { get; set; }
    public string score { get; set; }
    public bool has_turn { get; set; }
}

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

    public void ActivateNicks(Dictionary<string, iii> nicks)
    {
        DeactivateNicks();
        if (nicks.TryGetValue("1", out var first))
        {
            foreach (var go in FirstNick.GetComponentsInChildren<TMPro.TextMeshProUGUI>())
            {
                if (go.name == "Nick")
                    go.GetComponent<TMPro.TextMeshProUGUI>().text = first.nick;
                else if (go.name == "Score")
                    go.GetComponent<TMPro.TextMeshProUGUI>().text = first.score;
            }
            FirstNick.transform.Find("Arrow").gameObject.SetActive(first.has_turn);

            FirstNick.SetActive(true);
        }

        if (nicks.TryGetValue("2", out var second))
        {
            foreach (var go in SecondNick.GetComponentsInChildren<TMPro.TextMeshProUGUI>())
            {
                if (go.name == "Nick")
                    go.GetComponent<TMPro.TextMeshProUGUI>().text = second.nick;
                else if (go.name == "Score")
                    go.GetComponent<TMPro.TextMeshProUGUI>().text = second.score;
                SecondNick.transform.Find("Arrow").gameObject.SetActive(second.has_turn);

            }

            SecondNick.SetActive(true);
        }

        if (nicks.TryGetValue("3", out var third))
        {
            foreach (var go in ThirdNick.GetComponentsInChildren<TMPro.TextMeshProUGUI>())
            {
                if (go.name == "Nick")
                    go.GetComponent<TMPro.TextMeshProUGUI>().text = third.nick;
                else if (go.name == "Score")
                    go.GetComponent<TMPro.TextMeshProUGUI>().text = third.score;
                ThirdNick.transform.Find("Arrow").gameObject.SetActive(third.has_turn);

            }

            ThirdNick.SetActive(true);
        }

        if (nicks.TryGetValue("4", out var fourth))
        {
            foreach (var go in FourthNick.GetComponentsInChildren<TMPro.TextMeshProUGUI>())
            {
                if (go.name == "Nick")
                    go.GetComponent<TMPro.TextMeshProUGUI>().text = fourth.nick;
                else if (go.name == "Score")
                    go.GetComponent<TMPro.TextMeshProUGUI>().text = fourth.score;
                FourthNick.transform.Find("Arrow").gameObject.SetActive(fourth.has_turn);

            }

            FourthNick.SetActive(true);
        }
    }
}