using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class WritePacketsPacketIDTest
{
    // [Test]
    // public void WritePacketsPacketIDHasNotCollisions()
    // {
    //     var foundPacketIDs = new HashSet<int>();
    //     var writePacketTypeByPacketID = new Dictionary<int, Type>();
    //     foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
    //     {
    //         foreach (var assemblyType in assembly.GetTypes())
    //         {
    //             if (assemblyType.IsSubclassOf(typeof(WritePacketBase)))
    //             {
    //                 var writePacketTmpInstance = Activator.CreateInstance(assemblyType) as WritePacketBase;
    //                 Debug.Log($"Found <b>{assemblyType.Name}</b> with PacketID <b>{writePacketTmpInstance.PacketID}</b>");
    //                 if (foundPacketIDs.Contains(writePacketTmpInstance.PacketID) == false)
    //                 {
    //                     foundPacketIDs.Add(writePacketTmpInstance.PacketID);
    //                     writePacketTypeByPacketID.Add(writePacketTmpInstance.PacketID, assemblyType);
    //                 }   
    //                 else
    //                 {
    //                     var typeByPacketID = writePacketTypeByPacketID[writePacketTmpInstance.PacketID];
    //                     Debug.LogError($"Same PacketID <b>{writePacketTmpInstance.PacketID}</b> <b>{assemblyType.Name}</b> with <b>{typeByPacketID.Name}</b>");
    //                     Assert.Fail();
    //                 }                 
    //             }
    //         }
    //     }
    // }
}
