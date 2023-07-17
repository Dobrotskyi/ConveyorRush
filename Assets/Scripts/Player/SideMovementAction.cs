namespace AllPlayerActions
{
    public class SideMovementAction : MovementAction
    {
        private const float Left_Border_X = -2.5f;
        private const float Right_Border_X = 0f;

        public void MoveToTheRight()
        {
            if (transform.position.x > Left_Border_X)
                MoveToTheSide(true);
            else
                TurnOffMovingAnimations();
        }

        public void MoveToTheLeft()
        {
            if (transform.position.x < Right_Border_X)
                MoveToTheSide(false);
            else
                TurnOffMovingAnimations();
        }

    }
}
