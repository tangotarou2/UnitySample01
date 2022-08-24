using UniRx;
using UnityEngine;

namespace Events
{
    public sealed class InputEventProvider : MonoBehaviour
    {
        /// <summary>
        /// 攻撃ボタン入力
        /// </summary>
        public IReadOnlyReactiveProperty<bool> Attack => _attack;

        /// <summary>
        /// 移動方向入力
        /// </summary>
        public IReadOnlyReactiveProperty<Vector3> MoveDirection => _moveDirection;

        /// <summary>
        /// ジャンプ入力
        /// </summary>
        public IReadOnlyReactiveProperty<bool> Jump => _jump;

        // 実装
        private readonly ReactiveProperty<bool> _attack = new BoolReactiveProperty();
        private readonly ReactiveProperty<bool> _jump = new BoolReactiveProperty();
        private readonly ReactiveProperty<Vector3> _moveDirection = new ReactiveProperty<Vector3>();

        private void Start()
        {
            // Destroy時にDispose()する
            _attack.AddTo(this);
            _jump.AddTo(this);
            _moveDirection.AddTo(this);
        }

        private void Update()
        {
            // 各種入力をReactivePropertyに反映
            _jump.Value = Input.GetButton("Jump");
       //     _attack.Value = Input.GetButton("Attack");
            _moveDirection.Value = new Vector3(
                x: Input.GetAxis("Horizontal"),
                y: 0,
                z: Input.GetAxis("Vertical"));
        }
    }
}