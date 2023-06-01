using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public bool flipLeftAndRight;

    [SerializeField] private float speed;
    [SerializeField] private Transform wallCheckLeft;
    [SerializeField] private Transform wallCheckRight;
    [SerializeField] private Transform pitCheckLeft;
    [SerializeField] private Transform pitCheckRight;

    private bool leftSideClear;
    private bool rightSideClear;
    private bool leftPitClear;
    private bool rightPitClear;
    private bool movingLeft = true;
    private bool movingRight;

    private SpriteRenderer sr;

    private void Awake() =>
        sr = gameObject.GetComponent<SpriteRenderer>();

    // Update is called once per frame
    void Update()
    {
        leftSideClear = !Physics2D.Linecast(transform.position, wallCheckLeft.position, 1 << LayerMask.NameToLayer("Ground"));
        rightSideClear = !Physics2D.Linecast(transform.position, wallCheckRight.position, 1 << LayerMask.NameToLayer("Ground"));
        leftPitClear = Physics2D.Linecast(transform.position, pitCheckLeft.position, 1 << LayerMask.NameToLayer("Ground"));
        rightPitClear = Physics2D.Linecast(transform.position, pitCheckRight.position, 1 << LayerMask.NameToLayer("Ground"));

        if (!leftSideClear || !rightSideClear || !leftPitClear || !rightPitClear)
            ChangeDirection();

        if (movingLeft)
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        else if (movingRight)
            transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    private void ChangeDirection()
    {
        if (!leftSideClear || !leftPitClear)
        {
            movingLeft = false;
            movingRight = true;

            if (flipLeftAndRight)
                sr.flipX = true;
            else
                sr.flipX = false;
        }
        else if (!rightSideClear || !rightPitClear)
        {
            movingLeft = true;
            movingRight = false;

            if (flipLeftAndRight)
                sr.flipX = false;
            else
                sr.flipX = true;
        }
    }
}
