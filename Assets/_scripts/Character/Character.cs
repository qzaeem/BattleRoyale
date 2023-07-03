using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public abstract class Character : MonoBehaviour
{
    public Canvas healthCanvas;

    [SerializeField] protected Transform weaponTransform;
    [SerializeField] protected Renderer bodyRenderer; 
    [SerializeField] protected Image healthBar;

    private CharacterData _characterData;
    private Vector3 _initialPosition, _initialAngles;
    protected Color _color;
    private float _health;

    public Transform TargetTranform { get; set; }
    public CharacterData Data => _characterData;
    public float Health { get { return _health; } }

    protected abstract void SendGameOverEvent();
    protected abstract void AttackHasEnded();
    protected abstract void GetTargetTransform();

    public virtual void InitializeValues()
    {
        _initialPosition = transform.position;
        _initialAngles = transform.eulerAngles;
        _health = _characterData == null ? 100 : _characterData.health;
        healthBar.fillAmount = 1;
    }

    public virtual void Init(CharacterData characterData, Color color, bool isPlayer)
    {
        _characterData = characterData;

        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
        bodyRenderer.GetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetColor("_Color", color);
        bodyRenderer.SetPropertyBlock(materialPropertyBlock);

        _color = color;

        gameObject.tag = isPlayer ? Constants.PLAYER_TAG : Constants.ENEMY_TAG;

        var weapon = weaponTransform.GetChild(0).GetComponent<Weapon>();
        weapon.targetTag = isPlayer ? Constants.ENEMY_TAG : Constants.PLAYER_TAG;
        weapon.attackDamage = characterData.attackDamage;

        healthBar.transform.parent.gameObject.SetActive(false);
    }

    protected virtual void Attack()
    {
        Vector3 initialAngles = weaponTransform.localEulerAngles;
        Vector3 targetAngles = new Vector3(weaponTransform.localEulerAngles.x,
            weaponTransform.localEulerAngles.y, -90);

        DOTween.Sequence().AppendInterval(0.5f).Append(weaponTransform.DOLocalRotate(targetAngles, 0.1f))
            .Append(weaponTransform.DOLocalRotate(initialAngles, 0.1f))
            .AppendInterval(1).AppendCallback(MoveBack);
    }

    protected virtual void MoveToTarget()
    {
        Vector3 targetPosition = TargetTranform.position + TargetTranform.forward;
        var targetRot =
            Quaternion.LookRotation((targetPosition - transform.position).normalized, Vector3.up).eulerAngles;
        Vector3 targetAngles = new Vector3(transform.eulerAngles.x, targetRot.y, transform.eulerAngles.z);

        DOTween.Sequence().Append(transform.DORotate(targetAngles, 0.5f))
            .Append(transform.DOMove(targetPosition, 3)).AppendCallback(() =>
            {
                Attack();
            });
    }

    protected virtual void MoveBack()
    {
        var targetPos = _initialPosition;
        var targetRot =
            Quaternion.LookRotation((targetPos - transform.position).normalized, Vector3.up).eulerAngles;
        Vector3 targetAngles = new Vector3(transform.eulerAngles.x, targetRot.y, transform.eulerAngles.z);

        DOTween.Sequence().Append(transform.DORotate(targetAngles, 0.5f))
            .Append(transform.DOMove(targetPos, 3)).Append(transform.DORotate(_initialAngles, 0.5f))
            .AppendCallback(AttackHasEnded);
    }

    public virtual void StartAttack()
    {
        GetTargetTransform();
        MoveToTarget();
    }

    public void ShowHealthBar()
    {
        healthBar.transform.parent.gameObject.SetActive(true);
        float healthVal = _health / _characterData.health;
        healthBar.fillAmount = healthVal;
    }

    public virtual void ApplyDamage(float attackDamage)
    {
        _health -= attackDamage;

        if (_health <= 0)
        {
            _health = 0;
            SendGameOverEvent();
        }

        float healthVal = _health / _characterData.health;
        healthBar.fillAmount = healthVal;
    }
}
