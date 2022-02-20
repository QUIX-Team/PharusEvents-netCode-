using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    /*
        used in the playable levels (as empty)

        access all UI in that lelvel
        access NetworManager (singleton)

        for displaying UI stuff

        NOT FINISHED;;;

     */
    [SerializeField] Button startServerButton;

    [SerializeField] Button startHostButton;

    [SerializeField] Button startClientButton;

    [SerializeField] TextMeshProUGUI playersInGameText;

    private void Awake()
    {
        Cursor.visible = true;
    }

    void Start()
    {
        startHostButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartHost())
            {
                Debug.Log("host started ...");
            }
            else
            {
                Debug.Log("host could noit start ...");

            }
        });

        startServerButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartServer())
            {
                Debug.Log("server started ...");
            }
            else
            {
                Debug.Log("server could noit start ...");

            }

        });

        startClientButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartClient())
            {
                Debug.Log("client started ...");
            }
            else
            {
                Debug.Log("client could noit start ...");

            }

        });


    }

    void Update()
    {
        playersInGameText.text = $"players: {PlayersManager.Instance.PlayersInGame}";
    }
}
