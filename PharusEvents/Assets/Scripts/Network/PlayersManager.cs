using Unity.Netcode;

public class PlayersManager : NetworkBehaviour
{

    /* (singlton)
    used in the playable levels (as empty)

    for managing defferent playes

    NOT FINISHED;;;

 */

    public static PlayersManager Instance { get; private set; }

    private NetworkVariable<int> playersInGame = new NetworkVariable<int>();

    public int PlayersInGame
    {
        get
        {
            return playersInGame.Value;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {
            if (IsServer)
            {
                playersInGame.Value++;
            }
        };

        NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
        {
            if (IsServer)
            {
                playersInGame.Value--;
            }
        };
    }

}
