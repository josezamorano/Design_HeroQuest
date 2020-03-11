namespace HeroQuest.Library.Interfaces
{
    public interface ITarget
    {
        System.Windows.Controls.Image TargetImage { get; set; }
        int GetHealthPoints();
        void ReduceHealth(int enemyDamagePoints);
    }
}
