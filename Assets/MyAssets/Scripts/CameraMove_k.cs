using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 控制相机脚本
/// </summary>
public class CameraMove_k : MonoBehaviour
{
    private Transform CamerTrm;
    private Vector3 CameraRotation;
    [SerializeField] private Transform CharTransform;

    [Range(0.0f, 1f)]
    public float CameraSpeed;
    public Vector2 MaxminAngle;//限制范围
   
    // Start is called before the first frame update
    void Start()
    {
        CamerTrm = transform;   
    }

    // Update is called once per frame
    void Update()
    {
        //获取鼠标的方向给相机自动旋转
        var CameraX =Input.GetAxis("Mouse X");
        var CameraY =Input.GetAxis("Mouse Y");
        CameraRotation.x -= CameraY * CameraSpeed*10;
        CameraRotation.y += CameraX * CameraSpeed*10;
        //限制移动的范围
        CameraRotation.x = Mathf.Clamp(CameraRotation.x, MaxminAngle.x, MaxminAngle.y);
        CamerTrm.rotation = Quaternion.Euler(CameraRotation.x, CameraRotation.y,0);
        ////再移动镜头的时候控制任务跟随移动
        CharTransform.rotation = Quaternion.Euler(0, CameraRotation.y, 0);
        
    }
}
