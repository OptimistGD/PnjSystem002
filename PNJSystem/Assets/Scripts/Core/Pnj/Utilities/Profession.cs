namespace PNJSystem.Core.Utilities
{
    public class Profession :  IProfession
    {
        private readonly string description;
        public ProfessionId Id { get; }

        string IProfession.Description => description;
        

        public Profession(ProfessionId id, string description)
        {
            Id = id;
            this.description = description;
        }
    }
}