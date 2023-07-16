namespace AllPlayerActions
{
    public class SideMovementAction : MovementAction
    {
        public void MoveToTheRight() => MoveToTheSide(true);

        public void MoveToTheLeft() => MoveToTheSide(false);

    }
}
