public interface ILinkable {

    LinkedFire Link {
        get;
        set;
    }

    IWeapon Weapon {
        get;
        set;
    }

    bool CanLink();

    void OnFire();

    float GetFirerate();

}
