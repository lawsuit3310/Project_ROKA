public interface IFightable
{
        public void GiveDamage(CharacterController caller);
        public void Heal(float amount);

        public void OnDamaged();
        public void OnDead();
}