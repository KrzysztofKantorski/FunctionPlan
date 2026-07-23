namespace Domain.Common
{
    public abstract class Entity
    {
        public int Id { get; protected set; }

        //Constructor for ef core
        protected Entity() 
        {
        }

        protected Entity(int id) {
            Id = id;
        }
    }
}
