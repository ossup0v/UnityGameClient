using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;

public class NetworkClient : MonoBehaviour
{
    public static int dataBufferSize = 1024 * 4;

    public string Ip;
    public int Port = 26950;
    public Guid MyId;
    public TCP Tcp;
    public UDP Udp;

    private bool isConnected = false;
    private delegate void PacketHandler(Packet packet);
    private static Dictionary<int, PacketHandler> PacketHandlers;

    private void OnApplicationQuit()
    {
        Disconnect();
    }

    public void ConnectToServer(string serverIp, int? port = null)
    {
        if (port.HasValue)
            Port = port.Value;

        Ip = serverIp;
        Tcp = new TCP(this);
        Udp = new UDP(this);
        
        InitializeClientData();

        isConnected = true;
        Tcp.Connect();
    }

    public class TCP
    {
        public TCP(NetworkClient owner)
        {
            _owner = owner;
        }

        public TcpClient Socket;
        private NetworkStream stream;
        private Packet receivedData;
        private byte[] receiveBuffer;
        private NetworkClient _owner;

        internal void SendData(Packet packet)
        {
            try
            {
                if (Socket != null)
                {
                    stream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null);
                }
            }
            catch (Exception ex)
            {
                Debug.Log($"Exception was throwed on call {nameof(SendData)}, ex is {ex.Message}");

            }
        }

        public void Connect()
        {
            Socket = new TcpClient()
            {
                ReceiveBufferSize = dataBufferSize,
                SendBufferSize = dataBufferSize
            };

            receiveBuffer = new byte[dataBufferSize];
            Socket.BeginConnect(_owner.Ip, _owner.Port, ConnectCallback, null);
        }

