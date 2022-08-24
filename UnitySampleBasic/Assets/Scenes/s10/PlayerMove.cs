using System;
using UniRx;
using UnityEngine;

namespace Events
{
    /// <summary>
    /// 例：Inputをみて移動する
    /// </summary>
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 1.0f;
        [SerializeField] private InputEventProvider _inputEventProvider;

        private CharacterController _characterController;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();

            // ジャンプ
            // ジャンプボタン入力イベントを判定
            _inputEventProvider.Jump
                // ボタンが押された時に、
                .Where(x => x)
                // 接地中であり、
                .Where(_ => _characterController.isGrounded)
                // 最後にジャンプしてから1秒以上経過しているなら、
                .ThrottleFirst(TimeSpan.FromSeconds(1))
                .Subscribe(_ => {
                    // ジャンプ処理を実行する
                    Jump();
                });

            // 移動処理
            _inputEventProvider
                .MoveDirection
                // 一定値以上入力されたなら
                .Where(x => x.magnitude > 0.5f)
                .Subscribe(x => {
                    // そっち方向に移動する
                    _characterController.Move(x.normalized * _moveSpeed);
                });
        }

        private void Jump()
        {
            // ジャンプ処理（省略）
        }
    }
}