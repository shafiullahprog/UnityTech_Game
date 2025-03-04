using UnityEngine;
using Mirror;
using UnityEngine.UI;
using TMPro;
public class CustomNetworkManager : MonoBehaviour
{
    public  TMP_InputField ipInputField;
    public Button hostButton, serverButton, clientButton;

    private void Start()
    {
        // Assign button click events
        hostButton.onClick.AddListener(StartHost);
        serverButton.onClick.AddListener(StartServer);
        clientButton.onClick.AddListener(StartClient);
    }

    public void StartHost()
    {
        if (NetworkServer.active || NetworkClient.isConnected) return;
        Debug.Log("Starting Host...");
        NetworkManager.singleton.StartHost();
    }

    public void StartServer()
    {
        if (NetworkServer.active) return;
        Debug.Log("Starting Server...");
        NetworkManager.singleton.StartServer();
    }

    public void StartClient()
    {
        if (NetworkClient.isConnected) return;

        // Use default IP or input field value
        string ipAddress = string.IsNullOrEmpty(ipInputField?.text) ? "localhost" : ipInputField.text;
        NetworkManager.singleton.networkAddress = ipAddress;

        Debug.Log($"Connecting to server at {ipAddress}...");
        NetworkManager.singleton.StartClient();
    }
}