        private void ConnectCallback(IAsyncResult result)
        {
            Socket.EndConnect(result);

            if (!Socket.Connected)
            {
                return;
            }

            stream = Socket.GetStream();

            receivedData = new Packet();

            stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                var byteLength = stream.EndRead(result);
                if (byteLength < 0)
                {
                    _owner.Disconnect();
                    return;
                }

                byte[] data = new byte[byteLength];
                Array.Copy(receiveBuffer, data, byteLength);

                receivedData.Reset(HandleData(data));
                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
            }
            catch (Exception ex)
            {
                _owner.Disconnect();
                Debug.LogError($"Client with {_owner.MyId} get error with message {ex.Message}");
            }
        }

        private bool HandleData(byte[] data)
        {
            var packetLength = 0;

            receivedData.SetBytes(data);

            if (receivedData.UnreadLength() >= 4)
            {
                packetLength = receivedData.ReadInt();
                if (packetLength <= 0)
                {
                    return true;
                }
            }

            while (packetLength > 0 && packetLength <= receivedData.UnreadLength())
            {
                byte[] packetBytes = receivedData.ReadBytes(packetLength);
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (var packet = new Packet(packetBytes))
                    {
                        int packetId = packet.ReadInt();
                        PacketHandlers[packetId](packet);
                    }
                });

                packetLength = 0;

                if (receivedData.UnreadLength() >= 4)
                {
                    packetLength = receivedData.ReadInt();
                    if (packetLength <= 0)
                    {
                        return true;
                    }
                }
            }

            if (packetLength <= 1)
                return true;

            return false;
        }

        /// <summary>Disconnects from the server and cleans up the TCP connection.</summary>
        private void Disconnect()
        {
            _owner.Disconnect();

            stream = null;
            receivedData = null;
            receiveBuffer = null;
            Socket = null;
        }
    }

    public class UDP
    {
        public UdpClient Socket;
        public IPEndPoint EndPoint;
        private readonly NetworkClient _owner;

        public UDP(NetworkClient owner)
        {
            _owner = owner;
            EndPoint = new IPEndPoint(IPAddress.Parse(_owner.Ip), _owner.Port);
        }

        public void Connect(int localPort)
        {
            Socket = new UdpClient(localPort);

            Socket.Connect(EndPoint);
            Socket.BeginReceive(ReceiveCallback, null);

            using (Packet packet = new Packet())
            {
                SendData(packet);
            }
        }

        public void SendData(Packet packet)
        {
            try
            {
                packet.InsertGuid(_owner.MyId);
                if (Socket != null)
                {
                    Socket.BeginSend(packet.ToArray(), packet.Length(), null, null);
                }
                else
                {
                    Debug.Log("Trying to send packet when socket is null!  UDP.SendData");
                }
            }
            catch (Exception ex)
            {
                Debug.Log($"some error was throwed on call {nameof(UDP)}.{nameof(SendData)}, message {ex.Message}");
                //TODO disconnect
            }
        }

        private void ReceiveCallback(IAsyncResult res)
        {
            try
            {
                var data = Socket.EndReceive(res, ref EndPoint);
                Socket.BeginReceive(ReceiveCallback, null);

                if (data.Length < 4)
                {
                    Disconnect();
                    return;
                }

                HandleData(data);
            }
            catch (Exception ex)
            {
                Debug.Log($"some error was throwed on call {nameof(UDP)}.{nameof(ReceiveCallback)}, message {ex.Message}");
                Disconnect();
            }
        }

        private void HandleData(byte[] data)
        {
            using (var packet = new Packet(data))
            {
                var packetLength = packet.ReadInt();
                data = packet.ReadBytes(packetLength);
            }

            ThreadManager.ExecuteOnMainThread(() =>
            {
                using (Packet packet = new Packet(data))
                {
                    var packetId = packet.ReadInt();
                    PacketHandlers[packetId](packet);
                }
            });
        }
        /// <summary>Disconnects from the server and cleans up the UDP connection.</summary>
        private void Disconnect()
        {
            _owner.Disconnect();

            EndPoint = null;
            Socket = null;
        }

    }
    private void InitializeClientData()
    {
        PacketHandlers = new Dictionary<int, PacketHandler>
        {
            [(int)ToClient.welcome] = NetworkClientHandler.Welcome,
            [(int)ToClient.spawnPlayer] = NetworkClientHandler.SpawnPlayer,
            [(int)ToClient.playerPosition] = NetworkClientHandler.PlayerPosition,
            [(int)ToClient.playerRotation] = NetworkClientHandler.PlayerRotation,
            [(int)ToClient.playerDisconnected] = NetworkClientHandler.PlayerDisconnected,
            [(int)ToClient.playerHealth] = NetworkClientHandler.PlayerHealth,
            [(int)ToClient.playerRespawned] = NetworkClientHandler.PlayerRespawned,
            [(int)ToClient.createItemSpawner] = NetworkClientHandler.CreateItemSpawner,
            [(int)ToClient.itemSpawned] = NetworkClientHandler.ItemSpawned,
            [(int)ToClient.itemPickup] = NetworkClientHandler.ItemPickup,
            [(int)ToClient.spawnProjectile] = NetworkClientHandler.SpawnProjectile,
            [(int)ToClient.projectilePosition] = NetworkClientHandler.ProjectilePosition,
            [(int)ToClient.projectileExploded] = NetworkClientHandler.ProjectileExploded,
            [(int)ToClient.playerChooseWeapon] = NetworkClientHandler.PlayerChooseWeapon,
            [(int)ToClient.playerShooting] = NetworkClientHandler.PlayerShoot,
            [(int)ToClient.playerHit] = NetworkClientHandler.PlayerHit,
            [(int)ToClient.ratingTableInit] = NetworkClientHandler.RatingTableInit,
            [(int)ToClient.ratingTableNewPlayer] = NetworkClientHandler.RatingTableNewPlayer,
            [(int)ToClient.ratingTableUpdateKillAndDeath] = NetworkClientHandler.RatingTableUpdateKillAndDeath,
            [(int)ToClient.ratingTableUpdateDeath] = NetworkClientHandler.RatingTableUpdateDeath,
            [(int)ToClient.playerGrenadeCount] = NetworkClientHandler.PlayerGrenadeCount,
            [(int)ToClient.initMap] = NetworkClientHandler.InitMap,
            [(int)ToClient.playerScale] = NetworkClientHandler.PlayerScale,            
            [(int)ToClient.spawnBot] = NetworkClientHandler.SpawnBot,
            [(int)ToClient.botPosition] = NetworkClientHandler.BotPosition,
            [(int)ToClient.botRotation] = NetworkClientHandler.BotRotation,
            [(int)ToClient.botHealth] = NetworkClientHandler.BotHealth,
            [(int)ToClient.botShoot] = NetworkClientHandler.BotShoot,
            [(int)ToClient.botChooseWeapon] = NetworkClientHandler.BotChooseWeapon,
            [(int)ToClient.botHit] = NetworkClientHandler.BotHit,
            [(int)ToClient.ratingTableUpdateKilledBots] = NetworkClientHandler.RatingTableKilledBots,
            [(int)ToClient.roomPortToConnect] = NetworkClientHandler.ConnectToRoom,
            [(int)ToClient.roomList] = NetworkClientHandler.RoomList,
            [(int)ToClient.gameRoomSessionEnd] = NetworkClientHandler.GameRoomSessionEnd,
            [(int)ToClient.itemSpawnedOnMap] = NetworkClientHandler.ItemSpawnedOnMap,
            [(int)ToClient.playerBulletAmount] = NetworkClientHandler.PlayerBulletAmount,
            [(int)ToClient.itemOnMapPickup] = NetworkClientHandler.ItemOnMapPickup,
            [(int)ToClient.response] = NetworkClientHandler.HandleResponce,
        };
        Debug.Log($"{nameof(InitializeClientData)} was called");
    }

    /// <summary>Disconnects from the server and stops all network traffic.</summary>
    public void Disconnect()
    {
        if (isConnected)
        {
            isConnected = false;
            Tcp.Socket.Close();
            Udp.Socket.Close();

            Debug.Log("Disconnected from server.");
        }
    }
}
