using UniRx;
using UnityEngine;

namespace Events
{
    public sealed class InputEventProvider : MonoBehaviour
    {
        /// <summary>
        /// �U���{�^������
        /// </summary>
        public IReadOnlyReactiveProperty<bool> Attack => _attack;

        /// <summary>
        /// �ړ���������
        /// </summary>
        public IReadOnlyReactiveProperty<Vector3> MoveDirection => _moveDirection;

        /// <summary>
        /// �W�����v����
        /// </summary>
        public IReadOnlyReactiveProperty<bool> Jump => _jump;

        // ����
        private readonly ReactiveProperty<bool> _attack = new BoolReactiveProperty();
        private readonly ReactiveProperty<bool> _jump = new BoolReactiveProperty();
        private readonly ReactiveProperty<Vector3> _moveDirection = new ReactiveProperty<Vector3>();

        private void Start()
        {
            // Destroy����Dispose()����
            _attack.AddTo(this);
            _jump.AddTo(this);
            _moveDirection.AddTo(this);
        }

        private void Update()
        {
            // �e����͂�ReactiveProperty�ɔ��f
            _jump.Value = Input.GetButton("Jump");
       //     _attack.Value = Input.GetButton("Attack");
            _moveDirection.Value = new Vector3(
                x: Input.GetAxis("Horizontal"),
                y: 0,
                z: Input.GetAxis("Vertical"));
        }
    }
}