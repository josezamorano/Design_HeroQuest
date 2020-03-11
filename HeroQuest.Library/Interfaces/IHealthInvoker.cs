
namespace HeroQuest.Library.Interfaces
{
    public interface IHealthInvoker
    {
        IHealthState GetHealthState(int playerHealthPoints);
    }
}
