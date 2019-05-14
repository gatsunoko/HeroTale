using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScript : MonoBehaviour {

  public Vector2 leftUp;
  public Vector2 rightUp;
  public Vector2 rightDown;
  public Vector2 leftDown;
  public GameObject whitTip;

  void Start() {
    var parent = this.transform;
    //横線描画
    float viewX = leftUp.x;
    while (viewX < rightUp.x) {
      GameObject tip = Instantiate(whitTip, parent) as GameObject;
      tip.transform.position = new Vector2(viewX, leftUp.y);
      tip = Instantiate(whitTip, parent) as GameObject;
      tip.transform.position = new Vector2(viewX, leftDown.y);
      viewX += 0.2f;
    }
    //縦線描画
    float viewY = leftUp.y;
    while (viewY > leftDown.y) {
      GameObject tip = Instantiate(whitTip, parent) as GameObject;
      tip.transform.position = new Vector2(leftUp.x, viewY);
      tip = Instantiate(whitTip, parent) as GameObject;
      tip.transform.position = new Vector2(rightUp.x, viewY);
      viewY -= 0.2f;
    }
  }

  void Update() {

  }
}
