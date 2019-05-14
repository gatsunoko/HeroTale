using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

  Rigidbody2D rigid2d;
  SpriteRenderer spriteRenderer;
  float key_x = 0;
  float key_y = 0;
  public float speed = 5.0f;
  public Vector2 minSpeed;
  public Vector2 maxSpeed;
  GameObject gameDirector;
  GameDirectorScript gameDirectorScript;
  public int condition = 1;
  //接地判定
  public LayerMask groundLayer;
  bool grounded_result = false;
  bool[] grounded = new bool[3] { false, false, false };
  bool canJump = true;
  float jumpTimer = 0;

  void Start() {
    rigid2d = GetComponent<Rigidbody2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    this.gameDirector = GameDirectorScript.gameDirectorScript.gameObject;
    this.gameDirectorScript = gameDirector.GetComponent<GameDirectorScript>();
  }

  void FixedUpdate() {
    if (key_x != 0) {
      this.rigid2d.velocity = new Vector2(key_x * speed, rigid2d.velocity.y);
    }
    else {
      this.rigid2d.velocity = new Vector2(0, rigid2d.velocity.y);
    }
    if (condition == 0) {
      if (key_y != 0) {
        this.rigid2d.velocity = new Vector2(rigid2d.velocity.x, key_y * speed);
      }
      else {
        this.rigid2d.velocity = new Vector2(rigid2d.velocity.x, 0);
      }
    }
    else if (condition == 1) {
      if (key_y != 0) {
        if (canJump) {
          this.rigid2d.velocity = new Vector2(rigid2d.velocity.x, key_y * speed);
        }
        else {
          this.rigid2d.velocity = new Vector2(rigid2d.velocity.x, rigid2d.velocity.y);
        }
      }
      else {
        this.rigid2d.velocity = new Vector2(rigid2d.velocity.x, rigid2d.velocity.y);
      }
    }

    float vy = Mathf.Clamp(rigid2d.velocity.y, minSpeed.y, maxSpeed.y);
    this.rigid2d.velocity = new Vector2(rigid2d.velocity.x, vy);
  }

  void Update() {
    if (condition == 0) {
      spriteRenderer.color = new Color(1, 0, 0, 1);
      rigid2d.gravityScale = 0;
    }
    else if (condition == 1) {
      spriteRenderer.color = new Color(0, 0, 1, 1);
      rigid2d.gravityScale = 2;

      //ジャンプできるかどうかの接地判定
      Vector2 linePos = transform.position;
      linePos.y -= 0.1034f;
      grounded[0] = Physics2D.Linecast(transform.position, linePos, groundLayer);
      linePos.x -= 0.0888f;
      grounded[1] = Physics2D.Linecast(transform.position, linePos, groundLayer);
      linePos.x += 0.0888f;
      linePos.x += 0.0888f;
      grounded[2] = Physics2D.Linecast(transform.position, linePos, groundLayer);

      //接地判定をして結果をgourended_result変数に入れる
      if ((grounded[0]) || (grounded[1]) || (grounded[2])) {
        grounded_result = true;
        //this.layerStatus.ladder = false;
        if (!this.gameDirectorScript.inputJump) {
          canJump = true;
          jumpTimer = 0;
        }
      }
      else {
        grounded_result = false;
      }

      if (!grounded_result && canJump) {
        //0.7秒押し続けたらジャンプ解除
        jumpTimer += Time.deltaTime;
        if (jumpTimer >= 0.7) {
          jumpTimer = 0;
          canJump = false;
        }
        //ジャンプボタン話したら連続ジャンプできないように
        if (this.gameDirectorScript.inputUpUp) {
          canJump = false;
        }
      }
    }

    if (this.gameDirectorScript.inputRight) {
      key_x = 1;
    }
    else if (this.gameDirectorScript.inputLeft) {
      key_x = -1;
    }
    else {
      key_x = 0;
    }
    if (this.gameDirectorScript.inputUp) {
      key_y = 1;
    }
    else if (this.gameDirectorScript.inputDown) {
      key_y = -1;
    }
    else {
      key_y = 0;
    }
  }
}
