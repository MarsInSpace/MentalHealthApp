using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskButtons : MonoBehaviour
{
    Image Button;
    [SerializeField] float SizingSpeed;
    Vector3 StartScale;
    Vector2 StartPosition;

    [HideInInspector] public bool IsDragging = false;
    public static bool IsTaskComplete = false;

    public static int GrowthCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        Button = GetComponent<Image>();
        StartScale = transform.localScale;
        StartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Scaling();
        
        if(IsDragging){
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.nearClipPlane;
            transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        }
    }

    void Scaling(){
        float sizingSin = Mathf.Sin(Time.time * SizingSpeed);
        transform.localScale = new Vector3(StartScale.x + sizingSin, StartScale.y + sizingSin);
    }

    private void OnMouseDown() {
        IsDragging = true;
    }

    private void OnMouseUp() {
        if(PlantManager.IsMouseOver){
            IsDragging = false;
            IsTaskComplete = true;
            GrowthCounter++;
            this.gameObject.SetActive(false);
        }
        else{
            Vector2 currentPosition = transform.position;
            transform.position = Vector2.Lerp(currentPosition, StartPosition, 1);
            IsDragging = false;
        }
    }

}
