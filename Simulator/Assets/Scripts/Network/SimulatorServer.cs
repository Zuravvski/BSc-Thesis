using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class SimulatorServer : MonoBehaviour
{
    #region Public interface

    public GameObject prototype;

    #endregion

    #region Network related variables

    private TcpListener _server;
    private List<TcpClient> _clients;
    private bool playerWasAdded;
    private TcpClient _lastAddedClient;

    #endregion

    private GameObject _mainCamera;

	// Use this for initialization
    private void Start ()
	{
	    Application.runInBackground = true;
        _mainCamera = GameObject.FindWithTag("MainCamera");

        _server = new TcpListener(IPAddress.Any, 3001);
        _clients = new List<TcpClient>();
        _server.Start();
        print("The server is running...");
        AcceptClients();
    }
	
	// Update is called once per frame
    private void Update ()
	{
	    if (!playerWasAdded || !prototype) return;

	    if (_clients.Count > 1)
	    {
	        return;
	    }

        var robot = Instantiate(prototype, new Vector3(0, 1.0f, 0), Quaternion.identity);
	    robot.name = "Pololu 3pi #" + _clients.Count;
        robot.GetComponent<RobotControl>().SetClient(_lastAddedClient);
	    playerWasAdded = false;
	}

    private void OnApplicationQuit()
    {
        if (_server != null)
        {
            _server.Stop();
        }
        print("The server has been stopped.");
    }

    public void HandleDisconnection(GameObject robot, TcpClient client)
    {
        if (_clients.Count == 1)
        {
            _mainCamera.SetActive(true);
        }

        _clients.Remove(client);
        Destroy(robot);
    }

    private void AcceptClients()
    {
        _server.BeginAcceptTcpClient(AcceptorCallback, null);
    }

    private void AcceptorCallback(IAsyncResult result)
    {
        var client = _server.EndAcceptTcpClient(result);
        print("New client has been connected");
        _clients.Add(client);
        _lastAddedClient = client;
        playerWasAdded = true;
        AcceptClients();
    }
}
