using UnityEngine;
using System.Collections.Generic;

public class creatBallMap : MonoBehaviour
{
    public Sprite spriteStone,spriteHole;
    public Sprite spriteBlue,spriteRed,spriteViolet,spriteYellow;
    public static bool boolScroll = true;
    public GameObject ballMap;
    public GameObject ballIce;
    public Sprite spriteCreat;
    public bool boolCreat = false;
    public List<GameObject> gameObjects = new List<GameObject>();
    public List<GameObject> ballStones = new List<GameObject>();
    public string tagBall;
    public Vector3 clickPosition;
    public GameObject celingObject;
    private int checkClick = 0;
    void Update()
    {
        // Kiểm tra xem có sự kiện click chuột không
        if (Input.GetMouseButtonDown(0) && !boolScroll)
        {
            // Lấy vị trí của chuột trên màn hình và chuyển đổi thành vị trí trong thế giới game
            clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(clickPosition.y < (celingObject.transform.position.y - celingObject.transform.localScale.y/2f) && clickPosition.y > -2f){
                boolCreat = true;
            }
        }
        if(boolCreat){
            clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(clickPosition.y < (celingObject.transform.position.y - celingObject.transform.localScale.y/2f) && clickPosition.y > -2f){
                Collider2D[] colliders = Physics2D.OverlapCircleAll(clickPosition, 0.1f);
                
                // Duyệt qua tất cả các GameObject ở vị trí chuột
                foreach (Collider2D collider in colliders)
                {
                    if(collider.tag == "ballMap" || collider.tag == "ballCreat"){
                        if(tagBall == "ballStone" || tagBall == "ballHole"|| tagBall == "ballMap"){
                            Vector3 mapPosition = collider.gameObject.transform.position;
                            mapPosition.z -= 1;
                            AddGameObjectOld(collider.gameObject);
                            collider.gameObject.SetActive(false);
                            GameObject newBallMap = Instantiate(ballMap, mapPosition, Quaternion.identity); // tạo bóng
                            newBallMap.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteCreat; // gán img
                            newBallMap.transform.parent = transform;
                            newBallMap.tag = "ballFall";
                            AddGameObject(newBallMap);
                        }
                        if(tagBall == "ballIce"){
                            Vector3 mapPosition = collider.gameObject.transform.position;
                            mapPosition.z -= 1;
                            Sprite spriteRandom = collider.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
                            AddGameObjectOld(collider.gameObject);
                            collider.gameObject.SetActive(false);
                            GameObject newBallMap = Instantiate(ballMap, mapPosition, Quaternion.identity); // tạo bóng
                            newBallMap.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteRandom; // gán img
                            newBallMap.tag = "ballFall";
                            newBallMap.transform.parent = transform;
                            GameObject newBallIce = Instantiate(ballIce, mapPosition, Quaternion.identity); // tạo băng
                            newBallIce.transform.parent = newBallMap.transform;
                            newBallIce.transform.localScale = new Vector3(1,1,1);
                            AddGameObject(newBallMap);
                        }
                        if(tagBall == "ballFall"){
                            AddGameObjectOld(collider.gameObject);
                            collider.gameObject.SetActive(false);
                            Vector3 mapPosition = collider.gameObject.transform.position;
                            GameObject newBallMap = new GameObject("NewBallMap"); // Tạo một GameObject mới với tên là "NewBallMap"
                            newBallMap.transform.position = mapPosition; // Đặt vị trí của GameObject mới
                            newBallMap.tag = "ballFall";
                            AddGameObject(newBallMap);
                        }
                    }
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            boolCreat = false;
            foreach (GameObject ball in GameObject.FindGameObjectsWithTag("ballFall"))
            {
                ball.tag = "ballCreat";
            }
        }
    }
    public void _clickStone(){
        if(boolScroll){
            boolScroll = false;
            checkClick = 1;
        }
        else{
            if(checkClick == 1){
                boolScroll = true;
                checkClick = 0;
            }
            else{
                checkClick = 1;
            }
        }
        spriteCreat = spriteStone;
        tagBall = "ballStone";
    }
    public void _clickHole(){
        if(boolScroll){
            boolScroll = false;
            checkClick = 2;
        }
        else{
            if(checkClick == 2){
                boolScroll = true;
                checkClick = 0;
            }
            else{
                checkClick = 2;
            }
        }
        spriteCreat = spriteHole;
        tagBall = "ballHole";
    }
    public void _clickIce(){
        if(boolScroll){
            boolScroll = false;
            checkClick = 3;
        }
        else{
            if(checkClick == 3){
                boolScroll = true;
                checkClick = 0;
            }
            else{
                checkClick = 3;
            }
        }
        tagBall = "ballIce";
    }
    public void _clickDestroy(){
        if(boolScroll){
            boolScroll = false;
            checkClick = 4;
        }
        else{
            if(checkClick == 4){
                boolScroll = true;
                checkClick = 0;
            }
            else{
                checkClick = 4;
            }
        }
        tagBall = "ballFall";
    }
    public void _clickBlue(){
        if(boolScroll){
            boolScroll = false;
            checkClick = 5;
        }
        else{
            if(checkClick == 5){
                boolScroll = true;
                checkClick = 0;
            }
            else{
                checkClick = 5;
            }
        }
        spriteCreat = spriteBlue;
        tagBall = "ballMap";
    }
    public void _clickRed(){
        if(boolScroll){
            boolScroll = false;
            checkClick = 6;
        }
        else{
            if(checkClick == 6){
                boolScroll = true;
                checkClick = 0;
            }
            else{
                checkClick = 6;
            }
        }
        spriteCreat = spriteRed;
        tagBall = "ballMap";
    }
    public void _clickYellow(){
        if(boolScroll){
            boolScroll = false;
            checkClick = 7;
        }
        else{
            if(checkClick == 7){
                boolScroll = true;
                checkClick = 0;
            }
            else{
                checkClick = 7;
            }
        }
        spriteCreat = spriteYellow;
        tagBall = "ballMap";
    }
    public void _clickViolet(){
        if(boolScroll){
            boolScroll = false;
            checkClick = 8;
        }
        else{
            if(checkClick == 8){
                boolScroll = true;
                checkClick = 0;
            }
            else{
                checkClick = 8;
            }
        }
        spriteCreat = spriteViolet;
        tagBall = "ballMap";
    }
    void resetBall(){
        if(gameObjects.Count > 0){
            gameObjects[gameObjects.Count - 1].SetActive(true);
            RemoveGameObjectOld(gameObjects[gameObjects.Count - 1]);
        }
        if(ballStones.Count > 0){
            Destroy(ballStones[ballStones.Count - 1]);
            RemoveGameObject(ballStones[ballStones.Count - 1]);
        }
    }
    public void AddGameObjectOld(GameObject obj)
    {
        if (!gameObjects.Contains(obj))
        {
            gameObjects.Add(obj);
        }
    }    
    public void AddGameObject(GameObject obj)
    {
        if (!ballStones.Contains(obj))
        {
            ballStones.Add(obj);
        }
    }
    public void RemoveGameObjectOld(GameObject obj)
    {
        if (gameObjects.Contains(obj))
        {
            gameObjects.Remove(obj);
        }
    }
    public void RemoveGameObject(GameObject obj)
    {
        if (ballStones.Contains(obj))
        {
            ballStones.Remove(obj);
        }
    }
}
