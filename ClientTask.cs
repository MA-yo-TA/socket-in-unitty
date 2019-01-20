using UnityEngine.UI;
using UnityEngine;

namespace Script.Client
{
    public class ClientTask : MonoBehaviour
    {
        public static ClientTask Instance;

        private string _receivedStr = "xxxx";  // 安易な初期値
#pragma warning disable 0649
        // 画面表示用
        [SerializeField] private Text _textField;
#pragma warning restore 0649

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {

            // 初期値なら更新しない
            if (_receivedStr == "xxxx")
            {
                return;
            }


            string str;
            if (_receivedStr == "send a message")
            {
                str = "receive a message";
            }
            else
            {
                str = "no valid message received";
            }
            _textField.text = str;

        }

        // 受信した文字列セット
        public void SetReceivedStr(string s)
        {
            _receivedStr = s;
        }

    }
}