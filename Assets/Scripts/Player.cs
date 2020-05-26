using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class Player : LivingEntity
{

  public float moveSpeed = 5;

  Camera viewCamera;
  PlayerController controller;
  GunController gunController;

  protected override void Start()
  {
    base.Start();
    controller = GetComponent<PlayerController>();
    gunController = GetComponent<GunController>();
    viewCamera = Camera.main;
  }

  void Update()
  {
    // movement input
    // getAxisRaw removes smoothing
    Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    Vector3 moveVelocity = moveInput.normalized * moveSpeed;
    controller.Move(moveVelocity);

    //look Input
    Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
    Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
    float rayDistance;


    //pass in ray and out raydistance value as vector3
    //giving us length from camera to ground intersection
    if (groundPlane.Raycast(ray, out rayDistance))
    {
      Vector3 point = ray.GetPoint(rayDistance);
      //Debug.DrawLine(ray.origin, point, Color.red);
      //Debug.DrawRay(ray.origin,ray.direction * 100,Color.red);
      controller.LookAt(point);
    }

    //weapon input
    if (Input.GetMouseButton(0))
    {
      gunController.Shoot();
    }
  }
}