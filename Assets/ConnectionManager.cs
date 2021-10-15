using System;
using System.Collections.Generic;
using System.IO;
using BestHTTP.JSON;
using UnityEngine;
using BestHTTP.WebSocket;
using Newtonsoft.Json;

public class Item
{
    [JsonProperty("is_game_on")] public bool IsGameOn { get; set; }

    [JsonProperty("is_my_turn")] public bool isMyTurn { get; set; }

//    public string turn_id { get; set; }
    public List<BoardConfig> board { get; set; }
    public Dictionary<string, iii> nicks { get; set; }
    public DateTime timeStamp { get; set; }
    [JsonProperty("player_hand")] public List<string> playerHand { get; set; }
}

public class BoardConfig
{
    public int x { get; set; }
    public int y { get; set; }
    public string letter { get; set; }
}

public class Config
{
    // do not change variables names names
    public string player_id;
    public string room_id;
    public string server_address;
    public string player_nick;
}

public class ConnectionManager : MonoBehaviour
{
    private static WebSocket webSocket;
    private bool isMyTurn;
    private Config config;

    [SerializeField] GameObject waitingText;
    private Nicks nicks;
    private Timer timer;
    private buttons buttons;
    private Player player;
    private LetterManager letterManager;


    private const float connectTimeout = 3;
    private float timeFromLastConnectionRequest = connectTimeout;

    void Start()
    {
        nicks = GameObject.Find("Nicks").GetComponent<Nicks>();
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        buttons = GameObject.Find("buttons").GetComponent<buttons>();
        player = GameObject.Find("Player").GetComponent<Player>();
        letterManager = GameObject.Find("LetterManager").GetComponent<LetterManager>();


//        config = new Config
//        {
//            player_id = "t4", room_id = "1", server_address = "ws://localhost:5000/ws/", player_nick = "player"
//        }; // todo
    }

    private void Update()
    {
        if ((string.IsNullOrEmpty(config.player_id) || webSocket != null) &&
            (string.IsNullOrEmpty(config.player_id) || webSocket.IsOpen)) return;
        if (timeFromLastConnectionRequest >= connectTimeout)
        {
            webSocket = ConnectToServer(config);
            timeFromLastConnectionRequest = 0;
        }
        else
        {
            ClearDesk();
            waitingText.SetActive(true);
            timeFromLastConnectionRequest += Time.deltaTime;
        }
    }

    private void ClearDesk()
    {
        nicks.DeactivateNicks();
        buttons.RemoveButtons();
        player.RemoveLetters();
    }

    private WebSocket ConnectToServer(Config config)
    {
        string fullAddress = Path.Combine(config.server_address + config.room_id + "/"
                                          + config.player_id + "/" + config.player_nick);

        webSocket = new WebSocket(new Uri(fullAddress));
        webSocket.OnMessage += OnMessageRecieved;
        webSocket.Open();

        return webSocket;
    }

    private void OnMessageRecieved(WebSocket webSocket, string message)
    {
//        Debug.Log(message);
        Item item = JsonConvert.DeserializeObject<Item>(message);
        ClearDesk();
        if (item.IsGameOn)
        {
            waitingText.SetActive(false);
            timer.SetTimer(item.timeStamp);
            buttons.HandleServerData(item.board);
            nicks.ActivateNicks(item.nicks);
            player.NewLetters(item.playerHand);

        }
        else
        {
            nicks.ActivateNicks(item.nicks);
            waitingText.SetActive(true);
        }
    }

    public void SendUpdateToServer()
    {
        var tiles = buttons.SendDataToServer();
        Debug.Log(Json.Encode(tiles));
        webSocket.Send(Json.Encode(tiles));
    }

    public void ConfigFromJson(string json)
    {
        if (config == null)
            config = JsonUtility.FromJson<Config>(json);
    }


    public void SkipMove()
    {
        var d = new Dictionary<string, string>()
        {
            ["other"] = "skip",
            ["letter"] = letterManager.GetChosenLetter().ToString(),
        };
        Debug.Log(Json.Encode(d));
        webSocket.Send(Json.Encode(d));
    }

    public void Refresh()
    {
        var d = new Dictionary<string, string>()
        {
            ["other"] = "refresh"
        };
        webSocket.Send(Json.Encode(d));
    }
}