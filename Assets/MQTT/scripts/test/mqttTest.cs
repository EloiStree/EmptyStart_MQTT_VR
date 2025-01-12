﻿using UnityEngine;
using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System;
public class mqttTest : MonoBehaviour {
    public string m_ipAddress = "143.185.118.233";
    public int m_port = 8080;
    private MqttClient client;
	void Start () {
		client = new MqttClient(IPAddress.Parse(m_ipAddress),m_port , false , null ); 
		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 
		string clientId = Guid.NewGuid().ToString(); 
		client.Connect(clientId); 
		client.Subscribe(new string[] { "hello/world" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
	}
	void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e) 
	{ 
		Debug.Log("Received: " + System.Text.Encoding.UTF8.GetString(e.Message)  );
	}
	void OnGUI(){
		if ( GUI.Button (new Rect (20,40,80,20), "Level 1")) {
			Debug.Log("sending...");
			client.Publish("hello/world", System.Text.Encoding.UTF8.GetBytes("Sending from Unity3D!!!"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
			Debug.Log("sent");
		}
	}
}
