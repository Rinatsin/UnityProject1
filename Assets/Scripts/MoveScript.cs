using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    //Основные параметры
    public float speedMove;//Скорость персонажа
    public float jumpPower;//Сила прыжка

    //Параметры геймплея для персонажа
    private float gravityForce;//гравитация персонажа
    private Vector3 moveVector;//направление движения

    //Ссылки на компоненты
    private CharacterController ch_controller;
    private Animator ch_animator;

    void Start()
    {
        ch_controller = GetComponent<CharacterController>();
        ch_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMove();
        GamingGravity();
    }

    //Метод перемещения персонажа
    private void CharacterMove()
    {
        //перемещение по поверхности
        if (ch_controller.isGrounded)
        {
            ch_animator.ResetTrigger("Jump");
            ch_animator.SetBool("Failing", false);

            moveVector = Vector3.zero;
            moveVector.x = Input.GetAxis("Horizontal") * speedMove;
            moveVector.z = Input.GetAxis("Vertical") * speedMove;

            //Анимация передвижения персонажа
            if (moveVector.x != 0 || moveVector.z != 0) ch_animator.SetBool("Move", true);
            else ch_animator.SetBool("Move", false);

            //поворот персонажа в сторону направления перемещения
            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
                Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedMove, 0.0f);
                transform.rotation = Quaternion.LookRotation(direct);
            }
        }
        else
        {
            //при приложении силы гравитации более указанной воспроизводиться анимация падения
            if (gravityForce < -3f) ch_animator.SetBool("Failing", true);
        }

        moveVector.y = gravityForce;
        ch_controller.Move(moveVector * Time.deltaTime);//метод движения  по направлению
    }

    // метод гравитации
    private void GamingGravity()
    {
        if (!ch_controller.isGrounded)gravityForce -= 20f * Time.deltaTime;
        else gravityForce = -1f;

        if (Input.GetKeyDown(KeyCode.Space) && ch_controller.isGrounded)
        {
            gravityForce = jumpPower;
            ch_animator.SetTrigger("Jump");
        }
    }
}
