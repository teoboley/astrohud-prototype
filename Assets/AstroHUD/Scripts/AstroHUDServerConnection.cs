using System;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[System.Serializable] public class _UnityEventMessage : UnityEvent<IMessage> { }

public class AstroHUDServerConnection : MonoBehaviour
{
    [Header("On message event")]
    public _UnityEventMessage messageReceived;


    // State object for receiving data from remote device.  
    //public class StateObject
    //{
    //    // Client socket.  
    //    public Socket workSocket = null;
    //    // Size of receive buffer.  
    //    public const int BufferSize = 1024;
    //    // Receive buffer.  
    //    public byte[] buffer = new byte[BufferSize];
    //    // Received data string.  
    //    public StringBuilder sb = new StringBuilder();
    //}

    // The port number for the remote device.  
    //private IPAddress ipAddress = IPAddress.Parse("192.168.1.21");
    //private const int port = 5000;
    private Uri uri = new Uri("ws://192.168.1.21:5000");

    // ManualResetEvent instances signal completion.  
    //private static ManualResetEvent connectDone =
    //    new ManualResetEvent(false);
    //private static ManualResetEvent sendDone =
    //    new ManualResetEvent(false);
    //private static ManualResetEvent receiveDone =
    //    new ManualResetEvent(false);

    // The response from the remote device.  
    //private static String response = String.Empty;

    //Socket client;

    public ClientWebSocket clientWebSocket;

    // Start is called before the first frame update
    async void Start()
    {
        clientWebSocket = new ClientWebSocket();

        Debug.Log("[WS]:Attempting connection.");
        try
        {
            await clientWebSocket.ConnectAsync(uri, CancellationToken.None);
            if (clientWebSocket.State == WebSocketState.Open)
            {
                await SendMessage(new HelloMessage());
                while (true)
                {
                    // receive
                    ArraySegment<byte> bytesReceived = new ArraySegment<byte>(new byte[1024]);
                    WebSocketReceiveResult result = await clientWebSocket.ReceiveAsync(
                        bytesReceived,
                        CancellationToken.None
                    );
                    String response = Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count);
                    Debug.Log("[WS]Received: " + response);
                    IMessage responseMsg = JsonConvert.DeserializeObject<IMessage>(response);
                    messageReceived.Invoke(responseMsg);
                }
            }
            Debug.Log("[WS][connect]:" + "Connected");
        }
        catch (WebSocketException e)
        {
            Debug.Log("[WS][exception]:" + e.GetType() + " " + e.Message);
            if (e.InnerException != null)
            {
                Debug.Log("[WS][inner exception]:" + e.InnerException.Message);
            }
        }
    }

    async void OnDestroy()
    {
        await clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
    }

    //void Start()
    //{
    //    if (messageReceived == null)
    //    {
    //        Debug.Log("Creating server message event ref");
    //        messageReceived = new _UnityEventMessage();
    //    }

    //    // Connect to a remote device.  
    //    try
    //    {
    //        // Establish the remote endpoint for the socket.  
    //        IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

    //        // Create a TCP/IP socket.  
    //        client = new Socket(ipAddress.AddressFamily,
    //            SocketType.Stream, ProtocolType.Tcp);

    //        // Connect to the remote endpoint.  
    //        client.BeginConnect(remoteEP,
    //            new AsyncCallback(ConnectCallback), client);
    //        connectDone.WaitOne();

    //        Debug.Log("Connected to server");

    //        // Send test data to the remote device.  
    //        SendMessage(new Message<bool>("HELLO", false));
    //        sendDone.WaitOne();

    //        //// Receive the response from the remote device.  
    //        Receive(client);

    //        //// Write the response to the console.  
    //        //Console.WriteLine("Response received : {0}", response);

    //        //// Release the socket.  
    //        //client.Shutdown(SocketShutdown.Both);
    //        //client.Close();

    //    }
    //    catch (Exception e)
    //    {
    //        Console.WriteLine(e.ToString());
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    private async Task SendMessage<T>(Message<T> message)
    {
        ArraySegment<byte> bytesToSend = new ArraySegment<byte>(
            Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message))
        );
        await clientWebSocket.SendAsync(
            bytesToSend,
            WebSocketMessageType.Text,
            true,
            CancellationToken.None
        );
    }

    //private void SendMessage<T>(Message<T> message)
    //{
    //    Send(client, JsonUtility.ToJson(message));
    //}

    //private static void ConnectCallback(IAsyncResult ar)
    //{
    //    try
    //    {
    //        // Retrieve the socket from the state object.  
    //        Socket client = (Socket)ar.AsyncState;

    //        // Complete the connection.  
    //        client.EndConnect(ar);

    //        Console.WriteLine("Socket connected to {0}",
    //            client.RemoteEndPoint.ToString());

    //        // Signal that the connection has been made.  
    //        connectDone.Set();
    //    }
    //    catch (Exception e)
    //    {
    //        Console.WriteLine(e.ToString());
    //    }
    //}

    //private void Receive(Socket client)
    //{
    //    Debug.Log("Awaiting message from server");
    //    try
    //    {
    //        // Create the state object.  
    //        StateObject state = new StateObject();
    //        state.workSocket = client;

    //        // Begin receiving the data from the remote device.  
    //        client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
    //            new AsyncCallback(ReceiveCallback), state);
    //    }
    //    catch (Exception e)
    //    {
    //        Console.WriteLine(e.ToString());
    //    }
    //}

    //private void ReceiveCallback(IAsyncResult ar)
    //{
    //    try
    //    {
    //        // Retrieve the state object and the client socket
    //        // from the asynchronous state object.  
    //        StateObject state = (StateObject)ar.AsyncState;
    //        Socket client = state.workSocket;

    //        // Read data from the remote device.  
    //        int bytesRead = client.EndReceive(ar);
    //        Debug.Log("Bytes read: " + bytesRead);

    //        if (bytesRead > 0)
    //        {
    //            // There might be more data, so store the data received so far.  
    //            state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

    //            // Get the rest of the data.  
    //            client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
    //                new AsyncCallback(ReceiveCallback), state);
    //        }
    //        else
    //        {
    //            Debug.Log("No bytes read");
    //            // All the data has arrived; put it in response.  
    //            if (state.sb.Length > 1)
    //            {
    //                Debug.Log("Complete message from server received");
    //                response = state.sb.ToString();
    //                IMessage responseMsg = JsonUtility.FromJson<IMessage>(response);
    //                messageReceived.Invoke(responseMsg);
    //            }
    //            // Signal that all bytes have been received.  
    //            receiveDone.Set();

    //            // restart receive loop
    //            Receive(client);
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        Console.WriteLine(e.ToString());
    //    }
    //}

    //private void Send(Socket client, String data)
    //{
    //    // Convert the string data to byte data using ASCII encoding.  
    //    byte[] byteData = Encoding.ASCII.GetBytes(data);

    //    // Begin sending the data to the remote device.  
    //    client.BeginSend(byteData, 0, byteData.Length, 0,
    //        new AsyncCallback(SendCallback), client);
    //}

    //private void SendCallback(IAsyncResult ar)
    //{
    //    try
    //    {
    //        // Retrieve the socket from the state object.  
    //        Socket client = (Socket)ar.AsyncState;

    //        // Complete sending the data to the remote device.  
    //        int bytesSent = client.EndSend(ar);
    //        Console.WriteLine("Sent {0} bytes to server.", bytesSent);

    //        // Signal that all bytes have been sent.  
    //        sendDone.Set();
    //    }
    //    catch (Exception e)
    //    {
    //        Console.WriteLine(e.ToString());
    //    }
    //}
}

