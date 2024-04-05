using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ballBoom : MonoBehaviour
{
    public GameObject ballCheck;
    public float overlapRadius; // Bán kính để tìm các GameObject khác va chạm với "ball"
    public float widthBall;
    public SpriteRenderer ballRenderer;
    public Vector3 mousePosition;
    public Sprite spriteBoom;
    public Sprite spriteIce;
    public bool clickFire = false;
    private BoxCollider2D boxCollider;
    private Vector3 pointStart;
    public string[] tagsToSearch; // Mảng các tag cần tìm
    public string[] tagsMap; // Mảng các tag cần tìm
    // Start is called before the first frame update
    void Start()
    {
        pointStart = transform.position;
        boxCollider = GetComponent<BoxCollider2D>();
        ballRenderer = GetComponent<SpriteRenderer>();
        // chiều rộng của bóng
        widthBall = ballRenderer.bounds.size.x;
        overlapRadius = widthBall/2;
        tagsToSearch = new string[]{"ballHole", "ballStone","ballMap", "ballIce"};
        System.Array.Copy(tagsMap, tagsToSearch, tagsMap.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !fireBall.boolFire) // Kiểm tra xem người dùng đã nhấn chuột trái chưa
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Lấy vị trí của chuột trong không gian thế giới
            if(mousePosition.y > aimingLine.limitLine){
                ballCheck = GameObject.FindWithTag("ballCheck");
                clickFire = true;
            }
        }
        if(Input.GetMouseButtonUp(0)){
            clickFire = false;
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Lấy vị trí của chuột trong không gian thế giới
            if(mousePosition.y <= aimingLine.limitLine){
                foreach (GameObject ballReset in GameObject.FindGameObjectsWithTag("boomMap")){
                    ballReset.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ballReset.GetComponent<SpriteRenderer>().sprite;
                    if (ballReset.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name.EndsWith("Ice"))
                    {
                        ballReset.GetComponent<SpriteRenderer>().sprite = spriteIce;
                    }
                    else{
                        ballReset.gameObject.tag = "ballMap";
                    }
                    // Duyệt qua mỗi tag trong mảng tagsToSearch
                    foreach (string tagMap in tagsMap){
                        if(ballReset.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name == tagMap){
                            ballReset.gameObject.tag = tagMap;
                        }
                        if (ballReset.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name.EndsWith("Ice"))
                        {
                            ballReset.gameObject.tag = "ballIce";
                        }
                    }
                }
            }
        }
        if(clickFire){
            foreach (GameObject ballReset in GameObject.FindGameObjectsWithTag("boomMap")){
                ballReset.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ballReset.GetComponent<SpriteRenderer>().sprite;
                if (ballReset.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name.EndsWith("Ice"))
                {
                    ballReset.GetComponent<SpriteRenderer>().sprite = spriteIce;
                }
                else{
                    ballReset.gameObject.tag = "ballMap";
                }
                // Duyệt qua mỗi tag trong mảng tagsToSearch
                foreach (string tagMap in tagsMap){
                    if(ballReset.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name == tagMap){
                        ballReset.gameObject.tag = tagMap;
                    }
                    if (ballReset.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name.EndsWith("Ice"))
                    {
                        ballReset.gameObject.tag = "ballIce";
                    }
                }
            }
            if(gameObject.tag == "ballBoom"){
                
                // Duyệt qua mỗi tag trong mảng tagsToSearch
                foreach (string tagToSearch in tagsToSearch)
                {
                    // Lấy tất cả các Collider2D nằm trong bán kính nhất định
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(ballCheck.transform.position, overlapRadius*3f);
                    foreach (Collider2D col in colliders){
                        // Kiểm tra xem GameObject
                        if(col.gameObject.CompareTag(tagToSearch)){
                            // đổi màu
                            col.gameObject.GetComponent<SpriteRenderer>().sprite = col.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
                            col.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteBoom;
                            col.gameObject.tag = "boomMap";
                        }
                    }
                }
            }
            if(gameObject.tag == "ballLine"){
                // Lấy vector từ đối tượng đến target
                Vector2 vectorToTarget = (ballCheck.transform.position - pointStart).normalized;
                // Tính góc giữa vector A và trục Oy
                float angle = Mathf.Atan2(vectorToTarget.x, vectorToTarget.y) * Mathf.Rad2Deg;
                // Tạo một mảng chứa tất cả các GameObject va chạm với BoxCollider2D
                Collider2D[] colliders = Physics2D.OverlapBoxAll(boxCollider.bounds.center, new Vector2(widthBall, 20f), -angle);

                foreach (Collider2D col in colliders){
                    // Duyệt qua mỗi tag trong mảng tagsToSearch
                    foreach (string tagToSearch in tagsToSearch)
                    {
                        // Kiểm tra xem GameObject
                        if(col.gameObject.CompareTag(tagToSearch)){
                            // đổi màu
                            col.gameObject.GetComponent<SpriteRenderer>().sprite = col.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
                            col.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteBoom;
                            col.gameObject.tag = "boomMap";
                        }
                    }
                }
            }
            if(gameObject.tag == "ballLaze"){
                // Duyệt qua mỗi tag trong mảng tagsToSearch
                foreach (string tagToSearch in tagsToSearch)
                {
                    foreach (GameObject ballMap in GameObject.FindGameObjectsWithTag(tagToSearch)){
                        if(ballMap.transform.position.y <= ballCheck.transform.position.y + 0.1f && ballMap.transform.position.y >= ballCheck.transform.position.y - 0.1f){
                            // đổi màu
                            ballMap.gameObject.GetComponent<SpriteRenderer>().sprite = ballMap.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
                            ballMap.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteBoom;
                            ballMap.gameObject.tag = "boomMap";
                        }
                    }
                }
                // Lấy tất cả các Collider2D nằm trong bán kính nhất định
                Collider2D[] colliders = Physics2D.OverlapCircleAll(ballCheck.transform.position, overlapRadius);
                foreach (Collider2D col in colliders){
                    // Duyệt qua mỗi tag trong mảng tagsToSearch
                    foreach (string tagToSearch in tagsToSearch)
                    {
                        // Kiểm tra xem GameObject
                        if(col.gameObject.CompareTag(tagToSearch)){
                            // đổi màu
                            col.gameObject.GetComponent<SpriteRenderer>().sprite = col.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
                            col.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteBoom;
                            col.gameObject.tag = "boomMap";
                        }
                    }
                }
            }
            if(gameObject.tag == "ballRainbow"){
                Sprite[] spriteCheck = new Sprite[0];
                // Lấy tất cả các Collider2D nằm trong bán kính nhất định
                Collider2D[] colliders = Physics2D.OverlapCircleAll(ballCheck.transform.position, overlapRadius);
                foreach (Collider2D col in colliders){
                    // Duyệt qua mỗi tag trong mảng tagsToSearch
                    foreach (string tagToSearch in tagsToSearch)
                    {
                        if(col.gameObject.tag == tagToSearch && col.gameObject.tag != "ballHole" && col.gameObject.tag != "ballStone"){
                            Sprite newSprite = col.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
                            if (newSprite.name.EndsWith("Ice"))
                            {
                                newSprite = col.gameObject.GetComponent<SpriteRenderer>().sprite;
                            }
                            // Kiểm tra xem biến đã tồn tại trong mảng chưa
                            if (!spriteCheck.Contains(newSprite))
                            {
                                // Nếu không tồn tại, thêm vào cuối mảng
                                spriteCheck = spriteCheck.Concat(new Sprite[] { newSprite }).ToArray();
                            }
                        }
                    }
                }
                // Duyệt qua mỗi tag trong mảng tagsToSearch
                foreach (string tagToSearch in tagsToSearch)
                {
                    foreach (GameObject ballMap in GameObject.FindGameObjectsWithTag(tagToSearch)){
                        if (spriteCheck.Contains(ballMap.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite)){
                            // đổi màu
                            ballMap.gameObject.GetComponent<SpriteRenderer>().sprite = ballMap.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
                            ballMap.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteBoom;
                            ballMap.gameObject.tag = "boomMap";
                        }
                        if(ballMap.tag == "ballIce"){
                            if (spriteCheck.Contains(ballMap.gameObject.GetComponent<SpriteRenderer>().sprite)){
                                // đổi màu
                                spriteIce = ballMap.gameObject.GetComponent<SpriteRenderer>().sprite;
                                ballMap.gameObject.GetComponent<SpriteRenderer>().sprite = ballMap.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
                                ballMap.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteBoom;
                                ballMap.gameObject.tag = "boomMap";
                            }
                        }
                    }
                }
            }
        }
    }
}
