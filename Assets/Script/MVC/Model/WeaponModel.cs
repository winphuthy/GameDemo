public class WeaponModel : ModelBase
{
    private static WeaponModel instance = new WeaponModel();

    private WeaponModel()
    {
    }

    public Weapon TrainingSword = new Weapon(
        new WeaponType[] {WeaponType.ForgingWeapon},
        1,
        "TrainingSword",
        100,
        "",
        0.3f,
        1,
        -1,
        new int[] {30, 30},
        ""
    );
}