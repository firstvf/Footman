using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField _createInput;
    public InputField _joinInput;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(_createInput.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(_createInput.text);
    }

    public override void OnJoinedRoom()
    {
        //base.OnJoinedRoom();
        PhotonNetwork.LoadLevel("Game");
    }
}