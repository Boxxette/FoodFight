using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController2D : MonoBehaviour {

 #region Settings 
    //Settings & variables for player speed, movement smoothing, and in-air movement. Settings for animation directions.  
    private float mSpeed = 6.0F;
    [Range(0, .3F)] [SerializeField] private float mSmoothMovements = .05F;
    [SerializeField] private bool mAirControls = true;
    private bool mFaceRight = true;
    private Rigidbody2D mRigidBody2D; 
    private Vector3 mDirection = Vector3.zero;

    //Settings & variables for player grounding and ceiling location.
    [SerializeField]  private LayerMask mWhereGround;
    const float isGroundedRadius = .2F;
    [SerializeField] private Transform mCheckGround;
    private bool mGrounded;
    const float isCeilingRadius = .2F;
    [SerializeField] private Transform mcheckCeiling; 

    //Settings & variables for player jumps and things affecting jumps. Settings for player falling. 
    private float mJumpSpeed = 8.0F;
    [SerializeField] private float mJumpForce = 400F;
    private bool mFalling; 
    private float gravity = 20.0F;

    //Settings & variables for player crouching and climbing. 
    [Range(0, 1)] [SerializeField] private float mCrouchSpeed = .36F;
    [SerializeField] private Collider2D mCrouchColliderDisabled;
    private bool mIsCrouching = false;
 #endregion

 #region Events
    public UnityEvent HasLandedEvent;

    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent isCrouchedEvent;

 #endregion


    // Start is used for initialization
    void Start () {
        mRigidBody2D = GetComponent<Rigidbody2D>();
        if (HasLandedEvent == null)
        {
            HasLandedEvent = new UnityEvent();
        }
        if (isCrouchedEvent == null)
        {
            isCrouchedEvent = new BoolEvent();
        }
	}
	
	// Update is called once per frame. Update checks for the ground. 
	void Update () {

        //Check to see if the player is grounded. 
        bool isGrounded = mGrounded;
        mGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(mCheckGround.position, isGroundedRadius, mWhereGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                mGrounded = true;
                if (!isGrounded)
                {
                    //check for falling
                    HasLandedEvent.Invoke();
                }
            }
        }
	}

    //MovePlayer moves the player appropriately. 
    public void MovePlayer(float speed, bool crouched)
    {
        //Check to see if the player is standing or crouching. If the player is crouching, try to stand them up. 
        if (crouched)
        {
            if (Physics2D.OverlapCircle(mcheckCeiling.position, isCeilingRadius, mWhereGround))
            {
                //Keep the player crouched.
            }
            else
            {
                crouched = false;
            }
        }
    }
    // text being added for git push. Please delete me
}