[Serializable]
public struct IMessage
{
    public String type;
    public JRaw payload;
}

[Serializable]
public abstract class Message<T>
{
    public String type;
    public T payload;

    protected Message(String type, T payload)
    {
        this.type = type;
        this.payload = payload;
    }

    protected Message(IMessage message)
    {
        this.type = message.type;
        this.payload = PayloadFromJson(message.payload);
    }

    public abstract T PayloadFromJson(JRaw payload);
}

// MESSAGE TYPES

public class HelloMessage : Message<bool>
{
    public HelloMessage() : base("HELLO", false) { }

    public override bool PayloadFromJson(JRaw payload)
    {
        return false;
    }
}

public class SystemStateMessage : Message<SystemStateMessagePayload>
{
    public SystemStateMessage(IMessage message) : base(message) { }

    public override SystemStateMessagePayload PayloadFromJson(JRaw payload)
    {
        return JsonConvert.DeserializeObject<SystemStateMessagePayload>(payload.ToString());
    }

    public static bool Is(IMessage message)
    {
        return message.type == "SYSTEM_STATE";
    }
}

[Serializable]
public struct SystemStateMessagePayload
{
    public LifeSupportState lifeSupportState;
    //public NavigationState navigationState;
    public MissionState missionState;
}

[Serializable]
public struct LifeSupportState {
    public BodyState bodyState;
    public SuitState suitState;
}

[Serializable]
public struct BodyState {
    // kCal
    public int caloriesBurned;
    // Degrees celsius
    public float bodyTemperature;
}

[Serializable]
public struct SuitState {
    // Milliampere hours
    public float currentBattery;
    // Milliampere hours
    public float maxBattery;
    // Milliampere hours per minute
    public float batteryDrain;
    // Litres of liquid?
    public float maxOxygen;
    // Litres of liquid?
    public float currentOxygen;
    // PSI
    public int tankPressure;
    // Litres per minute
    public float currentOxygenConsumption;
    // Percentage
    public float humidity;
    // Cycles per second
    public float radioactivity;
    // BPM
    public int heartRate;
    // PSI
    public int suitPressure;
}

public class TaskListMessage : Message<TaskListMessagePayload>
{
    public TaskListMessage(IMessage message) : base(message) { }

    public override TaskListMessagePayload PayloadFromJson(JRaw payload)
    {
        return JsonConvert.DeserializeObject<TaskListMessagePayload>(payload.ToString());
    }

    public static bool Is(IMessage message)
    {
        return message.type == "TASK_LIST";
    }
}

[Serializable]
public struct TaskListMessagePayload
{
    public List<TaskState> tasks;
}

[Serializable]
public struct TaskState
{
    public string description;
    public string time;
    public string[] subtasks;
}

[Serializable]
public struct MissionState
{
    // seconds
    public int totalMissionLength;
    // seconds
    public int missionTimeElapsed;
}