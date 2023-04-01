using System.ComponentModel.DataAnnotations.Schema;

namespace TCIPlaba1.Models
{
    public class ParticipantParticipantMatch
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Match { get; set; }

        public int Team1 { get; set; }

        public byte TeamRole1 { get; set; }

        public byte Goals1 { get; set; }

        public int Team2 { get; set; }

        public byte TeamRole2 { get; set; }

        public byte Goals2 { get; set; }
        public DateTime Date { get; set; }

        public byte Division { get; set; }
        public virtual Division DivisionNavigation { get; set; } = null!;


        public byte Stadium { get; set; }


    }
}
