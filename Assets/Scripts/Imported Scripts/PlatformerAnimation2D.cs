using UnityEngine;

namespace PC2D
{
    /// <summary>
    /// This is a very very very simple example of how an animation system could query information from the motor to set state.
    /// This can be done to explicitly play states, as is below, or send triggers, float, or bools to the animator. Most likely this
    /// will need to be written to suit your game's needs.
    /// </summary>

    public class PlatformerAnimation2D : MonoBehaviour
    {
        public GameObject visualChild;

        private PlatformerMotor2D _motor;
        private PlayerManager _playerManager;
        private Animator _animator;
        private bool _isJumping;
        private bool _currentFacingLeft;

        private SpriteRenderer _sprite;

        // Use this for initialization
        void Start()
        {
            _motor = GetComponent<PlatformerMotor2D>();
            _playerManager = GetComponent<PlayerManager>();
            _animator = visualChild.GetComponent<Animator>();
            _sprite = visualChild.GetComponent<SpriteRenderer>();
            _animator.Play("DuaeIdle");
            

            _motor.onJump += SetCurrentFacingLeft;
        }

        // Update is called once per frame
        void Update()
        {
            if (_motor.motorState == PlatformerMotor2D.MotorState.Jumping )
            {
                if (_playerManager.duaeState == PlayerManager.PlayerState.Monster)
                {
					_isJumping = true;
                    _animator.Play("MONSTER_JUMP");
                }
                else {
					_isJumping = true;
                    _animator.Play("DUAE_JUMP");
                }



                if (_motor.velocity.x <= -0.1f)
                {
                    _currentFacingLeft = true;
                }
                else if (_motor.velocity.x >= 0.1f)
                {
                    _currentFacingLeft = false;
                }

               // Vector3 rotateDir = _currentFacingLeft ? Vector3.forward : Vector3.back;
                
            }
            else
            {
                _isJumping = false;
                visualChild.transform.rotation = Quaternion.identity;

                if (_motor.motorState == PlatformerMotor2D.MotorState.Falling ||
                                 _motor.motorState == PlatformerMotor2D.MotorState.FallingFast || 
				   							_motor.motorState == PlatformerMotor2D.MotorState.HeavyFalling)
                {
                    if (_playerManager.duaeState == PlayerManager.PlayerState.Monster )
                    {
                        _animator.Play("MONSTER_FALL");
                    }
                    else if (_playerManager.duaeState == PlayerManager.PlayerState.Uncloaked_Form3 &&
						_motor.motorState == PlatformerMotor2D.MotorState.HeavyFalling)
                    {
                        _animator.Play("DUAE_GSLAM");
                    }
                    else {
                        _animator.Play("DUAE_FALL");
                    }
                    //_sprite.flipX = false;
                }
                else if ((_playerManager.duaeState == PlayerManager.PlayerState.Uncloaked_Form1 ||
					_playerManager.duaeState == PlayerManager.PlayerState.Uncloaked_Form2 || 
					_playerManager.duaeState == PlayerManager.PlayerState.Uncloaked_Form3) &&
                    (_motor.motorState == PlatformerMotor2D.MotorState.WallSliding ||
                     _motor.motorState == PlatformerMotor2D.MotorState.WallSticking))
                {
                    _animator.Play("DUAE_WALLCLING");
                    //_sprite.flipX = true;

                }
                else if (_motor.motorState == PlatformerMotor2D.MotorState.OnCorner)
                {
					_animator.Play("DUAE_WALLCLING");
                }
                else if (_motor.motorState == PlatformerMotor2D.MotorState.Slipping)
                {
                    _animator.Play("Slip");
                }
                else if (_motor.motorState == PlatformerMotor2D.MotorState.Dashing)
                {
                    _animator.Play("Dash");
                    
                }
                else
                {
                    if (_motor.velocity.sqrMagnitude >= 0.1f * 0.1f)
                    {
                        if(_playerManager.duaeState == PlayerManager.PlayerState.Monster)
                        {
                            _animator.Play("MONSTER_WALK");
                        }
                        else if ((_playerManager.duaeState == PlayerManager.PlayerState.Uncloaked_Form2 ||
							_playerManager.duaeState == PlayerManager.PlayerState.Uncloaked_Form3) && 
							(_motor.velocity.x == _playerManager.uncloakedGroundSpeed ||
								_motor.velocity.x == -_playerManager.uncloakedGroundSpeed))
                        {
							_animator.Play("DUAE_CHARGE");

						}                            
						else if(_playerManager.duaeState == PlayerManager.PlayerState.Uncloaked_Form1 ||
							_playerManager.duaeState == PlayerManager.PlayerState.Uncloaked_Form2 || 
							_playerManager.duaeState == PlayerManager.PlayerState.Uncloaked_Form3)
						{

							_animator.Play("Hybrid_WALK");
						}
                        else
                        {
                            _animator.Play("DUAE_WALK");
                        }
                        
                        //_sprite.flipX = false;
                    }
                    else
                    {
                        if (_playerManager.duaeState == PlayerManager.PlayerState.Monster)
                        {
                            _animator.Play("MONSTER_IDLE");
                        }
                        else {
                            _animator.Play("DuaeIdle");
                        }
                        //_sprite.flipX = false;
                    }
                }
            }

            // Facing
            float valueCheck = _motor.normalizedXMovement;

            if (_motor.motorState == PlatformerMotor2D.MotorState.Slipping ||
                _motor.motorState == PlatformerMotor2D.MotorState.Dashing ||
                _motor.motorState == PlatformerMotor2D.MotorState.Jumping)
            {
                valueCheck = _motor.velocity.x;
            }
            
            if (Mathf.Abs(valueCheck) >= 0.1f)
            {
                Vector3 newScale = visualChild.transform.localScale;
                newScale.x = Mathf.Abs(newScale.x) * ((valueCheck > 0) ? 1.0f : -1.0f);
				if(_motor.motorState == PlatformerMotor2D.MotorState.WallSliding ||
					_motor.motorState == PlatformerMotor2D.MotorState.WallSticking){
					newScale.x = -newScale.x;
				}
					visualChild.transform.localScale = newScale;

                
            }
        }

        private void SetCurrentFacingLeft()
        {
            _currentFacingLeft = _motor.facingLeft;
        }
    }
}
