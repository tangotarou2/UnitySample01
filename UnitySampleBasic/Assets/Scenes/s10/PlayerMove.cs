using System;
using UniRx;
using UnityEngine;

namespace Events
{
    /// <summary>
    /// ��FInput���݂Ĉړ�����
    /// </summary>
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 1.0f;
        [SerializeField] private InputEventProvider _inputEventProvider;

        private CharacterController _characterController;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();

            // �W�����v
            // �W�����v�{�^�����̓C�x���g�𔻒�
            _inputEventProvider.Jump
                // �{�^���������ꂽ���ɁA
                .Where(x => x)
                // �ڒn���ł���A
                .Where(_ => _characterController.isGrounded)
                // �Ō�ɃW�����v���Ă���1�b�ȏ�o�߂��Ă���Ȃ�A
                .ThrottleFirst(TimeSpan.FromSeconds(1))
                .Subscribe(_ => {
                    // �W�����v���������s����
                    Jump();
                });

            // �ړ�����
            _inputEventProvider
                .MoveDirection
                // ���l�ȏ���͂��ꂽ�Ȃ�
                .Where(x => x.magnitude > 0.5f)
                .Subscribe(x => {
                    // �����������Ɉړ�����
                    _characterController.Move(x.normalized * _moveSpeed);
                });
        }

        private void Jump()
        {
            // �W�����v�����i�ȗ��j
        }
    }
}