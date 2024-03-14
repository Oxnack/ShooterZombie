

public class HP
{
    public int hp = 100;
    public bool life = true;

    public void GetAttack(int hpM)
    {
        hp -= hpM;
        if (hp <= 0)
        {
            life = false;
        }
    }
}
