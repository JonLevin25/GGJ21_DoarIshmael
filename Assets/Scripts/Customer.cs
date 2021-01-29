using UnityEngine;

public class Customer : MonoBehaviour
{
    public static int SatisfiedCustomers;

    public void ReceivePackage(PackageBox package)
    {
        SatisfiedCustomers++;
    }
}
