using TMPro;
using Unity.Collections;
using Unity.Netcode;

public class PlayerHUD : NetworkBehaviour
{

    /*
        to be used in the player

        access network string

        for showing player info (as an overlay)
     */
    private NetworkVariable<NetworkString> playerName = new NetworkVariable<NetworkString>();

    private bool overlySet = false;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            playerName.Value = $"user {OwnerClientId}";
        }
    }

    public void SetOverlay()
    {
        var localPlayerOverlay = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        localPlayerOverlay.text = playerName.Value;
    }

    void Update()
    {
        if(!overlySet && string.IsNullOrEmpty(playerName.Value))
        {
            SetOverlay();
            overlySet = true;
        }
    }
}
