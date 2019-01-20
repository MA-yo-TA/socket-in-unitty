using UnityEngine;
using UnityEngine.UI;

/*
 * 受信したメッセージを元に情報の管理をし、
 * 入力メッセージ入力のためのUIへの表示などをする
 * 通信用の非同期スレッドから直接Unityのメインスレッド呼ぶとエラーになるので一枚噛ませている
 */
namespace Script
{
	public class ServerTask : MonoBehaviour {
		// 受信した値など集約用のシステム
		public static ServerTask Instance;

		private string _receivedStr = "xxxx";  // 安易な初期値
		private string _ipPort = "none"; // 接続先情報保持用

#pragma warning disable 0649
		// 画面表示用
		[SerializeField]private Text _ipportField;
		[SerializeField]private Text _textField;
#pragma warning restore 0649

		private void Awake(){
			Instance = this;
		}

		private void Update () {
			// 接続先表示
			_ipportField.text = _ipPort;

			// 初期値なら更新しない
			if(_receivedStr == "xxxx"){
				return;
			}


			string str;
			if (_receivedStr == "PLEASE") {
				str = "send a message";
			}
            else {
                 str = "no valid message received";
            }
            _textField.text = str;

		}

		// 受信した文字列セット
		public void SetReceivedStr(string s){
			_receivedStr = s;
		}

		// 接続情報セット
		public void SetIpAddressPort(string ipport){
			_ipPort = ipport;
		}
	}
}
