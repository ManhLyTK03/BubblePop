using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creatBall : MonoBehaviour
{
    public static bool isCreat;
    public GameObject ballFire; // Quả bóng mẫu bắn
    public Transform pointFire; // Điểm tạo bóng bắn
    public GameObject ballNext; // Quả bóng mẫu next
    public Transform pointNext; // Điểm tạo bóng next
    public GameObject newBallFire; // bóng bắn
    public GameObject newBallNext; // bóng next
    public string[] ballCollor;
    public Sprite[] ballSprites;
    private Dictionary<string, Sprite> spriteDictionary = new Dictionary<string, Sprite>(); // Dictionary để ánh xạ tên và Sprite tương ứng
    private HashSet<string> uniqueColors = new HashSet<string>();
    public static string n; // Biến lưu trữ màu sắc bóng next
    public static string m; // Biến lưu trữ màu sắc bóng bắn
    public Sprite fireSprite;
    public Sprite nextSprite;
    private bool checkStart = false;
    // Start is called before the first frame update
    void Start(){
        // Điền thông tin từ mảng vào Dictionary
        foreach (Sprite sprite in ballSprites)
        {
            spriteDictionary.Add(sprite.name, sprite);
        }
        isCreat = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(isCreat){
            isCreat = false;
            Destroy_Creat();
        }
    }
    void Destroy_Creat(){
        Destroy(newBallNext);
        Invoke("checkBallColors", 0.1f);
    }
    // Hàm để thêm màu sắc vào mảng ballColors
    void checkBallColors(){
        uniqueColors.Clear(); // Xóa tất cả màu sắc hiện có từ HashSet
        ballCollor = new string[0];
        float maxCheckBall = ballBoom.maxCeiling/2f;
        if(mapRandom.checkStop){
            maxCheckBall = 100f;
        }
        // Lặp qua các quả bóng và lấy màu sắc của chúng
        foreach (GameObject ballMap in GameObject.FindGameObjectsWithTag("ballMap"))
        {
            if(ballMap.transform.position.y < maxCheckBall){
                // Lấy component SpriteRenderer sprite name của quả bóng
                string renderer = ballMap.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
                if (renderer != null)
                {
                    string color = renderer;
                    uniqueColors.Add(color);
                    AddBallColor(color);
                }
            }
        }
        foreach (GameObject ballMap in GameObject.FindGameObjectsWithTag("ballIce"))
        {
            if(ballMap.transform.position.y < maxCheckBall){
                // Lấy component SpriteRenderer sprite name của quả bóng
                string renderer = ballMap.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
                // Kiểm tra xem phần tử hiện tại có kết thúc bằng "Ice" không
                if (renderer.EndsWith("Ice"))
                {
                    // Nếu có, loại bỏ "Ice" từ phần tử
                    renderer = renderer.Substring(0, renderer.Length - 3);
                }
                if (renderer != null)
                {
                    string color = renderer;
                    uniqueColors.Add(color);
                    AddBallColor(color);
                }
            }
        }
        creatballRandom();
    }
    void AddBallColor(string color)
    {
        // Tạo mảng mới có kích thước lớn hơn mảng hiện tại 1 đơn vị
        string[] newColors = new string[ballCollor.Length + 1];
        // Sao chép mảng cũ vào mảng mới
        for (int i = 0; i < ballCollor.Length; i++)
        {
            newColors[i] = ballCollor[i];
        }
        // Thêm màu sắc mới vào vị trí cuối cùng của mảng mới
        newColors[newColors.Length - 1] = color;
        // Gán mảng mới vào mảng ballCollor
        ballCollor = newColors;
    }
    void creatballRandom(){
        if(ballCollor.Length == 0){
            return;
        }
        if(checkStart){
            bool found = false; // Biến cờ để kiểm tra xem có phần tử nào trùng khớp không
            // for (int i = 0; i < ballCollor.Length; i++)
            // {
            //     if (n == ballCollor[i])
            //     {
            //         m = n; // Gán m bằng nếu n trùng khớp với phần tử nào đó trong mảng ballColor
            //         found = true; // Đặt cờ thành true
            //         break; // Thoát khỏi vòng lặp sau khi tìm thấy phần tử trùng khớp
            //     }
            // }
            // Lặp qua các quả bóng và lấy màu sắc của chúng
            foreach (GameObject ballMap in GameObject.FindGameObjectsWithTag("ballMap"))
            {
                if(ballMap.transform.position.y < ballBoom.maxCeiling){
                    if (n == ballMap.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name)
                    {
                        m = n; // Gán m bằng nếu n trùng khớp với phần tử nào đó trong mảng ballColor
                        found = true; // Đặt cờ thành true
                        break; // Thoát khỏi vòng lặp sau khi tìm thấy phần tử trùng khớp
                    }
                }
            }
            foreach (GameObject ballMap in GameObject.FindGameObjectsWithTag("ballIce"))
            {
                if(ballMap.transform.position.y < ballBoom.maxCeiling){
                    if (n == ballMap.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name)
                    {
                        m = n; // Gán m bằng nếu n trùng khớp với phần tử nào đó trong mảng ballColor
                        found = true; // Đặt cờ thành true
                        break; // Thoát khỏi vòng lặp sau khi tìm thấy phần tử trùng khớp
                    }
                }
            }
            if (!found)
            {
                m = ballCollor[Random.Range(0, ballCollor.Length)]; // gán m bằng 1 màu ngẫu nhiên
            }
        }
        else{
            m = ballCollor[Random.Range(0, ballCollor.Length)]; // gán m bằng 1 màu ngẫu nhiên
        }
        newBallFire = Instantiate(ballFire, pointFire.position, Quaternion.identity); // Tạo bóng bắn mới
        fireSprite = GetSprite(m); // Lấy sprite dựa trên tên
        if (fireSprite != null)
        {
            // Sử dụng sprite được tìm thấy
            newBallFire.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = fireSprite;
        }
        n = ballCollor[Random.Range(0, ballCollor.Length)]; // gán n bằng 1 màu ngẫu nhiên
        newBallNext = Instantiate(ballNext, pointNext.position, Quaternion.identity); // Tạo bóng next mới
        nextSprite = GetSprite(n); // Lấy sprite dựa trên tên
        if (nextSprite != null)
        {
            // Sử dụng sprite được tìm thấy
            newBallNext.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = nextSprite;
        }
        checkStart = true;
    }
        // Hàm để lấy sprite dựa trên tên
    public Sprite GetSprite(string spriteName){
        Sprite sprite;
        if (spriteDictionary.TryGetValue(spriteName, out sprite))
        {
            return sprite;
        }
        else
        {
            return null;
        }
    }
    public void _swapBall(){
        if(buttonBoom.boolBoom){
            string a = m;
            m = n;
            n = a;
            fireSprite = GetSprite(m); // Lấy sprite dựa trên tên
            if (fireSprite != null)
            {
                // Sử dụng sprite được tìm thấy
                newBallFire.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = fireSprite;
            }
            nextSprite = GetSprite(n); // Lấy sprite dựa trên tên
            if (nextSprite != null)
            {
                // Sử dụng sprite được tìm thấy
                newBallNext.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = nextSprite;
            }
        }
    }
    void _checkCreat(){
        GameObject[] balls = GameObject.FindGameObjectsWithTag("ballFall");
        if (balls.Length == 0) {
            Invoke("Destroy_Creat", 0.1f);
        }
        else{
            Invoke("_checkCreat", 0.1f);
        }
    }
}
