public interface IPlayerMovement
{
    void UpdateMovement();
    float CurrentSpeed { get; }
    void SetMoveSpeed(float newSpeed);
}
