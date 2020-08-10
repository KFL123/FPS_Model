using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMent : MonoBehaviour
{
    private Transform characterTeansform;
    private Rigidbody charRigidbody;
    private bool isGrounded;//是否离地面状态

    public float MoveSpeed;//速度
    public float gravity;//重力
    public float JumpHeight;//跳高

    private void Start()
    {
        characterTeansform = transform;
        charRigidbody = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        //当前再地面状态
        if (isGrounded)
        {
            var tmp_Horizontal = Input.GetAxis("Horizontal");
            var tmp_Vertical = Input.GetAxis("Vertical");
            //世界坐标的的运动方向
            var tmp_currentDirection = new Vector3(tmp_Horizontal, 0, tmp_Vertical);
            tmp_currentDirection = characterTeansform.TransformDirection(tmp_currentDirection);//自身坐标转成世界坐标
            tmp_currentDirection *= MoveSpeed;//速度
            var charRigidbodySpeed = charRigidbody.velocity;//获取当前刚体的速度
                                                            //为什么要用自己设定的速度减去 当前的速度
            var tmp_VelocityChange = tmp_currentDirection - charRigidbodySpeed;//计算速度减去当前的速度
            tmp_VelocityChange.y = 0;
            //给刚体加力量    刚体添加力    forceMove  作用力的的方式
            charRigidbody.AddForce(tmp_VelocityChange, ForceMode.VelocityChange);

            if (Input.GetButtonDown("Jump"))
            {//跳的逻辑
                charRigidbody.velocity = new Vector3(charRigidbodySpeed.x, CalculateJumpHeightSpeed(), charRigidbodySpeed.z);
            }
        }
            //当处于高处时修改刚体  修改重力
        charRigidbody.AddForce(new Vector3(0, -gravity * Time.deltaTime, 0));
  
    }
    /// <summary>
    /// 跳跃的速度取决于我们的重力和跳跃高度 
    /// </summary>
    /// <returns></returns>
    private float CalculateJumpHeightSpeed()
    {
        return Mathf.Sqrt(2* gravity * JumpHeight);
    }
    /// <summary>
    /// 当一直处于这种状态
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
        Debug.LogError("当前状态1");
    }
    /// <summary>
    /// 退出这种状态
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
        Debug.LogError("当前状态2");
    }

}
