public interface ILinkable {

    LinkedFire Link {
        get;
        set;
    }

    IWeapon Weapon {
        get;
        set;
    }

    void OnFire();

    float GetFirerate();

}
