using UnityEngine;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using static IPManager;

/*
 * TestServer.cs
 * SocketServerを継承、開くポートを指定して、送受信したメッセージを具体的に処理する
 */
namespace Script.SocketServer
{
	public class ServerTest : SocketServer {

#pragma warning disable 0649
        // ポート指定（他で使用していないもの、使用されていたら手元の環境によって変更）
        [SerializeField] private int _port;
#pragma warning restore 0649
        private void Start(){
			// 接続中のIPアドレスを取得
			var ipAddress = IPManager.GetIP(ADDRESSFAM.IPv4);
			// 指定したポートを開く
			Listen(ipAddress, _port);

			// システムに接続情報をセット（表示用）
			ServerTask.Instance.SetIpAddressPort (ipAddress + ":" + _port);
		}

		// クライアントからメッセージ受信
		protected override void OnMessage(string msg){
			base.OnMessage(msg);

			// 今回は受信した文字列によって返すメッセージを変える

			// ビュアーに値をセットする
			ServerTask.Instance.SetReceivedStr (msg);
      if (msg == "PLEASE")
      {
      	// クライアントに受領メッセージを返す
        SendMessageToClient ("send a message");
			} else {
				// クライアントにエラーメッセージを返す
				SendMessageToClient ("Error\n");
			}
		}

	}
}
