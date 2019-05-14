using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirectorScript : MonoBehaviour {
  public static GameDirectorScript gameDirectorScript;

  //キー
  public bool inputUp;
  public bool inputUpUp;
  public bool inputDown;
  public bool inputRight;
  public bool inputLeft;
  public bool inputAttack;
  public bool inputJump;
  public bool inputJumpUp;
  public bool inputTime;


  void Awake() {
    gameDirectorScript = this;
  }

  void Start() {
  }

  void Update() {
    //ボタンが押されているかの状態を取得
    inputUp = jInput.GetKey(Mapper.InputArray[0]);
    inputUpUp = jInput.GetKeyUp(Mapper.InputArray[0]);
    inputDown = jInput.GetKey(Mapper.InputArray[1]);
    inputRight = jInput.GetKey(Mapper.InputArray[2]);
    inputLeft = jInput.GetKey(Mapper.InputArray[3]);
    inputAttack = jInput.GetKeyDown(Mapper.InputArray[4]);
    inputJump = jInput.GetKeyDown(Mapper.InputArray[5]);
    inputJumpUp = jInput.GetKeyUp(Mapper.InputArray[5]);
    inputTime = jInput.GetKeyDown(Mapper.InputArray[6]);
  }
}
