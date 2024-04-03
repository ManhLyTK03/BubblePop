using UnityEngine;

public class aimingLine : MonoBehaviour
{
    public Transform startPoint;
    public LineRenderer lineRenderer;
    public static float limitLine; // độ cao tối thiểu quả bóng đc bắn
    public bool checkClick = false;
    public SpriteRenderer ballRenderer;
    public static Vector3[] pointHit;
    private Vector2 direction;
    private RaycastHit2D hit;
    public static Vector3 ponitBallCollider;
    public GameObject ballCheck;
    public float widthBall;// chiều rộng của bóng
    public float distanceX,distanceY;
    public float leftEdgeX;
    public string[] tagsToSearch; // Mảng các tag cần tìm

    void Start(){
        lineRenderer = GetComponent<LineRenderer>();
        startPoint = GetComponent<Transform>();
        widthBall = ballRenderer.bounds.size.x;
        limitLine = startPoint.position.y + widthBall *1.5f;
        ballRenderer = GetComponent<SpriteRenderer>();
        distanceX = widthBall/2;
        distanceY = widthBall/2*Mathf.Sqrt(3f);
        leftEdgeX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;// Lấy tọa độ x của cạnh trái (left) của màn hình
        tagsToSearch = new string[]{"ballMap", "boomMap", "ballStone"};
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !fireBall.boolFire) // Kiểm tra xem người dùng đã nhấn chuột trái chưa
        {
            checkClick = true;
            ballCheck.GetComponent<Collider2D>().enabled = false;
        }
        if(checkClick){
            pointHit = new Vector3[0];
            // Thêm điểm bắt đầu vào mảng
            AddVectorToEnd(startPoint.position);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Lấy vị trí của chuột trong không gian thế giới
            if(mousePosition.y > limitLine){
                GameObject ballFireObject = GameObject.FindWithTag("ballFire");
                string[] tags = { "ballLine", "ballBoom", "ballLaze", "ballRainbow"};
                foreach (string tag in tags)
                {
                    if(ballFireObject != null){
                        break;
                    }
                    ballFireObject = GameObject.FindWithTag(tag);
                }
                // Bật hiển thị của line renderer
                lineRenderer.enabled = true;
                SpriteRenderer spriteBall = ballFireObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
                // Chuyển đổi chuỗi màu thành màu Color
                Color color = GetColorFromString(spriteBall.sprite.name);
                
                // Thiết lập màu cho đường lineRenderer
                lineRenderer.startColor = color;
                lineRenderer.endColor = color;
                mousePosition.z = 0f;

                // Tính toán vector hướng từ điểm bắt đầu đến vị trí chuột
                direction = (mousePosition - startPoint.position).normalized;
                
                // Tạo một ray từ vị trí của đối tượng đến chuột
                hit = Physics2D.Raycast(startPoint.position, direction,9999f, ~LayerMask.GetMask(new string[]{"ballFire"})); 
                    
                lineRenderer.SetPosition(0, pointHit[0]);
                if(ballFireObject.tag == "ballLine"){
                    lineRenderer.positionCount  = 2;
                    AddVectorToEnd(hit.point);
                    lineRenderer.SetPosition(1, pointHit[pointHit.Length - 1]);
                }
                else{
                    for (int i = 1;i<10;i++)
                    {
                        if(hit.collider != null){
                            AddVectorToEnd(hit.point);
                            lineRenderer.positionCount  = i+1;
                            lineRenderer.SetPosition(i, pointHit[i]);
                            if(hit.collider.tag == "vienMH"){
                                direction = Vector2.Reflect(direction, hit.normal);
                                hit = Physics2D.Raycast(pointHit[i], direction,9999f, ~LayerMask.GetMask(new string[]{"ballCheck"}));
                            }
                            else{
                                break;
                            }
                        }
                    }
                }
                
                // Duyệt qua mỗi tag trong mảng tagsToSearch
                foreach (string tagToSearch in tagsToSearch){
                    if(hit.collider.tag == tagToSearch){
                        // Tính vector hướng của đường thẳng
                        Vector2 direction = pointHit[pointHit.Length-1] - hit.collider.transform.position;

                        // Tính góc giữa vector hướng và trục Ox
                        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                        // Đảm bảo góc nằm trong khoảng [0, 360)
                        angle = (angle + 360) % 360;
                        if(angle > 270f && angle <= 330f){
                            pointHit[pointHit.Length - 1] = new Vector3(hit.collider.transform.position.x + distanceX, hit.collider.transform.position.y - distanceY, transform.position.z);
                        }
                        else if(angle > 210f && angle <= 270f){
                            pointHit[pointHit.Length - 1] = new Vector3(hit.collider.transform.position.x - distanceX, hit.collider.transform.position.y - distanceY, transform.position.z);
                        }
                        else if(angle > 90f && angle <= 210f){
                            pointHit[pointHit.Length - 1] = new Vector3(hit.collider.transform.position.x - 2f*distanceX, hit.collider.transform.position.y, transform.position.z);
                        }
                        else{
                            pointHit[pointHit.Length - 1] = new Vector3(hit.collider.transform.position.x + 2f*distanceX, hit.collider.transform.position.y, transform.position.z);
                        }
                        // Kiểm tra xem có GameObject nào ở vị trí pointPosition có tag là "ball" không
                        Collider2D colliderCheckpoint = Physics2D.OverlapPoint(pointHit[pointHit.Length - 1]);
                        if (colliderCheckpoint != null && (colliderCheckpoint.CompareTag(tagToSearch) || colliderCheckpoint.CompareTag("vienMH")))
                        {
                            if(angle > 90f && angle <= 210f){
                                pointHit[pointHit.Length - 1] = new Vector3(hit.collider.transform.position.x - distanceX, hit.collider.transform.position.y - distanceY, transform.position.z);
                            }
                            else if(angle > 270f && angle <= 330f){
                                pointHit[pointHit.Length - 1] = new Vector3(hit.collider.transform.position.x - distanceX, hit.collider.transform.position.y - distanceY, transform.position.z);
                            }
                            else if(angle > 210f && angle <= 270f){
                                pointHit[pointHit.Length - 1] = new Vector3(hit.collider.transform.position.x + distanceX, hit.collider.transform.position.y - distanceY, transform.position.z);
                            }
                            else{
                                pointHit[pointHit.Length - 1] = new Vector3(hit.collider.transform.position.x + distanceX, hit.collider.transform.position.y - distanceY, transform.position.z);
                            }
                        }
                    }
                }
                if(hit.collider.tag  == "wallTop"){
                    float min = widthBall/2f;
                    float positionBall = 0f;
                    for (float i = leftEdgeX + widthBall/2;i<=widthBall*11; i += widthBall)
                    {
                        if(min >= Mathf.Abs(hit.point.x - i)){
                            min = Mathf.Abs(hit.point.x - i);
                            positionBall = i;
                        }
                    }
                    
                    pointHit[pointHit.Length - 1] = new Vector3(positionBall,hit.collider.transform.position.y - widthBall/2,0f);
                    // Kiểm tra xem có GameObject nào ở vị trí pointPosition có tag là "ball" không
                    Collider2D colliderCheckpoint = Physics2D.OverlapPoint(pointHit[pointHit.Length - 1]);
                    if (colliderCheckpoint != null && colliderCheckpoint.CompareTag("ballMap"))
                    {
                        if(hit.point.x < pointHit[pointHit.Length - 1].x){
                            pointHit[pointHit.Length - 1].x = pointHit[pointHit.Length - 1].x - widthBall;
                        }
                        else{
                            pointHit[pointHit.Length - 1].x = pointHit[pointHit.Length - 1].x + widthBall;
                        }
                    }
                }
                ballCheck.transform.position = pointHit[pointHit.Length - 1];
                ballCheck.SetActive(true);
            }
            else{
                lineRenderer.enabled = false;
            }
        }
        if (Input.GetMouseButtonUp(0)) // Kiểm tra xem người dùng đã nhấn chuột trái chưa
        {
            ballCheck.GetComponent<Collider2D>().enabled = true;
            ponitBallCollider = ballCheck.transform.position;
            checkClick = false;
            lineRenderer.enabled = false;
        }
    }
    Color GetColorFromString(string colorName)
    {
        switch (colorName)
        {
            case "ballRed":
                return Color.red;
            case "ballBlue":
                return Color.blue;
            case "ballGreen":
                return Color.green;
            case "ballYellow":
                return Color.yellow;
            // Thêm các màu khác nếu cần
            default:
                return Color.white; // Màu mặc định
        }
    }

    void AddVectorToEnd(Vector3 newVector)
    {
        // Tạo mảng tạm thời lớn hơn 1 phần tử so với mảng cũ
        Vector3[] tempArray = new Vector3[pointHit.Length + 1];

        // Sao chép tất cả các phần tử từ mảng cũ sang mảng tạm
        for (int i = 0; i < pointHit.Length; i++)
        {
            tempArray[i] = pointHit[i];
        }
        // Gán vector mới vào cuối mảng tạm
        tempArray[tempArray.Length - 1] = newVector;

        // Gán mảng tạm vào mảng gốc
        pointHit = tempArray;
    }
}