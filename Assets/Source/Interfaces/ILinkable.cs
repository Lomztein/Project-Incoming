public interface ILinkable {

    LinkedFire Link {
        get;
        set;
    }

    IWeapon Weapon {
        get;
    }

    void OnFire();

    float GetFirerate();

}
