class Player
{
    void Attack(IAttack attack)
    {
        attack.Execute(this);
    }
}