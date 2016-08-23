using UnityEngine;
using System.Collections;

public class Thunder : MonoBehaviour {

    public Sprite[] sprites = new Sprite[5];

    public float moveTime = 0.1f;
    private float inverseMoveTime;

    public enum ThunderDirection { LEFT, RIGHT, UP, DOWN};
    private ThunderDirection currentDirection = ThunderDirection.RIGHT;

    public Module.Color color;
    private Rigidbody2D rb2D;

    // Use this for initialization
    void Start () {
    
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
        //currentDirection = RIGHT; //default setting

        //color = Module.Color.YELLOW;
    }

    public void setColor(int colorValue)
    {
        color = Module.parseColor(colorValue);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[colorValue];
    }
    public Module.Color getColor()
    {
        return color;
    }
   
    public void setDirection(ThunderDirection direction)
    {
        currentDirection = direction;
    }

    public ThunderDirection getDirection()
    {
        return currentDirection;
    }
  
    protected void Move(int xDir, int yDir) //
    {
        Vector2 start = transform.position; //자동 형변환, 데이터 손실 발생
        Vector2 end = start + new Vector2(xDir, yDir);
        //새로운 위치
        Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime); //inverseMoveTime에 맞추어서 작동 
                            //현재위치에서  목적지로간다  0.1초의 역수인 10초 * deltaTime 의 시간을 걸쳐서
        rb2D.MovePosition(newPosition);
     
    }

    // Update is called once per frame
    void FixedUpdate () {
        //if(transform.position.x <= 0 || transform.position.x >= BoardManager.columns || transform.position.y <= 0 || transform.position.y >= BoardManager.rows)
        //   this.gameObject.SetActive(false);
        
        switch (currentDirection)
        {
            case ThunderDirection.UP:
                    Move(0, 2);
                break;
            case ThunderDirection.DOWN:
                    Move(0, -2);
                break;
            case ThunderDirection.RIGHT:
                    Move(2, 0);
                break;
            case ThunderDirection.LEFT:
                    Move(-2, 0);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Module" || other.tag == "Destination" || other.tag=="Border")
        {
            Destroy(gameObject);
        }
        
    }

}
