using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class NetworkResponseService<T> where T : PacketResponse
{
    private NetworkResponseService() { }
    private readonly Dictionary<Guid, TaskCompletionSource<T>> _callbacks = new Dictionary<Guid, TaskCompletionSource<T>>();

    private static NetworkResponseService<T> _instance;
    internal static NetworkResponseService<T> Instance
    {
        get
        {
            if (_instance == null)
                _instance = new NetworkResponseService<T>();

            return _instance;
        }
    }

    internal void OnReceivePacket(T packet)
    {
        if (_callbacks.TryGetValue(packet.UId, out var cts))
        {
            try
            {
                cts.SetResult(packet);
                _callbacks[packet.UId] = null;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Unexpected packet type: {packet.GetType().Name} exception {ex.Message}");
            }
        }
    }


    internal void SubscribeCallback(Guid packetId, Action<T> callback, MonoBehaviour sender, bool needToCheckSender = true, int timeoutMs = 30_000)
    {
        if (callback == null)
            return;

        var tcs = new TaskCompletionSource<T>();
        var taskTimeout = Task.Delay(timeoutMs);

        var task = tcs.Task.ContinueWith(t =>
        {
            if (t.IsCanceled || t.IsFaulted || !t.IsCompleted)
            {
                Debug.LogError($"Some problem with executing network task, packet id is {packetId} {sender} is {sender} exception is {t.Exception}");
            }

            if (!needToCheckSender || sender != null)
                callback(t.Result);
            else if (sender == null && needToCheckSender)
                Debug.LogError("Sender of network call is null at the moment of receiving of the response, type of response is {nameof(T)} sender is {sender}");

            //следующая строчка нужна чтобы таска заканчивалась в том же потоке в котором и запрашивалась,
            //чтобы не было ошибок связанных с тем что до объекта в юнити кто-то пытается достучаться не из main thread
        }, TaskScheduler.FromCurrentSynchronizationContext());

        Task.WhenAny(task, taskTimeout).ContinueWith(t =>
        {
            if (t == taskTimeout)
            {
                Debug.LogError($"Network task was cancalled, response type is {nameof(T)}, packet id is {packetId}, timeout is {taskTimeout}");
                tcs.TrySetCanceled();
            }

            _callbacks.Remove(packetId);

            //следующая строчка нужна чтобы таска заканчивалась в том же потоке в котором и запрашивалась,
            //чтобы не было ошибок связанных с тем что до объекта в юнити кто-то пытается достучаться не из main thread
        }, TaskScheduler.FromCurrentSynchronizationContext());

        _callbacks[packetId] = tcs;
    }
}
