public class Customer : PackageTarget
{
    public static int SatisfiedCustomers; // Score TODO: SHow in UI

    public override void OnPackageHit(PackageBox package)
    {
        base.OnPackageHit(package);
        SatisfiedCustomers++;
    }
}
