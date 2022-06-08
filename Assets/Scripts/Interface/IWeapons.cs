using System.Collections;

public interface IWeapons
{
    void Shoot(bool shoot);
    IEnumerator Reload(float fireRate);
}
